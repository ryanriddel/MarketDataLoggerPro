using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MktSrvcAPI;
using FastOMS.Data_Structures;

namespace FastOMS
{
    
    public static class Utilities
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string FullQuoteBookToString(QuoteBook quote)
        {
            string returnString = "";

            if (quote.AskBk == null || quote.BidBk == null)
                return "";

            returnString += quote.TS;

            for (int i = 0; i < quote.BidBk.Length; i++)
                returnString += " " + quote.BidBk[i].prc;
            returnString += "=";
            for (int i = 0; i < quote.AskBk.Length; i++)
                returnString +=  " " + quote.AskBk[i].prc;
            returnString += "=";
            for (int i = 0; i < quote.BidBk.Length; i++)
                returnString += " " + quote.BidBk[i].sz;
            returnString += "=";
            for (int i = 0; i < quote.AskBk.Length; i++)
                returnString += " " + quote.AskBk[i].sz;
            returnString += "=";

            for (int i = 0; i < quote.BidExch.Length; i++)
                returnString += " " + quote.BidExch[i];
            returnString += "=";
            for (int i = 0; i < quote.AskExch.Length; i++)
                returnString += " " + quote.AskExch[i];

            returnString += "=";
            returnString += quote.NumBid + " " + quote.NumAsk + " " + quote.PartID + " " + quote.Mod;

            return returnString;
        }

        public static string TradeToString(TradeInfo trade)
        {
            string str = trade.TS + " " + trade.Prc + " " + trade.Sz + " " + trade.BidExch + " " + trade.AskExch + " " +
                trade.NBBOBidPrc + " " + trade.NBBOBidSz + " " + trade.NBBOAskPrc + " " + trade.NBBOAskSz + " " + trade.TotVol + " " +
                trade.CondNum;

            return str;


        }


        #region InstrInfo Helpers

        public static string InstrToStr(InstrInfo instr)
        {
            switch (instr.type)
            {
                case InstrInfo.EType.OPTION:
                    return $"{instr.sym} {instr.strike} {instr.callput} {instr.maturity.ToString("0000-00-00")}";
                case InstrInfo.EType.EQUITY:
                    return instr.sym;
            }
            return "";
        }

        public static InstrInfo[] CreateInstrInfo(string under, string exp, double strike, string callput)
        {
            InstrInfo[] _instr;
            if (!string.IsNullOrEmpty(under) && (exp != null) &&
                (strike > 0) && !string.IsNullOrEmpty(callput))
            {
                _instr = new InstrInfo[1];
                _instr[0] = new InstrInfo
                {
                    sym = under,
                    maturity = convertExpirationToInt(exp),
                    strike = (float)strike,
                    type = InstrInfo.EType.OPTION
                };
                _instr[0].callput = callput == "Call"
                    ? _instr[0].callput = InstrInfo.ECallPut.CALL
                    : _instr[0].callput = InstrInfo.ECallPut.PUT;
                return _instr;
            }
            return null;
        }
        

        public static int convertExpirationToInt(string exp)
        {
            var expArr = exp.Split('-');

            return Convert.ToInt32($"{expArr[0]}{expArr[1]}{expArr[2]}");
        }

        //copied from RBClients.SprdClient.CustSprdSymbol
        public static string InstrToStr(InstrInfo[] instrinfo)
        {
            string val = "";

            for (int i = 0; i < instrinfo.Length; ++i)
            {
                val += InstrToStr(instrinfo[i]);
                if (instrinfo.Length > 1)
                {
                    val += string.Format("{0}{1}", instrinfo[i].side.ToString().Substring(0, 1), instrinfo[i].ratio);

                    if (i < (instrinfo.Length - 1))
                    {
                        val += "_";
                    }
                }
            }
            return val;
        }

        static Parsers p = new Parsers();
        public static string InstrArrayToOldOMSString(InstrInfo[] instr)
        {
            if (instr.Length > 1)
            {
                return p.DecodeInstrInfoArrToStr(instr);
            }
            else
            {
                return p.DecodeInstrInfoToStr(instr);
            }
        }

        public static InstrInfo[] OldOMSStringToInstrArray(string s)
        {
            InstrInfo[] instrArr = null;
            p.EncodeInstrInfoFromStr(s, ref instrArr);
            return instrArr;
        }

