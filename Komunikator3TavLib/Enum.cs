using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komunikator3TavLib
{
    public enum NaciniOddaje { Privzeto = 0, Cakalnica = 1, Perun = 2 }
    public enum StatusiCakalnica {Neobdelan = 0, Obdelan = 1, VObdelavi = 2, Napaka = -1, Evidencno = 100 }
    public enum VrsteOdcitka {Perun = 1, SODO = 2, Cakalnica = 10, PerunCache = 20}
}
