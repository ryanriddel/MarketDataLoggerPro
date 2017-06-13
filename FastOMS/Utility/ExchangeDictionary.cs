using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MktSrvcAPI;

namespace FastOMS.Utility
{
    public static class ExchangeDictionary
    {
        private static Dictionary<byte, string> _optExchDict;
        private static Dictionary<byte, string> _eqExchDict;

        static ExchangeDictionary()
        {
            InitOptExchDict();
            InitEqExchDict();
        }

        public static string GetOptExchange(byte b)
        {
            if (_optExchDict.ContainsKey(b))
                return _optExchDict[b];
            else
                return "????";
        }

        public static string GetEqExchange(byte b)
        {
            if (_eqExchDict.ContainsKey(b))
                return _eqExchDict[b];
            else
                return "?????";
        }

        private static void InitOptExchDict()
        {
            _optExchDict = new Dictionary<byte, string>();

            for (int i = 0; i < OptExchUtl.code.Length; i++)
            {
                _optExchDict[Convert.ToByte(OptExchUtl.code[i])] = OptExchUtl.name[i];
            }
            _optExchDict.Add(0, "");
        }

        private static void InitEqExchDict()
        {
            _eqExchDict = new Dictionary<byte, string>();

            for (int i = 0; i < EqExchUtl.code.Length; i++)
            {
                _eqExchDict[Convert.ToByte(EqExchUtl.code[i])] = EqExchUtl.name[i];
            }
            _eqExchDict.Add(0, "");
        }
    }
}