        public static int EncodeInstrInfoFromStr(string sym, ref InstrInfo[] instrinfo)
        {
            int rc = -1;
            instrinfo = null;

            if (sym.Length > 0)
            {
                string[] strArr = sym.Split('_');
                instrinfo = new InstrInfo[strArr.Count()];

                bool sprd = (strArr.Count() > 1);

                List<string> instrList = new List<string>(strArr);

                List<OrdInfo.ESide> sideList = null;
                List<int> ratioList = null;

                if (sprd)
                {
                    instrList.Sort();
                    DecodeMLegInfo(ref instrList, ref sideList, ref ratioList);
                }

                bool done = false;

                int i = 0;
                while (!done && i < strArr.Count())
                {
                    if (EncodeInstrInfoFromStr(instrList[i], ref instrinfo[i]) != 0)
                    {
                        done = true;
                    }
                    else
                    {
                        if (sprd && i < Math.Min(instrinfo.Length, sideList.Count))
                        {
                            instrinfo[i].side = sideList[i];
                            instrinfo[i].ratio = ratioList[i];
                        }

                        ++i;
                    }
                }

                if (i == strArr.Count())
                    rc = 0;
            }

            return rc;
        }

        public static InstrInfo[] ConvertOptSymToInstrInfo(string optSym)
        {
            string[] arr = optSym.Split(' ');
            InstrInfo[] instr = new InstrInfo[1];
            instr[0] = new InstrInfo();

            if (arr.Length == 1)
            {
                instr[0].type = InstrInfo.EType.EQUITY;
                instr[0].sym = arr[0];
            }
            else
            {
                string sym = arr[0];
                int mat = DecodeExpiration(arr[1]);
                float strk = float.Parse(arr[2]);
                string type = arr[3];

                instr[0].type = InstrInfo.EType.OPTION;

                instr[0].sym = sym;
                instr[0].maturity = mat;
                instr[0].strike = strk;

                if (type == "C")
                {
                    instr[0].callput = InstrInfo.ECallPut.CALL;
                }
                else
                {
                    instr[0].callput = InstrInfo.ECallPut.PUT;
                }
            }
            return instr;
        }

        public static string DecodeInstrInfoToStrWithSideRatio(InstrInfo instrinfo)
        {
            string val = "";

            if (instrinfo.type == InstrInfo.EType.EQUITY)
            {
                val = string.Format("{0}{1}{2}", instrinfo.sym, instrinfo.side == OrdInfo.ESide.BUY ? "B" : "S", instrinfo.ratio);
            }
            else if (instrinfo.type == InstrInfo.EType.OPTION)
            {
                val = string.Format("{0}{1}{2}{3}{4}{5}", instrinfo.sym, instrinfo.maturity,
                    instrinfo.callput.ToString().Substring(0, 1), instrinfo.strike.ToString("0.00"),
                    instrinfo.side == OrdInfo.ESide.BUY ? "B" : "S", instrinfo.ratio);
            }

            return val;
        }

        public static string DecodeInstrInfoArrToStr(InstrInfo[] instrinfo)
        {
            string val = "";

            int length = instrinfo.Length;
            bool sprd = (length > 1 ? true : false);

            for (int i = 0; i < length; ++i)
            {
                val += DecodeInstrInfoToStrWithSideRatio(instrinfo[i]);

                if (sprd)
                {
                    if (i < (length - 1))
                        val += "_";
                }
            }

            return val;
        }

        public static int DecodeMLegInfo(ref List<string> symList, ref List<OrdInfo.ESide> sideList, ref List<int> ratioList)
        {
            sideList = new List<OrdInfo.ESide>(symList.Count());
            ratioList = new List<int>(symList.Count());

            for (int i = 0; i < symList.Count(); ++i)
            {
                int length = symList[i].Length;

                int idx = Math.Max(symList[i].LastIndexOf('B'), symList[i].LastIndexOf('S'));

                int ratio = 0;

                if (idx >= 0 &&
                    idx < length - 1 &&
                    Char.IsDigit(symList[i][idx + 1]) &&
                    (ratio = int.Parse(symList[i].Substring(idx + 1))) > 0)
                {
                    ratioList.Add(ratio);
                    sideList.Add(symList[i][idx] == 'B' ? OrdInfo.ESide.BUY : OrdInfo.ESide.SELL);
                    symList[i] = symList[i].Substring(0, idx);
                }
            }

            return symList.Count();
        }

