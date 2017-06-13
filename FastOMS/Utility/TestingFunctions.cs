using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using MktSrvcAPI;
using FastOMS.Data_Structures;

namespace FastOMS
{
    public static class TestingFunctions
    {
        static Random numGenerator = new Random((int) DateTime.Now.Ticks);
        public static InstrInfo[] TestOption;
        public static InstrInfo[] TestSpread;
        public static InstrInfo[] TestSpread2;
        public static InstrInfo[] TestSpread3;
        public static InstrInfo[] TestSpread4;
        public static InstrInfo[] FourLegTestSpread;

        static TestingFunctions()
        {
            TestOption = new InstrInfo[] { CreateOptionInstrument("ORCL", 20170721, InstrInfo.ECallPut.CALL, 48, OrdInfo.ESide.BUY) };

            TestSpread = new InstrInfo[]
            {
                CreateOptionInstrument("ORCL", 20170721, InstrInfo.ECallPut.CALL, 48F, OrdInfo.ESide.BUY),
                CreateOptionInstrument("ORCL",20170721, InstrInfo.ECallPut.CALL, 50F, OrdInfo.ESide.SELL)
            };
            TestSpread2 = new InstrInfo[]
            {
                CreateOptionInstrument("HD", 20170616, InstrInfo.ECallPut.CALL, 150F, OrdInfo.ESide.SELL),
                CreateOptionInstrument("HD",20170616, InstrInfo.ECallPut.CALL, 145F, OrdInfo.ESide.BUY)
            };
            
            TestSpread4 = new InstrInfo[]
            {
                CreateOptionInstrument("VXX", 20170915, InstrInfo.ECallPut.CALL, 14F, OrdInfo.ESide.BUY),
                CreateOptionInstrument("VXX",20170915, InstrInfo.ECallPut.CALL, 15F, OrdInfo.ESide.SELL)
            };

            InstrInfo[] instrArr = Utilities.OldOMSStringToInstrArray("ABX20190118C17.00B1_ABX20190118C22.00S2_ABX20190118C27.00B1");
            TestSpread3 = instrArr;


            FourLegTestSpread = new InstrInfo[]
            {
                TestSpread2[0],
                TestSpread2[1],
                TestSpread4[0],
                TestSpread4[1]
            };
        }

        public static InstrInfo CreateOptionInstrument(string symbol, int expiration, InstrInfo.ECallPut callOrPut, float strike, OrdInfo.ESide side)
        {
            InstrInfo newInstr = new InstrInfo();
            newInstr.callput = callOrPut;
            newInstr.maturity = expiration;
            newInstr.ratio = 1;
            newInstr.side = side;
            newInstr.strike = strike;
            newInstr.sym = symbol;
            newInstr.type = InstrInfo.EType.OPTION;
            return newInstr;
        }

        public static InstrInfo GenerateRandomInstrument(InstrInfo.EType type)
        {
            InstrInfo randomInstr = new InstrInfo() ;


            randomInstr.type = type;
            randomInstr.sym = highCapSymbols[numGenerator.Next(0, highCapSymbols.Length - 1)];
            if (type == InstrInfo.EType.OPTION)
            {
                int maturityYear = numGenerator.Next(2018, 2020);
                int maturityMonth = numGenerator.Next(1, 13);
                int maturityDay = numGenerator.Next(1, 29);

                randomInstr.maturity = maturityYear * 10000 + maturityMonth * 100 + maturityDay;
                randomInstr.strike = numGenerator.Next(10, 101);
                randomInstr.callput = maturityYear % 2 == 0 ? InstrInfo.ECallPut.CALL : InstrInfo.ECallPut.PUT;
            }
            
            return randomInstr;
        }


        public static string[] highCapSymbols = { "AAPL", "NVDA", "MU",
             "QQQ", "MSFT", "SIRI", "CSCO",  "BEAT",
            "SNAP", "FB", "TSLA", "SPY", "F", "BAC", "RAD", "GM", "KR",
            "GE",  "NFLX", "CAT", "GS", "BA", "GOOG" };
        public static string[] lowCapSymbols = {"CMA", "CSV", "EME", "COST", "BAK", "LMT", "WNR"
        , "CRS", "OFC", "UN", "EOCC", "ASML", "HON", "UL", "CL", "EVR", "GVA", "PATK", "WY",
        "EGP", "CE", "HSY", "WUBA", "BIVV", "AMRI" ,
            "BIO", "TWN", "BPY", "DECK", "DDD", "FSLR", "GEOS", "CZR",
            "SGA", "ACBI", "AMS", "ANCB", "AMC", "CCF", "CPK", "FGL",
            "GFN", "XLF", "FET", "ISCA","DIS","DB", "AAON",};

        public static void CreateLevel2Forms(uint number)
        {
            for (uint i = 0; i < number; i++)
                Hub._formFactory.CreateLevel2Form(new InstrInfo[] { GenerateRandomInstrument(InstrInfo.EType.OPTION) });
        }

        public static void CreateLevel2Form(InstrInfo[] i)
        {
            Hub._formFactory.CreateLevel2Form(i);
        }

