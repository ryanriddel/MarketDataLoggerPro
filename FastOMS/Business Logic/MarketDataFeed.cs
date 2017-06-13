using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraPivotGrid;
using System.Threading;
using System.IO;
using MktSrvcAPI;
using System.Net.Sockets;
using System.Collections.Concurrent;

using System.Timers;
using FastOMS.Data_Structures;
using FastOMS.Utility;
using System.Threading.Tasks;

namespace FastOMS
{
    public class MarketDataFeed : IMarketDataProducer
    {
        public delegate void FeedHandlerReconnectedHandler();
        public event FeedHandlerReconnectedHandler OnFeedHandlerReconnected;

        public delegate void FeedHandlerDisconnectedHandler();
        public event FeedHandlerDisconnectedHandler OnFeedHandlerDisconnected;

        public Thread marketDataThread;

        QuoteFeed _quoteFeed = new QuoteFeed();
        TradeFeed _tradeFeed = new TradeFeed();
        FeedHandler _feedHandler;
        Thread _feedHandlerThread;

        ConcurrentDictionary<string, List<IMarketDataConsumer<QuoteBook>>> _quoteSubsciberDictionary = 
            new ConcurrentDictionary<string, List<IMarketDataConsumer<QuoteBook>>>();

        ConcurrentDictionary<string, List<IMarketDataConsumer<TradeInfo>>> _tradeSubsciberDictionary = 
            new ConcurrentDictionary<string, List<IMarketDataConsumer<TradeInfo>>>();

        ConcurrentDictionary<string, QuoteBook> _spreadInitialQuotes = new ConcurrentDictionary<string, QuoteBook>();

        private int _carouselSize = 50;
        private QuoteBookStruct[] _quoteCarousel;
        private int _quoteCarouselIndex = 0;

        private TradeInfoStruct[] _tradeCarousel;
        private int _tradeCarouselIndex = 0;

        public volatile bool _isConnected = false;
        public bool _feedHandlerEnabled = true;
        public bool enableReconnecting = false;
        
        public MarketDataFeed()
        {

            marketDataThread = Thread.CurrentThread;

            

            _quoteCarousel = new QuoteBookStruct[_carouselSize];
            _tradeCarousel = new TradeInfoStruct[_carouselSize];

            _feedHandler = new FeedHandler();
            try
            {
                _feedHandler.InitializeClients();
                _feedHandler.OnFeedHandlerDisconnected += FeedConnectionLostHandler;
                ConnectToFeed();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in market data feed: " + e.ToString());
            }
            /*
            _feedHandlerThread = new Thread(() =>
            {
                _feedHandler = new FeedHandler();
                feedHandlerCallback();
            });
            _feedHandlerThread.SetApartmentState(ApartmentState.MTA);
            _feedHandlerThread.Name = "Feed Handler Thread";
            _feedHandlerThread.Priority = ThreadPriority.AboveNormal;
            _feedHandlerThread.IsBackground = true;
            _feedHandlerThread.Start();*/

        }

        void feedHandlerCallback()
        {
            _feedHandler.InitializeClients();
            _feedHandler.OnFeedHandlerDisconnected += FeedConnectionLostHandler;

        }
        
