using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Concurrent;
using System.Threading;
using MktSrvcAPI;
using System.Diagnostics;

namespace FastOMS
{
    public class OrderManager
    {
        public delegate void NewOrderAcceptedHandler(OrderInfo order, Guid ownerControlGuid);
        public event NewOrderAcceptedHandler OnNewOrderAccepted;

        public delegate void OrderStatusChangedHandler(OrderInfo order, Guid ownerControlGuid);
        public event OrderStatusChangedHandler OnOrderStatusChanged;

        public delegate void OrderRouterReconnectedHandler();
        public event OrderRouterReconnectedHandler OnOrderRouterReconnected;

        public delegate void OrderRouterDisconnectedHandler();
        public event OrderRouterDisconnectedHandler OnOrderRouterDisconnected;

        Timer _orderCheckTimer;

        private const int ISE_SPREAD_QUOTE_PORT = 33000;
        private const int SA_PORT = 9401;
        private const string Host68 = "172.20.168.71";
        private const string Host69 = "172.20.168.71";
        string _password = "test";
        string _username = "test";

        private StratClient _stratClient;
        ConcurrentQueue<KeyValuePair<Guid, OrderInfo>> _unsentOrderQueue;
        LinkedList<KeyValuePair<Guid, OrderInfo>> _waitingForOrderIDList;

        ConcurrentDictionary<ulong, KeyValuePair<Guid, OrderInfo>> _executedOrderDictionary;
        
        OrderManager _thisOrderManager;
        volatile int _numOrdersQueued = 0;

        public bool isConnected
        {
            get { return _stratClient.IsConnected(); }
        }

        public bool isLoggedIn
        {
            get { return _stratClient.IsLoggedIn(); }
        }


        public OrderManager()
        {
            try
            {
                _stratClient = new StratClient();

                _orderCheckTimer = new Timer(new TimerCallback((state) =>
                {
                    if (!isOrderExecutionThreadRunning && _numOrdersQueued > 0)
                        ThreadPool.QueueUserWorkItem(new WaitCallback(OrderExecutorFunction), isOrderExecutionThreadRunning);
                }));
                
                _thisOrderManager = this;

                _executedOrderDictionary = new ConcurrentDictionary<ulong, KeyValuePair<Guid, OrderInfo>>();
                _waitingForOrderIDList = new LinkedList<KeyValuePair<Guid, OrderInfo>>();
                 _unsentOrderQueue = new ConcurrentQueue<KeyValuePair<Guid, OrderInfo>>();

                _stratClient.RegisterCmdOrdHndlr(HandleNewOrd, HandleOpenOrd,
                    HandleOrdFill, HandleOrdReject, HandleOrdCancel, null, null, null, null, null); //may need to assign more of these handlers further down the road..

                OnOrderRouterReconnected += OrderManager_OnOrderRouterReconnected;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Order entry initialization problem: " + ex.Message);
            }
        }

        private void OrderManager_OnOrderRouterReconnected()
        {
            if(TryOrderServerLogin())
            {
                Console.WriteLine("Successfuly reconnected and re-logged in");
            }
            else
            {
                Console.WriteLine("Reconnection login failed");
            }
        }

        public bool ConnectToServer()
        {
            try
            {
                var stratClientIPAddress = new IPEndPoint(IPAddress.Parse(Host68), SA_PORT);

                _stratClient.Connect(IPAddress.Parse(Host68).ToString(), SA_PORT);


                Console.WriteLine("Connected to server on " + Thread.CurrentThread.Name);

                _stratClient.RegisterDisconnectedHndlr(LocalDisconnectedHndlr);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Order router connection failed: " + e.Message);
                return false;
            }
        }

        public void DisconnectFromServer()
        {
            _stratClient.Disconnect();
        }

        private async void TryReconnecting()
        {
            Console.WriteLine("Attempting to reconnect to the order router server...");
            if (!isConnected)
            {
                try
                {
                    await Task.Run(() =>
                    {

                        bool isConnected = false;
                        while (!isConnected)
                        {
                            Thread.Sleep(5000);
                            isConnected = ConnectToServer();
                        }

                    });
                    
                    if (OnOrderRouterReconnected != null)
                        OnOrderRouterReconnected.Invoke();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reconnection handler threw exception: " + e.Message);
                }
            }
        }

        


        public void RegisterLoginHandlers(StratLoginOkHndlr loginSuccessHandler, StratLoginFailedHndlr loginFailedHandler)
        {
            _stratClient.RegisterLoginHndlrs(loginSuccessHandler, loginFailedHandler);
        }
        
        public void RegisterDisconnectHandler(DisconnectedHndlr handler)
        {
            if (OnOrderRouterDisconnected != null) 
                OnOrderRouterDisconnected.Invoke();
                _stratClient.RegisterDisconnectedHndlr(handler);
            
        }

        private void LocalDisconnectedHndlr(System.Net.Sockets.Socket s)
        {
            TryReconnecting();
        }

