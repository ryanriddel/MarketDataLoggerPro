using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MktSrvcAPI;
using System.IO;
using System.Threading;
using FastOMS.Data_Structures;

namespace FastOMS
{

    public abstract class Logger<T>
    {
        public static ulong counter = 0;
        public string _pathname;
        public InstrInfo instrument;

        uint bufferSize = 500;
        ulong bufferCounter = 1;
        StringBuilder sBuilder;

        StreamWriter writer;


        public Logger(string pathname, InstrInfo inst)
        {
            instrument = inst;
            _pathname = pathname;
            sBuilder = new StringBuilder();
            writer = new StreamWriter(pathname);
            
        }
        ~Logger()
        {
            writer.Close();
        }

        public abstract string ItemToString(T item);

        public void Log(T item)
        {
            string str = ItemToString(item);
            sBuilder.AppendLine(str);

            if (bufferCounter % bufferSize == 0)
                WriteBuffer();

            bufferCounter++;
            
            
        }


        void WriteBuffer()
        {
            writer.Write(sBuilder.ToString());
            sBuilder.Clear();
        }

        protected void CloseWriter()
        {
            WriteBuffer();
            writer.Close();
        }


    }

    public class QuoteFeedLogger : Logger<QuoteBook>, IMarketDataConsumer<QuoteBook>
    {
        public static List<QuoteFeedLogger> quoteFeedLogger = new List<QuoteFeedLogger>(10);
        volatile bool enableFlag = false;

        public QuoteFeedLogger(string pathname, InstrInfo instr) : base(pathname, instr)
        {
            enableFlag = true;
        }

        public void NewDataHandler(QuoteBook newData)
        {
            if (enableFlag)
            {
                
                Log(newData);
            }

        }

        public override string ItemToString(QuoteBook quote)
        {
            return Utilities.FullQuoteBookToString(quote);
        }

        public void Close()
        {
            enableFlag = false;
            this.CloseWriter();


        }
    }

    public class TradeFeedLogger : Logger<TradeInfo>, IMarketDataConsumer<TradeInfo>
    {
        public static List<TradeFeedLogger> tradeFeedLoggers = new List<TradeFeedLogger>(10);
        volatile bool enableFlag = false;

        public TradeFeedLogger(string pathname, InstrInfo instr) : base(pathname, instr)
        {
            enableFlag = true;
        }

        public void NewDataHandler(TradeInfo newData)
        {
            if (enableFlag)
            { 
                Log(newData);
        }

        public override string ItemToString(TradeInfo trade)
        {
            return Utilities.TradeToString(trade);
        }

        public void Close()
        {
            enableFlag = false;
            this.CloseWriter();
        }
    }

    public static class FeedCollector
    {
        public static string[] bigCapSymbols = { "AAPL", "NVDA", "MU",
            "FET", "ISCA", "QQQ", "MSFT", "SIRI", "CSCO", "BBW", "BEAT",
            "SNAP", "FB", "TSLA", "SPY", "F", "BAC", "RAD", "GM", "KR",
             "DB", "AAON", "NFLX", "CAT", "GS", "BA", "DIS", "GOOG",
            "BIO", "TWN", "BPY", "DECK", "DDD", "FSLR", "GEOS", "CZR",
            "SGA", "ACBI", "AMS", "ANCB", "AMC", "CCF", "CPK", "FGL",
            "GFN", "XLF" };
        public static string[] ryansPicks = {"CMA", "CSV", "EME", "COST", "BAK", "LMT", "WNR"
        , "CRS", "OFC", "UN", "EOCC", "ASML", "HON", "UL", "CL", "EVR", "GVA", "PATK", "WY",
        "EGP", "CE", "HSY", "WUBA", "BIVV", "AMRI" };

        public static string[] blakesPicks = {"QID", "SDS", "TNA", "TZA", "VXX", "UVXY",
            "TBT", "IWM", "RWM", "SPXL", "SPXS", "FAS", "FAZ", "HAKK", "HAKD", "UUP", "UDN", "GLD", "SVXY"};

