using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
namespace Komunikator3TavLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IwsSporocanjeOdcitkov" in both code and config file together.
    [ServiceContract]
    public interface IwsSporocanjeOdcitkov
    {        
        [OperationContract]
        dcResult Set_StanjeNaPerun(Int32 MerilnoMesto, String DatumOdbirka, String StanjeET, String StanjeVT, String StanjeMT, Int32 NacinPridobitveOdcitka, Int32 Uporabnik, out Int32? PerunOdcitekID);
        [OperationContract]
        dcResult Set_OpomboNaStanjaNaPerun(Int32 PerunOdcitekID, String Opomba);
        [OperationContract]
        dcResult Get_ZadnjiOdbirekZaMerilnoMesto(Int32 MerilnoMesto, out dcPerunOdcitek PerunOdcitek);
        [OperationContract]
        dcResult Get_PovprecnoPoraboZaMerilnoMesto(Int32 MerilnoMesto, out dcPovprecnaPoraba PovprecnaPoraba);
        [OperationContract]
        dcResult Get_OdbirekZaMerilnoMestoInDatum(Int32 MerilnoMesto, DateTime DatumStanja, out dcPerunOdcitek PerunOdcitek);        
        [OperationContract]
        dcResult Get_InformativniIzracunPorabe(string smm, Int32 PaketID, String DatumOdcitka_Stari, String DatumOdcitka_Novi, Int32 PrikljucnaMoc, Int32? Kolicina_ET, Int32? Kolicina_DVT, Int32? Kolicina_DMT, Boolean eRacun, Boolean Trajnik, out List<dcPostavkaIzracuna> PostavkeObracuna, out Int32? prilogaa_id);
        [OperationContract]
        dcResult Get_InformativniIzracunPorabeOld(Int32 PaketID, String DatumOdcitka_Stari, String DatumOdcitka_Novi, Int32 PrikljucnaMoc, Int32? Kolicina_ET, Int32? Kolicina_DVT, Int32? Kolicina_DMT, Boolean eRacun, Boolean Trajnik, out List<dcPostavkaIzracuna> PostavkeObracuna);
        [OperationContract]
        dcResult Set_ObracunOdcitek(string smm, string datumOdcitka, out Int32? PrilogaA_ID);        
        [OperationContract]
        dcResult Set_ObracunOdcitekOld(Int32 MerilnoMesto, short DodajAkontacijo, out Int32? PrilogaA_ID);
        [OperationContract]
        dcResult Set_ObracunObrok(Int32 MerilnoMesto, Int32 Leto, Int32 Mesec, out Int32? PrilogaA_ID);
        [OperationContract]
        dcResult Set_StornirajFakturoZaPrilogoAID(Int32 PrilogaAID, Int32 StevilkaMerilnegaMesta);
        [OperationContract]
        dcResult Set_StornirajOdcitek(Int32 StevilkaMerilnegaMesta, String DatumOdcitka);
        [OperationContract]
        dcResult Set_DolociAkontacijskiNeakontacijski(Int32 StevilkaMerilnegaMesta, Boolean Neakontacijski);
        [OperationContract]
        dcResult Set_DolociVisinoAkontacije(Int32 StevilkaMerilnegaMesta, Decimal? MesecnoPovprecjeVT, Decimal? MesecnoPovprecjeMT, Decimal? MesecnoPovprecjeET);
            
        [OperationContract]
        dcResult Get_PrilogeAZaMerilnoMesto(Int32 MerilnoMesto, String DatumOd, String DatumDo, out KomunikatorEGP.ws_EG_PerunEG.dcParametriPrilogeAList ParametriPrilogeAList);
        [OperationContract]
        dcResult Get_PrilogoAZaPaketID(Int32 MerilnoMesto, Int32 PrilogaA_ID, out byte[] FakturaPrilogaA);

        [OperationContract]
        dcResult Set_StanjePlin(int MerilnoMesto, string DatumOdbirka, string StanjeET, Int32 tipOdcitka, string opomba);
        [OperationContract]
        dcResult Get_ZadnjiOdbirekPlin(int merilnoMesto, out dcPerunOdcitek odcitek);
        [OperationContract]
        dcResult Get_InformativniIzracunPorabePlin(int om, string stanje, out List<dcPostavkaIzracuna> PostavkeObracuna);
        /*
        [OperationContract]
        dcResult Get_FakturaOnDemandPdf(string stRacuna, out byte[] fakturaPDF);
        */ 
        [OperationContract]
        dcResult Set_OdpovedPogodbeODobavi(string smm, string sifraDobavitelja, string datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result);
        [OperationContract]
        dcResult Set_OdpovedPogodbeODobaviPreklic(string smm, string sifraDobavitelja, string datum, string kontaktnaOseba, string opomba, out bool result);
        
        [OperationContract]
        dcResult Get_NajdiZahteve(string smm, string vrstaZahteve, string statusZahteve, string datumOd, string datumDo, out dcEvidencaZahtevZahteva[] zahteve);
        
        [OperationContract]
        dcResult set_NeAkontacijski(string smm, bool neAkontacijski, out bool result);        
        [OperationContract]
        Int32 Test_KomunikatorEGP();
    }
}