        public void RegisterConnectionFailedHandler(ConnectFailedHndlr handler)
        {
            _stratClient.RegisterConnectFailedHndlr(handler);
        }

        public void RegisterConnectionSucceededHandler(ConnectedHndlr handler)
        {
            _stratClient.RegisterConnectedHndlr(handler);
        }

        public bool TryOrderServerLogin(string username, string password)
        {
            _password = password;
            _username = username;
            if (isConnected)
            {
                _stratClient?.RegisterLogin(true);
                _stratClient?.Login(username, password);
            }
            else
                return false;
            
            return isLoggedIn;
        }

        private bool TryOrderServerLogin()
        {
            if (isConnected)
            {
                _stratClient?.RegisterLogin(true);
                _stratClient?.Login(_username, _password);
            }
            else
                return false;

            return isLoggedIn;
        }

        
        private void PlaceOrder(OrderInfo order)
        {
            // parse the value
            uint size = Convert.ToUInt32(order.qty);
            float price = Convert.ToSingle(order.prc);
            byte type = Convert.ToByte(order.type);
            byte tif = Convert.ToByte(order.tif);
            byte side = Convert.ToByte(order.side);
            byte exch = order.exchByte;
            bool clsopn =  order.isClosingOrder;
            Console.WriteLine("Order placed " + Thread.CurrentThread.Name);
            Console.WriteLine(order.ToString() + "  " + order.instruments.Length + "   " + order.instruments[0].type.ToString() + "    " + price);

            if(isConnected && isLoggedIn)
                _stratClient.OrdNew(order.instruments, type, tif, price, size, side, exch, clsopn);
        }

        private void HandleNewOrd(ulong oid, InstrInfo[] instr, byte ordtype,
            byte ordtif, float ordprc, uint ordsz,
            byte ordside, string dest, bool contra)
        {

            OrderInfo newOrder = new OrderInfo(oid, instr, ordtype, ordtif, ordprc, ordsz, ordside, dest, contra);
            newOrder.status = OrderStatus.PendingNew;
            newOrder.remainingQuantity = ordsz;
            if(matchUnidentifiedOrderToControl(oid, newOrder))
            {
                //is clone needed?
                //OnNewOrderAccepted.BeginInvoke(_executedOrderDictionary[oid].Value, _executedOrderDictionary[oid].Key, null, null);
            }
            
        }

        //returns true if the operation is successful
        private bool matchUnidentifiedOrderToControl(ulong oid, OrderInfo order)
        {
            KeyValuePair<Guid, OrderInfo> tempItem = new KeyValuePair<Guid, OrderInfo>();
            int listLength = _waitingForOrderIDList.Count;
            
            if(listLength == 0)
            {
                Console.WriteLine("Problem with matcher...");
                return false;
            }

            //go through the list, starting with the oldest orders
            
            for (int i=0; i<listLength; i++)
            {
                    tempItem = _waitingForOrderIDList.ElementAt<KeyValuePair<Guid, OrderInfo>>(i);

                Console.WriteLine(tempItem.Value.ToString() + "        " + order.ToString());
                if(tempItem.Value.ToString() == order.ToString())
                {
                    //match!
                    lock(_waitingForOrderIDList)
                        _waitingForOrderIDList.Remove(tempItem);
                    break;
                }
                else if(i == listLength-1)
                {
                    //no match
                    Console.WriteLine("No match in order manager matcher : (  something's wrong...");
                    return false;
                }
            }

            tempItem.Value.orderID = oid;
            Console.WriteLine("HandleNewOrd");
            if (!_executedOrderDictionary.TryAdd(oid, tempItem))
            {
                Console.WriteLine("FAILURE TO ADD ORDER!!! ID = " + oid);
                return false;
            }
            else
                Console.WriteLine("Order matched with control");
            return true;
        }

        private void HandleOpenOrd(ulong oid)
        {
            Console.WriteLine("HandleOpenOrd " + Thread.CurrentThread.Name);
            
            lock(_executedOrderDictionary[oid].Value)
            {
                _executedOrderDictionary[oid].Value.status = OrderStatus.New;
                _executedOrderDictionary[oid].Value.remainingQuantity = _executedOrderDictionary[oid].Value.qty;
            }
            
            OnOrderStatusChanged.Invoke(_executedOrderDictionary[oid].Value, _executedOrderDictionary[oid].Key);
        }