        public static int DecodeExpiration(string sym)
        {
            int val = 0;

            int xp = 0;
            int yy = 0;
            int mm = 0;
            int dd = 0;

            if (sym.Length == 8 &&
                Char.IsDigit(sym[0]) &&
                (xp = int.Parse(sym)) > 0 &&
                (yy = xp / 10000) >= 2014 &&
                (mm = (xp / 100) % 100) >= 1 &&
                mm <= 12 &&
                (dd = xp % 100) >= 1 &&
                dd <= 31)
            {
                val = xp;
            }

            return val;
        }

        public static string EncodeNormalizedStrFromInstrInfo(InstrInfo[] instrinfo)
        {
            string infoString = "";

            InstrInfo[] info = new InstrInfo[instrinfo.Length];
            instrinfo.CopyTo(info, 0);

            List<InstrInfo> list = info.ToList();
            list.Sort();

            return DecodeInstrInfoArrToStr(list.ToArray());
        }

        public static int EncodeInstrInfoFromStr(string sym, ref InstrInfo instrinfo)
        {
            int rc = -1;

            instrinfo = null;

            int length = sym.Length;

            float strike = 0.0f;
            int xp = 0;

            if (length > 0 &&
                sym[0] >= 'A' &&
                sym[0] <= 'Z')
            {
                int idx = Math.Max(sym.LastIndexOf('C'), sym.LastIndexOf('P'));

                if (idx >= 0 &&
                    idx < length - 1 &&
                    Char.IsDigit(sym[idx + 1]) &&
                    (strike = float.Parse(sym.Substring(idx + 1))) > 0.0f &&
                    idx > 8 &&
                    (xp = DecodeExpiration(sym.Substring(idx - 8, 8))) > 0

                  )
                {
                    instrinfo = new InstrInfo();

                    instrinfo.type = InstrInfo.EType.OPTION;
                    instrinfo.sym = sym.Substring(0, idx - 8);
                    instrinfo.maturity = xp;
                    instrinfo.strike = strike;
                    instrinfo.callput = (sym[idx] == 'C' ? InstrInfo.ECallPut.CALL : InstrInfo.ECallPut.PUT);
                    instrinfo.ratio = 1;
                    rc = 0;
                }
                else if (sym.All(Char.IsLetterOrDigit))
                {
                    instrinfo = new InstrInfo();
                    instrinfo.type = InstrInfo.EType.EQUITY;
                    instrinfo.sym = sym;

                    rc = 0;
                }
            }

            return rc;
        }

        public static string EncodeInstrInfoToSterling(InstrInfo[] instrinfo)
        {
            string val = "";

            if (instrinfo.Length == 1 &&
                instrinfo[0].type == InstrInfo.EType.OPTION)
            {
                string expiration = string.Format("{0}{1:00}{2:00}", (instrinfo[0].maturity / 10000) - 2000, (instrinfo[0].maturity / 100) % 100, instrinfo[0].maturity % 100);

                val = string.Format("{0} {1}{2}{3}", instrinfo[0].sym, expiration,
                                    (instrinfo[0].callput == InstrInfo.ECallPut.CALL ? 'C' : 'P'),
                                    (instrinfo[0].strike * 1000).ToString());
            }

            return val;
        }

        public static InstrInfo DecodeOCCtoInstrInfo(string option)
        {
            InstrInfo instr = new InstrInfo();
            int splitPt = 0;

            for (int i = option.Length - 1; i >= 0; i--)
            {
                if (option[i] == 'P' || option[i] == 'C')
                {
                    if (option[i] == 'P')
                        instr.callput = InstrInfo.ECallPut.PUT;
                    else if (option[i] == 'C')
                        instr.callput = InstrInfo.ECallPut.CALL;

                    splitPt = i;
                    break;
                }
            }

            string pre = option.Substring(0, splitPt);
            string post = option.Substring(splitPt + 1);

            instr.type = InstrInfo.EType.OPTION;
            instr.strike = (float)Convert.ToDouble(post.Substring(0, 5) + "." + post.Substring(5));
            instr.maturity = Convert.ToInt32(pre.Substring(pre.Length - 6));
            instr.sym = pre.Substring(0, pre.Length - 6);
            return instr;
        }