        public static string[] marksPicks = { "LVS", "WYNN", "BIDU", "MGM", "BLK", "WHR", "TOL",
            "MS", "KO", "EEM", "CVX", "SLB", "PG", "V", "SO", "TLT", "USO", "GDX", "IP", "GE", "CRM",
            "ABT", "T", "NSC", "STZ" };

        public static string[] ryansPicks2 = {"OTEL", "LAQ", "KTYB", "PKO", "FONR", "PWOD", "BXC", "JCAP",
        "NAME", "CHMG", "WLFC", "FCAP", "FGBI", "HNNA", "UNB", "AJX", "PKBK", "CPKF", "PFCF", "ACNB", "SFST",
        "M", "NAP", "PHF", "UCP", "BNED",  "BWG", "CHMI", "PCI", "CVO"};

        static List<string> symbols;
        public static string HistoricalDataPathname;

        public static List<QuoteFeedLogger> quoteFeedLoggers = new List<QuoteFeedLogger>();
        public static List<TradeFeedLogger> tradeFeedLoggers = new List<TradeFeedLogger>();
        static Action<string> updateTextBox1;
        static Action<string> updateTextBox2;

        static CloudManager cloud = new CloudManager();
        static Timer timer;
        static FeedCollector()
        {
            symbols = new List<string>(bigCapSymbols);
            symbols.AddRange(ryansPicks);
            symbols.AddRange(blakesPicks);
            symbols.AddRange(marksPicks);
            symbols.AddRange(ryansPicks2);

            
        }

        public static void setupTextBoxUpdaters(Action<string> func1, Action<string> func2)
        {
            updateTextBox1 = func1;
            updateTextBox2 = func2;
        }

        public static void Begin()
        {
            updateTextBox1("Feed collector started.");
            timer = new Timer(LogManager, isLoggerRunning, 5000, 5000);
            timer.Change(5000, 5000);
            
        }

        public static void updateText1(string arg)
        {
            updateTextBox1?.Invoke(DateTime.Now.ToLocalTime().ToString() + ":" + arg);
        }

        public static void updateText2(string arg)
        {
            updateTextBox1?.Invoke(DateTime.Now.ToLocalTime().ToString() +":" + arg);
        }

        public static void StartLogging()
        { 
            string dateStamp = Utilities.GetDateStamp();
            string desktopPath = AppDomain.CurrentDomain.BaseDirectory;
            System.IO.Directory.CreateDirectory(desktopPath + " \\Historical_Data\\DATA_" + dateStamp);
            Console.WriteLine(desktopPath + "\\Historical_Data" + "\\DATA_" + dateStamp + "\\TRADE_");
            updateTextBox1("Initializing logging...");
            
            for (int i = 0; i < symbols.Count; i++)
            {
                string tradePath = desktopPath + "\\Historical_Data" + "\\DATA_" + dateStamp + "\\TRADE_" + symbols[i] + "_" + dateStamp + ".txt";
                string quotePath = desktopPath + "\\Historical_Data" + "\\DATA_" + dateStamp + "\\QUOTE_" + symbols[i] + "_" + dateStamp + ".txt";

                InstrInfo[] instr = Utilities.OldOMSStringToInstrArray((symbols[i]));

                QuoteFeedLogger quoteLogger = new QuoteFeedLogger(quotePath, instr[0] );
                TradeFeedLogger tradeLogger = new TradeFeedLogger(tradePath, instr[0]);


                try
                {

                    Hub._marketDataFeed.SubscribeToQuoteFeed(instr);
                    Hub._marketDataFeed.SubscribeToTradeFeed(instr);
                    Hub._marketDataFeed.AddQuoteConsumer(instr, quoteLogger);
                    Hub._marketDataFeed.AddTradeConsumer(instr, tradeLogger);

                }
                catch(Exception e)
                {
                    updateTextBox2("Error starting logger: " + e.Message);
                }

                quoteFeedLoggers.Add(quoteLogger);
                tradeFeedLoggers.Add(tradeLogger);
                

             }
            updateTextBox1("Subscribed to " + symbols.Count + " symbols.");
            isLoggerRunning = true;
        }