        public static void CreateLevel2Form(InstrInfo i)
        {
            InstrInfo[] a = { i };
            Hub._formFactory.CreateLevel2Form(a);
        }

        public static void CreateLevel2EquityForm(string symbol)
        {
            
            Hub._formFactory.CreateLevel2Form(CreateEquityInstrument(symbol));
        }

        public static InstrInfo[] CreateEquityInstrument(string symbol)
        {
            InstrInfo[] info = new InstrInfo[1];
            info[0] = new InstrInfo();
            info[0].type = InstrInfo.EType.EQUITY;
            info[0].sym = symbol;
            return info;
        }

        public static void CreateLevel2OptionForm(InstrInfo[] instruments)
        {
            Hub._formFactory.CreateLevel2Form(instruments);
        }

        public static void CreateRandomEquityLevel2Form()
        {
            Hub._formFactory.CreateLevel2Form(new InstrInfo[] { GenerateRandomInstrument(InstrInfo.EType.EQUITY) });
        }

        
        public static void CreateQuoteGenerator()
        {
            BackgroundWorker newWorker = new BackgroundWorker();
            newWorker.DoWork += new DoWorkEventHandler(NewWorker_DoWork);

            
        }

        public static QuoteBook GenerateRandomQuoteBook(InstrInfo[] instruments)
        {
            QuoteBook book = new QuoteBook();
            Random rGen = new Random((int) DateTime.Now.Ticks);

            int bidBookLength = rGen.Next(3, 6);
            int askBookLength = rGen.Next(3, 6);
            float startingPrice =  (float) Math.Round(((float)rGen.Next(3, 20) / 3.0195F),2) ;
            bidBookLength = askBookLength;

            book.BidBk = new QuoteInfo[bidBookLength];
            book.AskBk = new QuoteInfo[askBookLength];
            book.BidExch = new byte[bidBookLength];
            book.AskExch = new byte[askBookLength];
            book.NumBid = (byte) bidBookLength;
            book.NumAsk = (byte) askBookLength;

            
            for(int i=0; i<bidBookLength; i++)
            {
                book.BidBk[i].prc = startingPrice;
                book.BidBk[i].sz = (uint) rGen.Next(1, 5);
                book.BidExch[i] = (byte) i;
                startingPrice += 0.01F;
            }
            startingPrice += (float) rGen.Next(1,10) / 100F;
            
            for (int i = 0; i < askBookLength; i++)
            {
                book.AskBk[i].prc = startingPrice;
                book.AskBk[i].sz = (uint)rGen.Next(1, 5);
                book.AskExch[i] = (byte)i;
                startingPrice += 0.01F;
            }

            book.TS = Utilities.GetTimestampFromDateTime(DateTime.Now);
            book.Mod = 0;
            book.Instr = instruments;
            book.PartID = 0;
            
            return book;
        }

        public static TradeInfo GenerateRandomTradeInfo(Random rGen, InstrInfo[] instruments)
        {
            TradeInfo newT = new TradeInfo();
            
            newT.Instr = instruments;
            newT.BidExch = EqExchUtl.name[(int)rGen.Next(1, 11)];
            newT.AskExch = EqExchUtl.name[(int)rGen.Next(1, 11)];
            newT.Cond = new byte[] { 0 };
            newT.High = rGen.Next(200, 300) / 10F;
            newT.Low = rGen.Next(100, 200) / 10F;
            newT.TotVol = (uint) rGen.Next(1, 13) * 100;
            newT.TS = Utilities.GetTimestampFromDateTime(DateTime.Now);
            newT.Prc = rGen.Next(150, 250) / 10F;
            newT.Sz = (uint) rGen.Next(1, 1000);
            newT.Open = 5;
            newT.PartID = 0;
            newT.NBBOBidSz = 10;
            newT.NBBOAskSz = 11;
            newT.NBBOBidPrc = newT.Low + 1;
            newT.NBBOAskPrc = newT.High - 1;
            newT.CondNum = 1;

            return newT;
        }

        private static void NewWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int numberOfSymbols = 5;
        }

        public static void RunTest()
        {
            /*
             _marketDataFeed.SubscribeToQuoteFeed(testInstrument);


             Thread newThread = new Thread(() =>
             {
                 long counter = 0;
                 while (true)
                 {
                     QuoteBook q = new QuoteBook();

                     q.BidBk = new QuoteInfo[8];
                     q.AskBk = new QuoteInfo[8];
                     q.BidExch = new byte[8];
                     q.AskExch = new byte[8];

                     q.Instr = new InstrInfo[1];
                     q.Instr[0] = testInstrument;

                     q.InstrumentName = Utilities.InstrToStr(testInstrument);
                     q.TEST_TIMESTAMP_TICKS = DateTime.Now.Ticks;

                     Hub._marketDataFeed.AddQuote(q);
                     counter++;

                     if (counter % 1000000 == 0)
                         Console.WriteLine("MIL: " + (int)counter / 1000000);
                 }



             });
             newThread.Start();*/
        }
    }
}
