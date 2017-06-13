using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MktSrvcAPI;

namespace FastOMS
{
    public enum OrderStatus
    {
        New = 0,
        PendingNew = 1,
        Cancelled = 2,
        PendingCancel = 3,
        Partial = 4,
        Filled = 5,
        Rejected = 6
    }
    public class OrderInfo : ICloneable, IInfraData
    {
        public ulong orderID { get; set; }
        public InstrInfo[] instruments { get; set; }
        public float prc { get; set; }
        public uint qty { get; set; }
        public uint lastQtyExecuted { get; set; }
        public uint remainingQuantity { get; set; }
        public byte exchByte { get; set; }
        public string exchange { get; set; }
        public ESide side { get; set; }
        public ETIF tif { get; set; }
        public EType type { get; set; }
        public bool isClosingOrder { get; set; } //if true, the order is closing out a position
        public bool isContra { get; set; }
        public OrderStatus status { get; set; }
        public uint fillQuantity { get; set; }
        public string Symbol { get { return Utilities.InstrArrayToOldOMSString(instruments); } }
        public string Error { get; set; }
        public DateTime LastExecTS { get; set; }


        public List<KeyValuePair<DateTime, uint>> partialFillHistory = new List<KeyValuePair<DateTime, uint>>();

        //this is essentially a *hash* of this particular order...it is imperfect, but the best we can do
        //with the current order callbacks.
        public override string ToString()
        {
            string returnString = Utilities.InstrToStr(instruments) + type.ToString() + tif.ToString() + prc.ToString()
                + qty.ToString() + side.ToString();
            return returnString;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public OrderInfo()
        {

        }
        public OrderInfo(ulong _orderID, InstrInfo[] instr, byte _ordType, byte _ordTIF, float _ordPrc, uint _ordSize, 
            byte _ordSide, string _orderDest, bool _contra)
        {
            orderID = _orderID;
            instruments = ((InstrInfo[])instr.Clone());
            type = (EType)(_ordType);
            tif = (ETIF)( _ordTIF);
            prc = _ordPrc;
            qty = _ordSize;
            side = (ESide)_ordSide;
            exchange = _orderDest;
            isContra = _contra;
        }

        public enum ESide
        {
            BUY = 0,
            SELL = 1,
            SHORTSELL = 2
        }
        public enum ETIF
        {
            DAY = 0,
            GTC = 1,
            OPG = 2,
            IOC = 3,
            FOK = 4,
            GTX = 5,
            GTD = 6
        }
        public enum EType
        {
            MKT = 0,
            LIM = 1,
            STOP = 2,
            STOPLIM = 3,
            PEGGED = 4
        }
    }
}
