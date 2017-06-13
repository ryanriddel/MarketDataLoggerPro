using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MktSrvcAPI;

namespace FastOMS.Data_Structures
{
    public class QuoteBook : IBufferItem<QuoteBook, QuoteBookStruct>, IInfraData
    {

        public InstrInfo[] Instr { get; set; }
        public uint TS { get; set; }
        public byte PartID { get; set; }
        public int Mod { get; set; }

        public byte NumBid { get; set; }
        public byte[] BidExch { get; set; }
        public QuoteInfo[] BidBk { get; set; }

        public byte NumAsk { get; set; }
        public byte[] AskExch { get; set; }
        public QuoteInfo[] AskBk { get; set; }

        public long TEST_TIMESTAMP_TICKS = 0;
        public long ENTERED_BUFFER_TICKS = 0;
        public string InstrumentName = "";

        public void GetTOB(ref float bidprc, ref uint bidsz, ref float askprc, ref uint asksz)
        {
            int i = 0;
            bool valid = true;
            while (valid &&
                   i < NumBid)
            {
                bidsz += ((i == 0 || BidBk[i].prc == BidBk[i - 1].prc) ? BidBk[i].sz : 0);
                valid = ((i == 0 || BidBk[i].prc == BidBk[i - 1].prc) ? true : false);
                ++i;
            }

            bidprc = (NumBid > 0 ? BidBk[0].prc : 0.0f);

            i = 0;
            valid = true;
            while (valid &&
                   i < NumAsk)
            {
                asksz += ((i == 0 || AskBk[i].prc == AskBk[i - 1].prc) ? AskBk[i].sz : 0);
                valid = ((i == 0 || AskBk[i].prc == AskBk[i - 1].prc) ? true : false);
                ++i;
            }

            askprc = (NumAsk > 0 ? AskBk[0].prc : 0.0f);
        }

        public void Update(ref QuoteBookStruct newQuote)
        {
            Instr = (InstrInfo[]) newQuote.Instr.Clone();
            BidBk = newQuote.BidBk;
            AskBk = newQuote.AskBk;
            AskExch = newQuote.AskExch;
            BidExch = newQuote.BidExch;
            NumAsk = newQuote.NumAsk;
            NumBid = newQuote.NumBid;
            TS = newQuote.TS;
            PartID = newQuote.PartID;
            Mod = newQuote.Mod;
            TEST_TIMESTAMP_TICKS = newQuote.TEST_TIMESTAMP_TICKS;
            ENTERED_BUFFER_TICKS = DateTime.Now.Ticks;
        }

    }

    public struct QuoteBookStruct
    {
        public InstrInfo[] Instr { get; set; }
        public uint TS { get; set; }
        public byte PartID { get; set; }
        public int Mod { get; set; }

        public byte NumBid { get; set; }
        public byte[] BidExch { get; set; }
        public QuoteInfo[] BidBk { get; set; }

        public byte NumAsk { get; set; }
        public byte[] AskExch { get; set; }
        public QuoteInfo[] AskBk { get; set; }

        public long TEST_TIMESTAMP_TICKS;
        public string InstrumentName;

        public QuoteBookStruct(QuoteBook newQuote)
        {
            Instr = (InstrInfo[])newQuote.Instr.Clone();
            BidBk = newQuote.BidBk;
            AskBk = newQuote.AskBk;
            AskExch = newQuote.AskExch;
            BidExch = newQuote.BidExch;
            NumAsk = newQuote.NumAsk;
            NumBid = newQuote.NumBid;
            TS = newQuote.TS;
            PartID = newQuote.PartID;
            Mod = newQuote.Mod;
            TEST_TIMESTAMP_TICKS = newQuote.TEST_TIMESTAMP_TICKS;
            InstrumentName = Utilities.InstrToStr(Instr);
        }

        public void PopulateFields(ref InstrInfo[] instr, ref uint ts, ref byte partid, ref int mod, ref byte numbid, ref byte numask, ref byte[] bidexch, ref byte[] askexch, ref QuoteInfo[] bidbk, ref QuoteInfo[] askbk)
        {
            //Instr = (InstrInfo[]) instr.Clone();
            Instr = instr;
            BidBk = bidbk;
            AskBk = askbk;
            AskExch = askexch;
            BidExch = bidexch;
            NumAsk = numask;
            NumBid = numbid;
            TS = ts;
            PartID = partid;
            Mod = mod;
            TEST_TIMESTAMP_TICKS = DateTime.Now.Ticks;
            InstrumentName = Utilities.InstrToStr(instr);
        }

        public QuoteBookStruct(bool initialize)
        {
            Instr = new InstrInfo[2];
            BidBk = new QuoteInfo[10];
            AskBk = new QuoteInfo[10];
            AskExch = new byte[10];
            BidExch = new byte[10];
            NumAsk = 10;
            NumBid = 10;
            TS = 0;
            PartID = 0;
            Mod = 0;
            TEST_TIMESTAMP_TICKS = DateTime.Now.Ticks;
            InstrumentName = "";
        }
    }
}