        public static string SpreadLegToString(InstrInfo leg)
        {
            string temp = string.Empty;
            try
            {
                if (leg.type == InstrInfo.EType.OPTION)
                {
                    temp = leg.sym + " " + leg.maturity.ToString() + " " + leg.callput + " " + leg.strike.ToString() + " " + leg.side.ToString().Substring(0, 1) + leg.ratio.ToString();
                }
                else if (leg.type == InstrInfo.EType.EQUITY)
                {
                    temp = leg.sym + " " + leg.side.ToString().Substring(0, 1) + leg.ratio.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Translation error.");
            }

            return temp;
        }

        #endregion

        #region Exchange Codes
        public static byte ExchangeStringToByte(string exchange)
        {
            byte exch;
            switch (exchange)
            {
                case "EDGA":
                    exch = Convert.ToByte('J');
                    break;
                case "EDGX":
                    exch = Convert.ToByte('K');
                    break;
                case "EDGXO":
                    exch = Convert.ToByte('E');
                    break;
                case "XASE":
                case "AMEX":
                case "AMEXO":
                    exch = Convert.ToByte('A');
                    break;
                case "XCBO":
                case "CBOEOS":
                case "CBOEO":
                case "NSE":
                    exch = Convert.ToByte('C');
                    break;
                case "XISX":
                case "ISEOS":
                case "ISEO":
                case "ISE":
                    exch = Convert.ToByte('I');
                    break;
                case "C2O":
                case "CBSX":
                    exch = Convert.ToByte('W');
                    break;
                case "XPHO":
                case "PHLXO":
                case "NQPX":
                    exch = Convert.ToByte('X');
                    break;
                case "SMARTOPT":
                case "SMART":
                    exch = Convert.ToByte('$');
                    break;
                case "BOXO":
                case "NQBX":
                    exch = Convert.ToByte('B');
                    break;
                case "NASDAQO":
                case "INET":
                    exch = Convert.ToByte('Q');
                    break;
                case "BATSO":
                case "BATS":
                    exch = Convert.ToByte('Z');
                    break;
                case "PCXO":
                    exch = Convert.ToByte('N');
                    break;
                case "GEMINI":
                    exch = Convert.ToByte('H');
                    break;
                case "MIAX":
                case "CHX":
                    exch = Convert.ToByte('M');
                    break;
                case "XBXO":
                    exch = Convert.ToByte('T');
                    break;
                case "NYSE":
                    exch = Convert.ToByte('N');
                    break;
                case "ARCA":
                    exch = Convert.ToByte('P');
                    break;
                case "BYX":
                    exch = Convert.ToByte('Y');
                    break;
                default:
                    exch = 0;
                    break;
            }
            return exch;
        }

        public static string EquityExchangeCodeToString(byte exch)
        {
            return EqExchUtl.name[exch];
        }

#endregion
        public static double StandardDeviation(IEnumerable<long> values)
        {
            double avg = values.Average();
            
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }

        #region Date/Time Tools
        public static DateTime UnixEpochToDateTime(uint u)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            return epoch.AddSeconds(u);
        }

        public static DateTime UnixCurrentDateToDateTime(uint usec)
        {
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);

            return dt.AddMilliseconds(usec);
        }

        public static DateTime TimeFromMidnight(uint usec)
        {
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);

            return dt.AddMilliseconds(usec);
        }

        public static uint convertTimestampToMilliseconds(uint timestamp)
        {
            int hours = (int)timestamp / 10000000;
            int minutes = (int)timestamp / 100000 - hours * 100;
            int seconds = (int)timestamp / 1000 - hours * 10000 - minutes * 100;
            return (uint)((timestamp % 1000) + seconds * 1000 + minutes * 60 * 1000 + hours * 60 * 60 * 1000);
        }

        public static uint GetMillisecondsOfToday()
        {
            var currentTime = DateTime.Now;
            var startOfDay = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            return (uint)(currentTime - startOfDay).TotalMilliseconds;
        }
        public static uint GetTimestampFromDateTime(DateTime time)
        {
            //for string: Convert.ToUInt32(DateTime.Now.ToString("HHmmssfff"));
            //130,123,456
            return (uint)(time.Hour * 10000000 + time.Minute * 100000 + time.Second * 1000 + time.Millisecond);
        }

        public static string GetDateStamp()
        {
            var t = DateTime.Now;
            return t.Month.ToString() + t.Day.ToString() + "_" + t.Hour.ToString() + t.Minute.ToString();
        }

        #endregion
    }
}
