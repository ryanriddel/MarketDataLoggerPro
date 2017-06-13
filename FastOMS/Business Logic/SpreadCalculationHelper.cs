using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MktSrvcAPI;
using RBClients.SprdClient;
using FastOMS.Data_Structures;

namespace FastOMS
{
    public class SpreadCalculationHelper
    {
        public InstrInfo[] Legs;

        public SpreadCalculationHelper(InstrInfo[] spreadInstruments)
        {
            Legs = spreadInstruments;
        }

        public ImpliedMarketData CalculateImpliedMarket(List<QuoteBook> legQuoteBooks)
        {
            LegOrderData[,] LOD = new LegOrderData[legQuoteBooks.Count, 2];

            for (int i = 0; i < legQuoteBooks.Count; i++)
            {
                LOD[i, 0] = new LegOrderData();
                LOD[i, 1] = new LegOrderData();
                legQuoteBooks[i].GetTOB(ref LOD[i, 1].price, ref LOD[i, 1].size, ref LOD[i, 0].price, ref LOD[i, 0].size);
            }

            return new ImpliedMarketData(CalcBidPrc(Legs, LOD), CalcAskPrc(Legs, LOD), CalcBidSize(Legs, LOD), CalcAskSize(Legs, LOD));
        }
        


        public float CalcBidPrc(InstrInfo[] Legs, LegOrderData[,] Lod)
        {
            float tempValue = 0;
            try
            {
                int legCnt = Legs.Length;
                for (int i = 0; i < legCnt; i++)
                {
                    float value = 0;

                    if (Legs[i].side == OrdInfo.ESide.BUY)
                        value = 1 * (Legs[i].ratio) * (Lod[i, 1].price);
                    else
                        value = -1 * (Legs[i].ratio) * (Lod[i, 0].price);

                    if (Legs[i].type == InstrInfo.EType.EQUITY)
                        value = value / 100;

                    tempValue = tempValue + value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalcBidPrc error: " + ex.Message);
            }
            return tempValue;
        }

        public uint CalcBidSize(InstrInfo[] Legs, LegOrderData[,] Lod)
        {
            uint tempValue = 0;
            try
            {
                int legCnt = Legs.Length;
                for (int i = 0; i < legCnt; i++)
                {
                    if (Legs[i].side == OrdInfo.ESide.BUY)
                        tempValue = Lod[i, 1].size;
                    else
                        tempValue = Lod[i, 0].size;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalcBidSize error: " + ex.Message);
            }
            return tempValue;
        }

        public uint CalcAskSize(InstrInfo[] Legs, LegOrderData[,] Lod)
        {
            uint tempValue = 0;
            try
            {
                int legCnt = Legs.Length;
                for (int i = 0; i < legCnt; i++)
                {
                    if (Legs[i].side == OrdInfo.ESide.BUY)
                        tempValue = Lod[i, 0].size;
                    else
                        tempValue = Lod[i, 1].size;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalcAskSize error: " + ex.Message);
            }
            return tempValue;
        }

        public float CalcAskPrc(InstrInfo[] Legs, LegOrderData[,] Lod)
        {
            float tempValue = 0;
            try
            {
                int legCnt = Legs.Length;
                for (int i = 0; i < legCnt; i++)
                {
                    float value = 0;

                    if (Legs[i].side == OrdInfo.ESide.BUY)
                        value = 1 * (Legs[i].ratio) * (Lod[i, 0].price);
                    else
                        value = -1 * (Legs[i].ratio) * (Lod[i, 1].price);

                    if (Legs[i].type == InstrInfo.EType.EQUITY)
                        value = value / 100;

                    tempValue = tempValue + value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalcAskPrc error: " + ex.Message);
            }
            return tempValue;
        }
        
        public class ImpliedMarketData
        {
            public float bidPrice;
            public float askPrice;
            public uint bidSize;
            public uint askSize;

            public ImpliedMarketData(float bPrice, float aPrice, uint bSize, uint aSize)
            {
                bidPrice = bPrice;
                askPrice = aPrice;
                bidSize = bSize;
                askSize = aSize;
            }
        }
    }
}