        private void HandleOrdFill(ulong oid, uint totqty, uint exeqty, uint remqty, uint fillqty, float prc)
        {
            Console.WriteLine("HandleOpenFill");
            lock (_executedOrderDictionary[oid].Value)
            {
                _executedOrderDictionary[oid].Value.status =  remqty > 0 ? OrderStatus.Partial : OrderStatus.Filled;

                _executedOrderDictionary[oid].Value.qty = totqty;
                _executedOrderDictionary[oid].Value.remainingQuantity = remqty;
                _executedOrderDictionary[oid].Value.fillQuantity = exeqty;
                _executedOrderDictionary[oid].Value.lastQtyExecuted = fillqty;
                _executedOrderDictionary[oid].Value.prc = prc;
                _executedOrderDictionary[oid].Value.partialFillHistory.Add(new KeyValuePair<DateTime, uint>(DateTime.Now, fillqty));
                _executedOrderDictionary[oid].Value.LastExecTS = DateTime.Now;
            }
            OnOrderStatusChanged.Invoke(_executedOrderDictionary[oid].Value, _executedOrderDictionary[oid].Key);
        }
        private void HandleOrdReject(ulong oid, string err)
        {
            Console.WriteLine("HandleOrdReject: " + err);
            lock(_executedOrderDictionary[oid].Value)
            {
                _executedOrderDictionary[oid].Value.status = OrderStatus.Rejected;
                _executedOrderDictionary[oid].Value.Error = err;
            }
            OnOrderStatusChanged.Invoke(_executedOrderDictionary[oid].Value, _executedOrderDictionary[oid].Key);
        }

        private void HandleOrdCancel(ulong oid)
        {
            Console.WriteLine("HandleOrdCancel");
            lock(_executedOrderDictionary[oid].Value)
                _executedOrderDictionary[oid].Value.status = OrderStatus.Cancelled;
            OnOrderStatusChanged.Invoke(_executedOrderDictionary[oid].Value, _executedOrderDictionary[oid].Key);
        }

        private void HandleOrdAmend(ulong oid, ulong newoid, float prc, uint qty)
        {
            Console.WriteLine("HandleOrdAmend");
            KeyValuePair<Guid, OrderInfo> oldOrder;
             
            if (_executedOrderDictionary.TryRemove(oid, out oldOrder))
            {
                oldOrder.Value.prc = prc;
                oldOrder.Value.qty = qty;
                oldOrder.Value.orderID = newoid;
                
                if (_executedOrderDictionary.TryAdd(newoid, oldOrder))
                {
                    OnOrderStatusChanged.Invoke(_executedOrderDictionary[oid].Value, _executedOrderDictionary[oid].Key);
                    return;
                }
                else
                    Console.WriteLine("ERROR AMENDING ORDER: SAME KEY ALREADY EXISTS");
            }
            else
                Console.WriteLine("ERROR AMENDING ORDER: ORIGINAL ORDER DID NOT EXIST.");
        }
        
        public void CancelOrder(ulong OrderID)
        {
            if (isLoggedIn && isConnected)
            {
                //containskey is OK concurrency-wise, because no thread will ever remove orders from the dictionary
                if (_executedOrderDictionary.ContainsKey(OrderID)) 
                {
                    lock(_executedOrderDictionary[OrderID].Value)
                        _executedOrderDictionary[OrderID].Value.status = OrderStatus.PendingCancel;
                    OnOrderStatusChanged.Invoke(_executedOrderDictionary[OrderID].Value, _executedOrderDictionary[OrderID].Key);
                    _stratClient.OrdCancel(OrderID);
                }
                else
                    Console.WriteLine("Could not cancel, orderid not present in dictionary");
            }
        }
        
        public void NewOrderSubmitted(OrderInfo order, Guid ownerGuid)
        {
            Console.WriteLine("Order submitted " + Thread.CurrentThread.Name);
            _unsentOrderQueue.Enqueue(new KeyValuePair<Guid, OrderInfo>(ownerGuid, order));
            
            try
            {
                if(!isOrderExecutionThreadRunning)
                    ThreadPool.QueueUserWorkItem(new WaitCallback(OrderExecutorFunction), isOrderExecutionThreadRunning);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
        }

        //Designed to execute order batches very quickly, and then to turn off
        int executorTimeOutMilliseconds = 5000;
        volatile bool isOrderExecutionThreadRunning = false;
        private async void OrderExecutorFunction(object state)
        {
            bool isThreadRunning = (bool)state;
            isThreadRunning = true;
                Console.WriteLine("Order Executor Started in " + Thread.CurrentThread.Name);
                Stopwatch watch = new Stopwatch();
                watch.Start();

                while (watch.ElapsedMilliseconds <= executorTimeOutMilliseconds)
                {
                    while (_unsentOrderQueue.Count > 0)
                    {
                        try
                        {
                            if (isConnected && isLoggedIn)
                            {
                                KeyValuePair<Guid, OrderInfo> newOrder;
                                if (_unsentOrderQueue.TryDequeue(out newOrder))
                                {

                                    lock (_thisOrderManager._waitingForOrderIDList)
                                        _thisOrderManager._waitingForOrderIDList.AddLast(newOrder);

                                    _thisOrderManager.PlaceOrder(newOrder.Value);


                                }

                                    watch.Restart();
                                
                            }
                            else
                                Thread.Sleep(1000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Order execution error 0.0");
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1);

                        if (watch.ElapsedMilliseconds >= executorTimeOutMilliseconds)
                            break;
                    }
                    Thread.Sleep(25);
                }
            isThreadRunning = false;
        }
        
    }
}
