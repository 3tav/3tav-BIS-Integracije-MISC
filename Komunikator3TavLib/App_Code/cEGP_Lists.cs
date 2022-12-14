using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Komunikator3TavLib
{
    public class KomunikatorEG_Consts
    {
        public const int BIS_User_wsKomunikatorEG = -9001;
    }

    public enum eNacinPridobitveOdcitka
    {
        Dobavitelj = 7,
        Preun = 8
    }

    public enum eZCO_StoredProceduraAction
    {
        VpisNoveZahteve = 1,
        DopolnilDobavitelj = 2,
        PosodobitevVpisaneZahteve = 4,
        UmaknjenaZahteva = 9
    }

    public enum ewsActions
    {
        Perun_OddajaStanja = 1,
        EG_InteraktivniObracun = 2,
    }

    public enum ewsRequestStatus
    {
        ErrorUnknown = 10,
        ErrorBeforeExecute = 20,
        IncorrectInputData = 30,
        Executed = 90
    }

    public enum ewsResponseStatus
    {
        ErrorUnknown = 10,
        Finished_WithErrors = 85,
        Finished_NoErrors = 90
    }

    public enum ePartnerType
    {
        Lastnik = 1,
        Placnik = 2,
        Naslovnik = 3
    }

    public enum ewsPrivilegeForWebServiceID
    {
        wsPerun = 1,
        wsStoritveEG = 2,
        wsKlicniCenter = 3
    }


    public enum ewsPrivilegeForRoutineID
    {
        Get_MerilnoMesto = 1,
        Get_SeznamOdcitkov = 2,
        Get_ZadnjiOdcitek = 3,
        Get_LastnikZaMM = 4,
        Get_PlacnikZaMM = 5,
        Get_NaslovnikZaMM = 6,
        Get_PartnerjeZaMM = 7,

        Get_SaldoKontnaKartica = 10,
        Get_IzdaneFakture = 11,

        Get_PerunStanje = 12,
        Get_PrilogeAZaMerilnoMesto = 13,
        Get_PrilogoAZaPaketID = 14,
        Get_InformativniIzracunPorabe = 17,

        Get_DokumenteZaMerilnoMesto = 15,
        Get_DocumentsForPartner = 16,

        Set_VpisOdcitka = 1001,
        Set_InteraktivniObracun = 1002,
        Set_StornirajFakturoZaPrilogoAID = 1003,
        Set_StornirajOdcitek = 1004,
        Set_DolociAkontacijskiNeakontacijski = 1005,
        Set_DolociVisinoAkontacije = 1006
    }
}