        public bool ConnectToFeed()
        {
            try
            {
                if (!_feedHandler.ConnectToData())
                {
                    Console.WriteLine("Could not connect to market data feed");
                    _feedHandlerEnabled = false;
                    _isConnected = false;

                    TryReconnecting();
                    return false;
                }
                else
                {
                    Console.WriteLine("Connected to market data feed");
                    _isConnected = true;
                    _feedHandlerEnabled = true;
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Connection error: " + e.ToString());
                return false;
            }
        }
        

        private void FeedConnectionLostHandler()
        {
            _feedHandlerEnabled = false;
            try
            {
                if (_isConnected)
                {
                    //this signifies a reconnection
                    _isConnected = false;

                    if (OnFeedHandlerDisconnected != null)
                        OnFeedHandlerDisconnected.Invoke();
                    if(enableReconnecting)
                        TryReconnecting();
                }
                else
                {
                    //do nothing, for now
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private async void TryReconnecting()
        {
            Console.WriteLine("Attempting reconnection");
            if (true)
            {
                try
                {
                    await Task.Run(() =>
                   {

                       bool isConnected = false;
                       while (!isConnected)
                       {
                           Thread.Sleep(5000);
                           isConnected = _feedHandler.ConnectToData();
                       }

                   });

                    _isConnected = true;
                    _feedHandlerEnabled = true;
                    if(OnFeedHandlerReconnected != null)
                        OnFeedHandlerReconnected.Invoke();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Reconnection handler threw exception: " + e.Message);
                    _isConnected = false;
                }


            }
        }

        public void DisconnectFromFeed()
        {
            try
            {
                _feedHandlerEnabled = false;
                _feedHandler.DisconnectLevel2Data();
                _isConnected = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to disconnect from market data feed connection.  " + ex.Message);
            }
        }

        public void GetSpreadQuote(InstrInfo[] instruments, IMarketDataConsumer<QuoteBook> cons)
        {
            if (instruments.Length > 1)
            {
                Console.WriteLine("Getting spread quote...");
                FastOMS.Data_Structures.QuoteBook qb = new Data_Structures.QuoteBook();

                while (!_spreadInitialQuotes.TryGetValue(Utilities.InstrToStr(instruments), out qb))
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("waiting for spread quote");
                }

                cons.NewDataHandler(qb);

                
            }

        }


        public void getLastSpreadQuote(InstrInfo[] instruments, IMarketDataConsumer<QuoteBook> cons)
        {
            Console.WriteLine(_spreadInitialQuotes.Keys.Count);


            ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
            {
                GetSpreadQuote(instruments, cons);

            }), null);
            
        }
        public void UnsubscribeFromInstrumentQuotes(InstrInfo[] instrument)
        {
            _feedHandler.UnsubscribeFromSymbolQuotes(instrument);
        }
        public void UnsubscribeFromInstrumentTrades(InstrInfo[] instrument)
        {
            _feedHandler.UnsubscribeFromSymbolTrades(instrument);
        }

        public void AddQuote(ref QuoteBookStruct quote)
        {
            _quoteFeed.AddQuote(ref quote);
        }

        public void AddTrade(ref TradeInfoStruct trade)
        {
            _tradeFeed.AddTrade(ref trade);
        }
        
        private object t = new object();
        public bool AddQuoteConsumer(InstrInfo[] instruments, IMarketDataConsumer<QuoteBook> consumer)
        {
            Console.WriteLine("add quote consumer: " + Utilities.InstrToStr(instruments));
            string instrumentsString = Utilities.InstrToStr(instruments);
            
                if (_quoteSubsciberDictionary.ContainsKey(instrumentsString))
                {
                    //this implies that a dataconsumer has alread subscribed, and thus that we are
                    //already subscribed to this quote's feed
                    if (_quoteSubsciberDictionary[instrumentsString].Contains(consumer))
                        return false;
                    else
                        _quoteSubsciberDictionary[instrumentsString].Add(consumer);
                }
                else
                {
                
                    //first time we are hearing about this instrument
                    _quoteSubsciberDictionary[instrumentsString] = new List<IMarketDataConsumer<QuoteBook>>(5);
                    _quoteSubsciberDictionary[instrumentsString].Add(consumer);
                    
                    if (instruments.Length > 1)
                        SubscribeToSpreadQuoteFeed(instruments);
                    else
                        SubscribeToQuoteFeed(instruments);
                }
            
            
            return _quoteFeed.AddQuoteConsumer(instrumentsString, consumer);
            
        }

        public void RemoveQuoteConsumer(InstrInfo[] instruments, IMarketDataConsumer<QuoteBook> consumer)
        {
            string instrumentsString = Utilities.InstrToStr(instruments);

            if (_quoteSubsciberDictionary.ContainsKey(instrumentsString))
            {
                //this implies that a dataconsumer has alread subscribed, and thus that we are
                //already subscribed to this quote's feed
                if (_quoteSubsciberDictionary[instrumentsString].Contains(consumer))
                {
                    _quoteSubsciberDictionary[instrumentsString].Remove(consumer);
                    if (_quoteSubsciberDictionary[instrumentsString].Count == 0)
                    {
                        //UnsubscribeFromInstrumentQuotes(instruments);
                        _quoteFeed._quoteFeedBuffers[instrumentsString].ConsumerUnsubscribe(consumer);
                    }
                }
            }
            else
            {
                return;
            }
        }

        private object p = new object();
        public bool AddTradeConsumer(InstrInfo[] instruments, IMarketDataConsumer<TradeInfo> consumer)
        {
            string instrumentsString = Utilities.InstrToStr(instruments);
            
                if (_tradeSubsciberDictionary.ContainsKey(instrumentsString))
                {
                    //this implies that a dataconsumer has alread subscribed, and thus that we are
                    //already subscribed to this quote's feed
                    if (_tradeSubsciberDictionary[instrumentsString].Contains(consumer))
                        return false;
                    else
                        _tradeSubsciberDictionary[instrumentsString].Add(consumer);
                }
                else
                {
                
                    //first time we are hearing about this instrument
                    _tradeSubsciberDictionary[instrumentsString] = new List<IMarketDataConsumer<TradeInfo>>(5);
                    _tradeSubsciberDictionary[instrumentsString].Add(consumer);

                    SubscribeToTradeFeed(instruments);
                }
            
            
            return _tradeFeed.AddTradeConsumer(instrumentsString, consumer);
        }

        public void RemoveTradeConsumer(InstrInfo[] instruments, IMarketDataConsumer<TradeInfo> consumer)
        {
            string instrumentsString = Utilities.InstrToStr(instruments);

            if (_tradeSubsciberDictionary.ContainsKey(instrumentsString))
            {
                //this implies that a dataconsumer has alread subscribed, and thus that we are
                //already subscribed to this quote's feed
                if (_tradeSubsciberDictionary[instrumentsString].Contains(consumer))
                {
                    _tradeSubsciberDictionary[instrumentsString].Remove(consumer);
                    if (_quoteSubsciberDictionary[instrumentsString].Count == 0)
                    {
                        //UnsubscribeFromInstrumentTrades(instruments);
                        _tradeFeed._tradeFeedBuffers[instrumentsString].ConsumerUnsubscribe(consumer);
                    }
                }
            }
            else
            {
                return;
            }
        }

        public bool SubscribeToSpreadQuoteFeed(InstrInfo[] instruments)
        {
            
            _feedHandler.SubscribeToSymbolQuoteFeed(instruments, SpreadDepthOfBkHndlr);
            return _isConnected;
        }

        public bool SubscribeToQuoteFeed(InstrInfo[] instruments)
        {
            if (_isConnected)
            {
                _feedHandler.SubscribeToSymbolQuoteFeed(instruments, DepthOfBkHndlr);
                Console.WriteLine("Subscribe to " + Utilities.InstrToStr(instruments));
                    }
            return _isConnected;
        }

        public bool SubscribeToTradeFeed(InstrInfo[] instruments)
        {
            if (_isConnected)
                _feedHandler.SubscribeToSymbolTradeFeed(instruments, LastTrdHndlr);

            return _isConnected;
        }

        //Used for testing
        public void FeedQuoteToHandler(QuoteBook quote)
        {
            DepthOfBkHndlr(quote.Instr, quote.TS, quote.PartID, quote.Mod, quote.NumBid, quote.NumAsk, quote.BidExch, quote.AskExch, quote.BidBk, quote.AskBk);
        }

        //Used for testing
        public void FeedTradeToHandler(TradeInfo trade)
        {
            LastTrdHndlr(trade.Instr, trade.TS, trade.PartID, trade.Prc, trade.Sz, trade.CondNum, trade.Cond, trade.NBBOBidPrc, trade.NBBOAskPrc, trade.NBBOBidSz, trade.NBBOAskSz, trade.BidExch, trade.AskExch, trade.High, trade.Low, trade.Open, trade.TotVol);
        }

        private void DepthOfBkHndlr(InstrInfo[] instr, uint ts, byte partid, int mod, byte numbid, byte numask, byte[] bidexch, byte[] askexch, QuoteInfo[] bidbk, QuoteInfo[] askbk)
        {
            if (instr == null)
                return;

            if (instr.Length == 0 || ts == 0)
                return;

            if (askexch == null || askbk == null )
                return;

            if (_feedHandlerEnabled)
            {
                _quoteCarousel[_quoteCarouselIndex].PopulateFields(
                    ref instr,
                    ref ts,
                    ref partid,
                    ref mod,
                    ref numbid,
                    ref numask,
                    ref bidexch,
                    ref askexch,
                    ref bidbk,
                    ref askbk);
                AddQuote(ref _quoteCarousel[_quoteCarouselIndex]);

                if (_quoteCarouselIndex == _carouselSize - 2)
                    _quoteCarouselIndex = 0;
                else
                    _quoteCarouselIndex++;
            }
        }

        private void SpreadDepthOfBkHndlr(InstrInfo[] instr, uint ts, byte partid, int mod, byte numbid, byte numask, byte[] bidexch, byte[] askexch, QuoteInfo[] bidbk, QuoteInfo[] askbk)
        {

            Console.WriteLine("SPREAD**************************************************************************!");

            if (instr == null)
                return;

            if (instr.Length == 0 || ts == 0)
                return;

            //if (bidexch == null || askexch == null || askbk == null || bidbk == null)
            // return;
            try
            {
                Console.WriteLine("Spread try");
                if (_feedHandlerEnabled)
                {
                    _quoteCarousel[_quoteCarouselIndex].PopulateFields(
                        ref instr,
                        ref ts,
                        ref partid,
                        ref mod,
                        ref numbid,
                        ref numask,
                        ref bidexch,
                        ref askexch,
                        ref bidbk,
                        ref askbk);
                    AddQuote(ref _quoteCarousel[_quoteCarouselIndex]);

                    QuoteBook book = new QuoteBook();
                    book.Update(ref _quoteCarousel[_quoteCarouselIndex]);
                    if (!_spreadInitialQuotes.TryAdd(Utilities.InstrToStr(instr), book))
                        Console.WriteLine("QUOTE NOT ADDED TO DICT");

                    if (_quoteCarouselIndex == _carouselSize - 2)
                        _quoteCarouselIndex = 0;
                    else
                        _quoteCarouselIndex++;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }

        private void LastTrdHndlr(InstrInfo[] instr, uint ts, byte partid, float prc, uint sz, byte condnum, byte[] cond, float nbbobidprc, float nbboaskprc, uint nbbobidsz, uint nbboasksz, string bidexch, string askexch, float high, float low, float open, uint totvol)
        {
            if (instr.Length == 0 || ts == 0)
                return;
            
            if (_feedHandlerEnabled)
            {
                _tradeCarousel[_tradeCarouselIndex].PopulateFields(
                    ref instr,
                    ref ts,
                    ref partid,
                    ref prc,
                    ref sz,
                    ref condnum,
                    ref cond,
                    ref nbbobidprc,
                    ref nbboaskprc,
                    ref nbbobidsz,
                    ref nbboasksz,
                    ref bidexch,
                    ref askexch,
                    ref high,
                    ref low,
                    ref open,
                    ref totvol);

                AddTrade(ref _tradeCarousel[_tradeCarouselIndex]);

                if (_tradeCarouselIndex == _carouselSize-2)
                    _tradeCarouselIndex = 0;
                else
                    _tradeCarouselIndex++;
            }
        }
    }

    public class FeedHandler
    {
        public delegate void FeedHandlerDisconnectedHandler();
        public event FeedHandlerDisconnectedHandler OnFeedHandlerDisconnected;

        #region DATA_FEED_MEMBER_VARIABLES

        private Parsers parsers = new Parsers();

        private DepthOfBkClient optDobkClient = null;
        private DepthOfBkClient sprdDobkClient = null;
        private DepthOfBkClient eqDobkClient = null;

        private TradeClient optLastTrdClient = null;
        private TradeClient eqLastTrdClient = null;
        private TradeClient sprdLastTrdClient = null;

        private static string host = "172.20.168.71";
        string Host68 = "172.20.168.68";
        string Host69 = "172.20.168.69";

        private static int EQ_QUOTE_PORT = 12000;
        private static int OPT_QUOTE_PORT = 13000;
        private static int OPT_TRADE_PORT = 14000;
        private static int EQ_TRADE_PORT = 15000;
        private static int ISE_SPREAD_QUOTE_PORT = 33000;

        private static int quotes = 0;
        private static int trades = 0;

        private static int YEAR;
        private static int MONTH;
        private static int DAY;
        #endregion

        public void ConnectionLostHandler()
        {
            try
            {
                if(OnFeedHandlerDisconnected != null)
                    OnFeedHandlerDisconnected.Invoke();

            }
            catch(Exception e)
            {
                Console.WriteLine("Connection Lost Handler had a problem disconnecting: " + e.Message);
            }
        }

        #region DATA_FEED_MEMBER_FUNCTIONS
        Thread feedHandlerThread;
        public void InitializeClients()
        {
            try
            {
                InitializeDOBKClients();
                InitializeTradeClients();
            }
            catch(Exception e)
            {
                Console.WriteLine("Initialization error: " + e.ToString());
            }
        }

        public volatile bool IsTryingToReconnect = false;
        public bool ConnectToData()
        {
            try
            {
                ConnectLevel2Data();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public FeedHandler()
        {
            feedHandlerThread = Thread.CurrentThread;
            Console.WriteLine("Feed handler initialized on " + feedHandlerThread.Name);
        }

        public void SubscribeToSymbolQuoteFeed(InstrInfo[] instr, DepthOfBkHndlr handler)
        {
            Console.WriteLine("Subscribe To Symbol Feed: " + Utilities.InstrToStr(instr));
            try
            {
                if (instr[0].type == InstrInfo.EType.EQUITY)
                {
                    eqDobkClient.Subscribe(instr, handler);

                }
                else if (instr[0].type == InstrInfo.EType.OPTION)
                {
                    if (instr.Length > 1)
                        sprdDobkClient.Subscribe(instr, handler);
                    else
                        optDobkClient.Subscribe(instr, handler);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Quote subscription error: " + ex.Message);
            }

        }

        public void SubscribeToSpreadQuoteFeed(InstrInfo[] instr, DepthOfBkHndlr handler)
        {

            try
            {
                if (instr[0].type == InstrInfo.EType.OPTION)
                {
                    optDobkClient.Subscribe(instr, handler);
                }
                else
                    throw new Exception("Spreads must be composed of options.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Quote subscription error: " + ex.Message);
            }

        }

        public void UnsubscribeFromSymbolQuotes(InstrInfo[] instr)
        {
            if (instr[0].type == InstrInfo.EType.EQUITY)
            {
                eqDobkClient.Unsubscribe(instr);
                

            }
            else if (instr[0].type == InstrInfo.EType.OPTION)
            {
                optDobkClient.Unsubscribe(instr);
                
            }
        }

        public void UnsubscribeFromSymbolTrades(InstrInfo[] instr)
        {
            if (instr[0].type == InstrInfo.EType.EQUITY)
            {
                eqLastTrdClient.Unsubscribe(instr);


            }
            else if (instr[0].type == InstrInfo.EType.OPTION)
            {
                optLastTrdClient.Unsubscribe(instr);

            }
        }


        public void SubscribeToSymbolTradeFeed(InstrInfo[] instr, LastTrdHndlr handler)
        {
            try
            {
                if (instr[0].type == InstrInfo.EType.EQUITY)
                {
                    eqLastTrdClient.Subscribe(instr, handler);

                }
                else if (instr[0].type == InstrInfo.EType.OPTION)
                {
                    
                    optLastTrdClient.Subscribe(instr, handler);

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Trade subscription error: " + e.Message);
            }

        }


        #region DATA_FEED_CONNECTION_TERMINATORS
        public void DisconnectLevel2Data()
        {
            try
            {
                if (eqLastTrdClient.IsConnected())
                {
                    eqLastTrdClient.Disconnect();
                }
                if (eqDobkClient.IsConnected())
                {
                    eqDobkClient.Disconnect();
                }
                if (optLastTrdClient.IsConnected())
                {
                    optLastTrdClient.Disconnect();
                }
                if (optDobkClient.IsConnected())
                {
                    optDobkClient.Disconnect();
                }
                if (sprdDobkClient.IsConnected())
                    sprdDobkClient.Disconnect();
                if (sprdLastTrdClient.IsConnected())
                    sprdLastTrdClient.Disconnect();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
        #region DATA_FEED_CONNECTION_INITIALIZERS
        private void InitializeTradeClients()
        {
            optLastTrdClient = new TradeClient();

            optLastTrdClient.RegisterSessHndlrs(optLastTrdClient_HandleErr, optLastTrdClient_HandleConnectionFailed,
                                        optLastTrdClient_HandleConnected, optLastTrdClient_HandleDisconnected);

            eqLastTrdClient = new TradeClient();
            eqLastTrdClient.RegisterSessHndlrs(eqLastTrdClient_HandleErr, eqLastTrdClient_HandleConnectionFailed,
                                        eqLastTrdClient_HandleConnected, eqLastTrdClient_HandleDisconnected);

            sprdLastTrdClient = new TradeClient();
            sprdLastTrdClient.RegisterSessHndlrs(sprdLastTrdClient_HandleErr, sprdLastTrdClient_HandleConnectionFailed,
                                        sprdLastTrdClient_HandleConnected, sprdLastTrdClient_HandleDisconnected);
        }

        private void InitializeDOBKClients()
        {
            optDobkClient = new DepthOfBkClient();
            optDobkClient.RegisterSessHndlrs(optDobkClient_HandleErr, optDobkClient_HandleConnectionFailed,
                                        optDobkClient_HandleConnected, optDobkClient_HandleDisconnected);


            eqDobkClient = new DepthOfBkClient();
            eqDobkClient.RegisterSessHndlrs(eqDobkClient_HandleErr, eqDobkClient_HandleConnectionFailed,
                                        eqDobkClient_HandleConnected, eqDobkClient_HandleDisconnected);

            sprdDobkClient = new DepthOfBkClient();
            sprdDobkClient.RegisterSessHndlrs(sDBClient_HandleErr, sDBClient_HandleConnectionFailed,
                                        sDBClient_HandleConnected, sDBClient_HandleDisconnected);
        }

        private void ConnectLevel2Data()
        {
            try
            {
                optLastTrdClient.Connect(host, OPT_TRADE_PORT);
                eqLastTrdClient.Connect(host, EQ_TRADE_PORT);
                optDobkClient.Connect(host, OPT_QUOTE_PORT);
                eqDobkClient.Connect(host, EQ_QUOTE_PORT);
                sprdDobkClient.Connect(host, ISE_SPREAD_QUOTE_PORT);
            }
            catch(Exception e)
            {
                Console.WriteLine("ConnectLevel2Data error: " + e.ToString());
            }
            
        }
        #endregion
        #region DATA_FEED_CONNECTION_HANDLERS
        private void eqDobkClient_HandleErr(string errstr)
        {
            Console.WriteLine("Eqdobk error" + errstr);
        }
        private void eqDobkClient_HandleConnectionFailed(Socket s)
        {
            Console.WriteLine("EqDobk Handle connection failed");
        }
        private void eqDobkClient_HandleConnected(Socket s)
        {
            Console.WriteLine("Equity Quote Feed Connected");
            //InstrInfo instr = new InstrInfo();
            //instr.sym = "SPY";
            //instr.type = InstrInfo.EType.EQUITY;

            //eqDobkClient.Subscribe(instr, this.DepthOfBkHndlr);

            //logMngr.LogMsg("EqDobkClient connection established[" + eqDobkClient.Host + ":" + eqDobkClient.Port.ToString() + "]", LogMngr.LogType.INFO);
        }

        private void eqDobkClient_HandleDisconnected(Socket s) { Console.WriteLine("Handle disconnected"); }

        private void optLastTrdClient_HandleErr(string errstr)
        {
            ConnectionLostHandler();
            Console.WriteLine("OptLastTradeClient Error: " + errstr);
        }

        private void optLastTrdClient_HandleConnectionFailed(Socket s)
        {
            Console.WriteLine("Option last trade connection failed");
            ConnectionLostHandler();
        }
        private void optLastTrdClient_HandleConnected(Socket s)
        {

            Console.WriteLine("Option Trade Feed Connected");
            //logMngr.LogMsg("OptLstTrdClient connection established[" + optLastTrdClient.Host + ":" + optLastTrdClient.Port.ToString() + "]", LogMngr.LogType.INFO);
        }

        private void optLastTrdClient_HandleDisconnected(Socket s)
        {

        }

        private void eqLastTrdClient_HandleErr(string errstr)
        {
            ConnectionLostHandler();
            Console.WriteLine("Last Trade Client Handling Error: " + errstr);
        }

        private void eqLastTrdClient_HandleConnectionFailed(Socket s)
        {
            ConnectionLostHandler();
            Console.WriteLine("Equity Trade Client Connection Failed");
        }
        private void eqLastTrdClient_HandleConnected(Socket s)
        {
            Console.WriteLine("Equity Trade Client Handle Connected");
            //InstrInfo instr = new InstrInfo();
            //instr.sym = "SPY";
            //instr.type = InstrInfo.EType.EQUITY;

            //eqLastTrdClient.Subscribe(instr, this.LastTrdHndlr);

            //logMngr.LogMsg("EqLstTrdClient connection established[" + eqLastTrdClient.Host + ":" + eqLastTrdClient.Port.ToString() + "]", LogMngr.LogType.INFO);
        }

        private void eqLastTrdClient_HandleDisconnected(Socket s)
        {

        }

        private void sprdLastTrdClient_HandleErr(string errstr)
        {
            ConnectionLostHandler();
        }

        private void sprdLastTrdClient_HandleConnectionFailed(Socket s)
        {
            ConnectionLostHandler();
        }
        private void sprdLastTrdClient_HandleConnected(Socket s)
        {


            //logMngr.LogMsg("SprdLstTrdClient connection established[" + sprdLastTrdClient.Host + ":" + sprdLastTrdClient.Port.ToString() + "]", LogMngr.LogType.INFO);
        }

        private void sprdLastTrdClient_HandleDisconnected(Socket s)
        {

        }

        private void sDBClient_HandleErr(string errstr)
        {
            ConnectionLostHandler();
            Console.WriteLine("Spread handle error: " + errstr);
        }

        private void sDBClient_HandleConnectionFailed(Socket s)
        {
            ConnectionLostHandler();
            Console.WriteLine("Spread handle error: " + "connection failed.");
        }
        private void sDBClient_HandleConnected(Socket s)
        {
            Console.WriteLine("Spread connection succeeded.");

            //logMngr.LogMsg("SecureDBClient connection established[" + sDBClient.Host + ":" + sDBClient.Port.ToString() + "]", LogMngr.LogType.INFO);
        }

        private void sDBClient_HandleDisconnected(Socket s)
        {
            Console.WriteLine("Spread handler disconnected.");
        }



        private void optDobkClient_HandleErr(string errstr)
        {
            ConnectionLostHandler();
            Console.WriteLine("Option quote feed connection error: " + errstr);
            //logMngr.LogMsg("OptDobkClient Error: " + errstr, LogMngr.LogType.INFO);
        }

        private void optDobkClient_HandleConnectionFailed(Socket s)
        {
            ConnectionLostHandler();
            //logMngr.LogMsg("Check connection params", LogMngr.LogType.INFO);    //  Host, Port failed
        }

        private void optDobkClient_HandleConnected(Socket s)
        {
            Console.WriteLine("Option depth of book handle connected.");
            //logMngr.LogMsg("OptDobkClient connection established[" + optDobkClient.Host + ":" + optDobkClient.Port.ToString() + "]", LogMngr.LogType.INFO);
        }

        private void optDobkClient_HandleDisconnected(Socket s)
        {


            if (!optDobkClient.IsConnected())
            {
                //logMngr.LogMsg("Options depth of book connection[" + optDobkClient.Host + ":" + optDobkClient.Port.ToString() + "] lost", LogMngr.LogType.CRITICAL);
            }
        }
        #endregion

        #endregion
    }


    public class QuoteFeedBuffer : FastBuffer<QuoteBook, QuoteBookStruct>
    {
        string _instrumentName;

        public QuoteFeedBuffer(string instrumentName, int bufferSize = 1024) : base(bufferSize)
        {
            _instrumentName = instrumentName;
        }

        public void AddQuote(ref QuoteBookStruct quote)
        {
            this.Add(ref quote);
        }

    }

    public class TradeFeedBuffer : FastBuffer<TradeInfo, TradeInfoStruct>
    {
        string _instrumentName;

        public TradeFeedBuffer(string instrument, int bufferSize = 1024) : base(bufferSize)
        {
            _instrumentName = instrument;
        }

        public void AddTrade(TradeInfoStruct trade)
        {
            this.Add(ref trade);
        }
    }

    public class QuoteFeed
    {
        public ConcurrentDictionary<string, QuoteFeedBuffer> _quoteFeedBuffers;

        public QuoteFeed()
        {
            _quoteFeedBuffers  = new ConcurrentDictionary<string, QuoteFeedBuffer>();
        }
        public bool AddQuoteBuffer(string instrumentName, int bufferSize = 1024)
        {
            if (!_quoteFeedBuffers.ContainsKey(instrumentName))
            {
                _quoteFeedBuffers[instrumentName] = new QuoteFeedBuffer(instrumentName);
                return true;
            }
            else
                return false;
        }

        public bool AddQuoteConsumer(string instrumentName, IMarketDataConsumer<QuoteBook> consumer)
        {
            if (_quoteFeedBuffers.ContainsKey(instrumentName))
            {
                _quoteFeedBuffers[instrumentName].ConsumerSubscribe(consumer);
                return true;
            }
            else
            {
                bool success = _quoteFeedBuffers.TryAdd(instrumentName, new QuoteFeedBuffer(instrumentName));

               // _quoteFeedBuffers[instrumentName] = new QuoteFeedBuffer(instrumentName);
                _quoteFeedBuffers[instrumentName].ConsumerSubscribe(consumer);
                return false;
            }
        }

        public void AddQuote(ref QuoteBookStruct quote)
        {
            if (_quoteFeedBuffers.ContainsKey(quote.InstrumentName))
                _quoteFeedBuffers[quote.InstrumentName].Add(ref quote);
            else
                return;
        }
        
    }

    public class TradeFeed
    {
        public ConcurrentDictionary<string, TradeFeedBuffer> _tradeFeedBuffers = new ConcurrentDictionary<string, TradeFeedBuffer>();

        public TradeFeed()
        {

        }
        public bool AddTradeBuffer(string instrumentName, int bufferSize = 1024)
        {
            if (!_tradeFeedBuffers.ContainsKey(instrumentName))
            {
                _tradeFeedBuffers[instrumentName] = new TradeFeedBuffer(instrumentName);
                return true;
            }
            else
                return false;
        }

        public bool AddTradeConsumer(string instrumentName, IMarketDataConsumer<TradeInfo> consumer)
        {
            if (_tradeFeedBuffers.ContainsKey(instrumentName))
            {
                _tradeFeedBuffers[instrumentName].ConsumerSubscribe(consumer);
                return true;
            }
            else
            {
                _tradeFeedBuffers[instrumentName] = new TradeFeedBuffer(instrumentName);
                _tradeFeedBuffers[instrumentName].ConsumerSubscribe(consumer);
                return false;
            }
        }

        public void AddTrade(ref TradeInfoStruct trade)
        {
            if (_tradeFeedBuffers.ContainsKey(trade.InstrumentName))
                _tradeFeedBuffers[trade.InstrumentName].Add(ref trade);
            else
                return;
        }
        
    }
}
