using System;
using System.Collections.Generic;
using System.Threading;
using MktSrvcAPI;
using System.Windows.Forms;
using FastOMS.Data_Structures;
using FastOMS.UI;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using FastOMS.UI.Interfaces;

namespace FastOMS
{
    public static class Hub
    {
        public static MarketDataFeed _marketDataFeed;
        public static OrderManager _orderManager;
        public static FormFactory _formFactory;

        static Thread _marketDataThread;
        static Thread _orderManagerThread;
        static Thread _formFactoryThread;
        
        static List<ILayoutSaveLoader> formLayoutList = new List<ILayoutSaveLoader>();
        
        static string previousLoginUsername = "";
        static string previousLoginPassword = "";
        

        static Hub()
        {
            Thread.CurrentThread.Name = "Hub Thread";
        }

        public static void InitializeHub()
        {
            Utilities.log.Info("Initializing Hub...");
            
            InitializeMarketDataFeed();
            InitializeOrderManager();
        }

        #region Market_Data
        private static void InitializeMarketDataFeed()
        {
            _marketDataThread = new Thread(() =>
            {
                Hub._marketDataFeed = new MarketDataFeed();
                
            });
            _marketDataThread.SetApartmentState(ApartmentState.MTA);
            _marketDataThread.Priority = ThreadPriority.AboveNormal;
            _marketDataThread.IsBackground = true;
            _marketDataThread.Name = "Market data thread";
            _marketDataThread.Start();
        }
        
        public static void TESTSUBSCRIBETOSPREAD()
        {
            _marketDataFeed.SubscribeToSpreadQuoteFeed(TestingFunctions.TestSpread);
        }
        
        #endregion

        #region Order_Manager

        public static bool ConnectOrderManager()
        {
            _orderManager.ConnectToServer();
            Thread.Sleep(1);
            return _orderManager.isConnected;  //returning the isConnected immediately after trying to connect probably won't work
        }

        public static bool TryOrderManagerLogin(string username = "test", string password = "test")
        {
            previousLoginUsername = username;
            previousLoginPassword = password;

            return _orderManager.TryOrderServerLogin(username, password);
        }

        public static bool DisconnectOrderManager()
        {
            _orderManager.DisconnectFromServer();

            Thread.Sleep(1);
            return _orderManager.isConnected;
        }

        private static void _orderManagerLoginOKHandler(string _id, bool _admin, string _user)
        {
            Console.WriteLine("Order Server Login Successful.");
        }

        private static void _orderManagerLoginFailedHandler()
        {
            Console.WriteLine("Order Server Login Failed.  Username:" + previousLoginUsername + " and Password:" + previousLoginPassword + " were invalid credentials.");
        }

        private static void _orderManagerConnectionFailedHandler(System.Net.Sockets.Socket s)
        {
            Console.WriteLine("Order server connection failed.");
        }

        private static void _orderManagerConnectionSucceededHandler(System.Net.Sockets.Socket s)
        {
            Console.WriteLine("Order server connection succeeded");
            TryOrderManagerLogin();
        }
        private static void InitializeOrderManager()
        {
            _orderManagerThread = new Thread(() =>
            {
                Hub._orderManager = new OrderManager();
                _orderManager.RegisterLoginHandlers(_orderManagerLoginOKHandler, _orderManagerLoginFailedHandler);
                _orderManager.RegisterConnectionSucceededHandler(_orderManagerConnectionSucceededHandler);
                _orderManager.RegisterConnectionFailedHandler(_orderManagerConnectionFailedHandler);
                _orderManager.ConnectToServer();
            });
            _orderManagerThread.SetApartmentState(ApartmentState.STA);
            _orderManagerThread.Priority = ThreadPriority.Highest;
            _orderManagerThread.Name = "Order manager thread";
            _orderManagerThread.Start();
        }

        #endregion

        
        
        
    }
}