        public static void DisconnectAndRemoveLoggers()
        {
            try
            {

                for (int i = 0; i < quoteFeedLoggers.Count; i++)
                {
                    int k = i;
                    //Hub._marketDataFeed.UnsubscribeFromInstrument(new InstrInfo[] { quoteFeedLoggers[i].instrument });
                    Hub._marketDataFeed.RemoveQuoteConsumer(new InstrInfo[] { quoteFeedLoggers[i].instrument }, quoteFeedLoggers[i]);
                    Hub._marketDataFeed.UnsubscribeFromInstrumentQuotes(new InstrInfo[] { quoteFeedLoggers[i].instrument });
                    quoteFeedLoggers[i].Close();
                    cloud.UploadMarketDataFile(quoteFeedLoggers[k]._pathname);
                }

                for (int i = 0; i < tradeFeedLoggers.Count; i++)
                {
                    int k = i;
                    //Hub._marketDataFeed.UnsubscribeFromInstrument(new InstrInfo[] { tradeFeedLoggers[i].instrument });
                    Hub._marketDataFeed.RemoveTradeConsumer(new InstrInfo[] { tradeFeedLoggers[i].instrument }, tradeFeedLoggers[i]);
                    Hub._marketDataFeed.UnsubscribeFromInstrumentTrades(new InstrInfo[] { quoteFeedLoggers[i].instrument });
                    tradeFeedLoggers[i].Close();
                    cloud.UploadMarketDataFile(tradeFeedLoggers[k]._pathname);
                }
                quoteFeedLoggers.Clear();
                tradeFeedLoggers.Clear();
            }
            catch (Exception e)
            {
                updateTextBox2("Error disconnecting/removing loggers: " + e.ToString());
            }

            try
            {
                updateTextBox1("Disconnecting from feed...");
                Hub._marketDataFeed.DisconnectFromFeed();
                updateTextBox1("Disconnected from feed.");
            }
            catch (Exception e)
            {
                updateTextBox2("Error disconnecting from feed: " + e.ToString());
            }

            isLoggerRunning = false;
        }

        static bool isLoggerRunning = false;
        static bool isDuringMarketHours = false;

        public static void LogManager(object state)
        {
            bool isLoggerRunningLocal = (bool)state;
            DateTime now = DateTime.Now;

            if (((now.Hour == 15 && now.Minute > 30) || (now.Hour > 15) || (now.Hour < 8) || (now.Hour == 8 && now.Minute < 30)) && isDuringMarketHours)
            {
                updateTextBox1("Market is closed.");
                isDuringMarketHours = false;
            }

            if (((now.Hour == 15 && now.Minute <= 30) || now.Hour <15) && ((now.Hour == 8 && now.Minute >= 30) || (now.Hour >= 9)) && !isDuringMarketHours)
            {
                updateTextBox1("Market is open!");
                isDuringMarketHours = true;
            }

            if (isDuringMarketHours && !FeedCollector.isLoggerRunning)
            {
                //start logging
                updateTextBox1("Started logging...");
                FeedCollector.isLoggerRunning = true;
                StartLogging();
                
                updateTextBox1("Logging successfully started.");
                
            }

            if (!isDuringMarketHours && FeedCollector.isLoggerRunning)
            {
                //close logging
                FeedCollector.isLoggerRunning = false;
                updateTextBox1("Stopping logging...");

                
                ThreadPool.QueueUserWorkItem(new WaitCallback((o) => { DisconnectAndRemoveLoggers(); updateTextBox1("Logging successfully stopped."); }), null);
                
            }
        }
    }
}
