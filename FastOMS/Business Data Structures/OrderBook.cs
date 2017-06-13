using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MktSrvcAPI;

namespace FastOMS.Data_Structures
{
    public class OrderBook
    {
        public bool isCancelling;
        public bool _contra;
        public byte _exch;
        public InstrInfo[] _instr;
        public ulong _ordID;
        public float _ordprc;
        public byte _ordSide;
        public uint _ordsz;
        public byte _ordtif;
        public byte _ordType;

        public OrderBook() { }
    }
}
