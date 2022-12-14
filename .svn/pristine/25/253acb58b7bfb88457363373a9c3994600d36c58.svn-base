using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komunikator3TavLib
{
    public static class Util
    {
        public static void SanitizeInput(string inputEt, string inputVt, string inputMt, out string outputEt, out string outputVt, out string outputMt)
        {
            Decimal? DecStanjeET = null, DecStanjeDVT = null, DecStanjeDMT = null;
            Decimal et = 0, vt = 0, mt = 0;

            outputEt = inputEt;
            outputVt = inputVt;
            outputMt = inputMt;

            if (Decimal.TryParse(inputEt, out et))
                DecStanjeET = et;

            if (Decimal.TryParse(inputVt, out vt))
                DecStanjeDVT = vt;

            if (Decimal.TryParse(inputMt, out mt))
                DecStanjeDMT = mt;

            if (DecStanjeET != null)
            {
                if (DecStanjeET > 0)
                {
                    outputEt = DecStanjeET.ToString();
                    DecStanjeDMT = null;
                    DecStanjeDVT = null;
                    outputMt = null;
                    outputVt = null;                    
                }
            }

            if (DecStanjeDVT != null)
            {
                if (DecStanjeDVT > 0)
                {
                    outputVt = DecStanjeDVT.ToString();
                    DecStanjeET = null;
                    outputEt = null;                    
                }
            }

            if (DecStanjeDMT != null)
            {
                if (DecStanjeDMT > 0)
                {
                    outputMt = DecStanjeDMT.ToString();
                    DecStanjeET = null;
                    outputEt = null;
                }
            }            
        }
    }
}
