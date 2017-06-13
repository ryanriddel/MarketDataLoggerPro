using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MktSrvcAPI;
using FastOMS.Data_Structures;

namespace FastOMS
{
    public interface IMarketDataConsumer<T>
    {
        void NewDataHandler(T _event);
    }

    public interface IGenericMarketDataConsumer : IMarketDataConsumer<QuoteBook>, IMarketDataConsumer<TradeInfo>
    {

    }
    public interface IMarketDataProducer
    {
        //Return true if the instrument(s) had not been previously subscribed to
        bool AddQuoteConsumer(InstrInfo[] instruments, IMarketDataConsumer<QuoteBook> consumer);
        bool AddTradeConsumer(InstrInfo[] instruments, IMarketDataConsumer<TradeInfo> consumer);
    }
}
