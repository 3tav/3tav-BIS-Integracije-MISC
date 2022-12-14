using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Komunikator3TavLib
{
    [DataContract]
    public class OdcitekPlin
    {
        [DataMember]
        public int MerilnoMesto { get; set; }                
        [DataMember]
        public DateTime DatumOdcitka { get; set; }
        [DataMember]
        public string Stanje { get; set; }
        [DataMember]
        public int TipOdcitka { get; set; }
        [DataMember]
        public string Opomba { get; set; }
        [DataMember]
        public int Uporabnik { get; set; }

        public OdcitekPlin()
        {
            TipOdcitka = 1;
            Opomba = string.Empty;
            Uporabnik = -1;
        }

        public OdcitekPlin(int merilnoMesto, DateTime datumOdcitka, string stanje) : this()
        { 
            MerilnoMesto = merilnoMesto;
            DatumOdcitka = datumOdcitka;
            Stanje = stanje;            
        }        
    }
}