using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Komunikator3TavLib.OdcitkiPlin;

namespace Komunikator3TavLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wsSporocanjeOdcitkov" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select wsSporocanjeOdcitkov.svc or wsSporocanjeOdcitkov.svc.cs at the Solution Explorer and start debugging.
    public class wsSporocanjeOdcitkov : IwsSporocanjeOdcitkov
    {        
        public wsSporocanjeOdcitkov()
        {
            cSettings.Fill_FromSettingsFile();            
            //LocalTables.Read_TableNames();
        }

        public string TestGetPodatkiMM(string smm)
        {
            sFunctionResult fr;

            //string testString = string.Empty;
            var ts = new StringBuilder(string.Format("Test Get_PodatkiMM za: {0};", smm));
            var statusString = string.Empty;

            // podatki MM iz baze
            dcMerilnoMesto dataMM;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
                                                
            int idOdjemnegaMesta = -1;
            fr = EGP_BIS.GetMerilnoMestoId(smm, out idOdjemnegaMesta);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                statusString = string.Format("Napaka GetMerilnoMestoId: {0};", cdcResult.Get_FromFunctionResult(fr).MSG);
            }
            else
            {
                fr = EGP_BIS.Get_MM(idOdjemnegaMesta, out dataMM);
                if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
                {
                    statusString = string.Format("Napaka GetPodatkiMM: {0};", cdcResult.Get_FromFunctionResult(fr).MSG);
                }
                else
                {
                    statusString = string.Format("id: {0}, naziv: {1}", idOdjemnegaMesta, dataMM.Naziv);
                }                                
            }
            ts.Append(statusString);
            ts.Append(cSettings.Get_3Tav_ConnString());
            return ts.ToString();                        
        }

        public string TestGetZadnjiOdbirek(string smm)
        {
            sFunctionResult fr;
            
            //string testString = string.Empty;
            var ts = new StringBuilder(string.Format("Test Get_ZadnjiOdcitek za: {0};", smm));
            var statusString = string.Empty;

            // zadnji odcitek
            var perunOdcitek = new dcPerunOdcitek();
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            
            fr = PerunEG.Get_ZadnjiOdcitekZaMerilnoMesto(smm, out perunOdcitek);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                statusString = string.Format("Napaka Get_ZadnjiOdcitek: {0};", cdcResult.Get_FromFunctionResult(fr).MSG);
            }
            else
            {
                statusString = string.Format("Datum odcitka: {0}; ET: {1}, VT: {2}, MT: {3}", perunOdcitek.DatumStanja, perunOdcitek.StanjeET, perunOdcitek.StanjeVT, perunOdcitek.StanjeMT);
            }
            return ts.Append(statusString).ToString();                                    
        }        

        public dcResult Set_StanjeNaPerun(Int32 MerilnoMesto, String DatumOdbirka, String StanjeET, String StanjeVT, String StanjeMT, Int32 NacinPridobitveOdcitka, Int32 Uporabnik, out Int32? PerunOdcitekID)
        {
            // krovna metoda za oddajo odčitka 
            sFunctionResult fr;

            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            PerunOdcitekID = null;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            if (!cCommon.ToDate(DatumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + DatumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Set_VpisOdcitka);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            // Sanitiranje inputa            
            Decimal? DecStanjeET = null, DecStanjeDVT = null, DecStanjeDMT = null;
            Decimal et = 0, vt = 0, mt = 0;

            if (Decimal.TryParse(StanjeET, out et))
                DecStanjeET = et;

            if (Decimal.TryParse(StanjeVT, out vt))
                DecStanjeDVT = vt;

            if (Decimal.TryParse(StanjeMT, out mt))
                DecStanjeDMT = mt;


            if (DecStanjeET != null)
            {
                if (DecStanjeET > 0)
                {
                    DecStanjeDMT = null;
                    DecStanjeDVT = null;
                    StanjeMT = null;
                    StanjeVT = null;
                }
            }

            if (DecStanjeDVT != null)
            {
                if (DecStanjeDVT > 0)
                {
                    DecStanjeET = null;
                    StanjeET = null;
                }
            }

            if (DecStanjeDMT != null)
            {
                if (DecStanjeDMT > 0)
                {
                    DecStanjeET = null;
                    StanjeET = null;
                }
            }

            if (StanjeET == null && StanjeMT == null && StanjeVT == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Vrednosti odčitka niso določene!", ""));
            }

            // po novem enoten klic web servisa, ne delam razlik med distribucijami
            fr = PerunEG.Set_VpisOdcitkaZaMerilnoMesto((Int32)DataMM.SODO, (Int32)DataMM.SODO_SMM, DatumOdbirka, 1 /*Redni odčitek*/, StanjeET, StanjeVT, StanjeMT);
            if ((fr.resBool) && (fr.resInteger == 0))
            {
                PerunOdcitekID = 999999;
            }

            if ((fr.resBool) && (fr.resInteger == 0))
            {
                /*
                Decimal? DecStanjeET = null;
                Decimal? DecStanjeDVT = null;
                Decimal? DecStanjeDMT = null;

                if ((StanjeET != null) && (StanjeET.Length > 0))
                {
                    DecStanjeET = Convert.ToDecimal(StanjeET);

                }
                else
                {
                    DecStanjeDVT = Convert.ToDecimal(StanjeVT);
                    DecStanjeDMT = Convert.ToDecimal(StanjeMT);
                }
                */
                cbie_sp bie_sp = new cbie_sp(cSettings.Get_3Tav_ConnString());
                fr = bie_sp.Set_VpisOdcitka_OdcitkiPerun(MerilnoMesto, (Int32)DataMM.SODO_SMM, DatumOdbirkaOdDatuma, DecStanjeET, DecStanjeDVT, DecStanjeDMT, "", Uporabnik, NacinPridobitveOdcitka);
            }
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_StanjeNaPerun(Int32 MerilnoMesto, String DatumOdbirka, String StanjeET, String StanjeVT, String StanjeMT, Int32 NacinPridobitveOdcitka, Int32 Uporabnik, String Vir, Int32 NacinOddaje, out Int32? PerunOdcitekID)
        {
            // krovna metoda za oddajo odčitka  + vir
            sFunctionResult fr;

            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            PerunOdcitekID = null;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            if (!cCommon.ToDate(DatumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + DatumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Set_VpisOdcitka);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            // Sanitiranje inputa            
            Decimal? DecStanjeET = null, DecStanjeDVT = null, DecStanjeDMT = null;
            Decimal et = 0, vt = 0, mt = 0;

            if (Decimal.TryParse(StanjeET, out et))
                DecStanjeET = et;

            if (Decimal.TryParse(StanjeVT, out vt))
                DecStanjeDVT = vt;

            if (Decimal.TryParse(StanjeMT, out mt))
                DecStanjeDMT = mt;

            
            if (DecStanjeET != null)
            {
                if (DecStanjeET > 0)
                {
                    DecStanjeDMT = null;
                    DecStanjeDVT = null;
                    StanjeMT = null;
                    StanjeVT = null;
                }
            }

            if (DecStanjeDVT != null)
            {
                if (DecStanjeDVT > 0)
                {
                    DecStanjeET = null;
                    StanjeET = null;
                }
            }

            if (DecStanjeDMT != null)
            {
                if (DecStanjeDMT > 0)
                {
                    DecStanjeET = null;
                    StanjeET = null;
                }
            }

            // po novem enoten klic web servisa, ne delam razlik med distribucijami
            PerunOdcitekID = 0;
            if (NacinOddaje != (int)NaciniOddaje.Cakalnica)
            {
                // oddaja v PERUN
                fr = PerunEG.Set_VpisOdcitkaZaMerilnoMesto((Int32)DataMM.SODO, (Int32)DataMM.SODO_SMM, DatumOdbirka, 1 /*Redni odčitek*/, StanjeET, StanjeVT, StanjeMT, Vir);
                if ((fr.resBool) && (fr.resInteger == 0))
                {
                    PerunOdcitekID = 999999;
                }               
            }
            else
            {
                fr.resBool = true; // kot bi perun vrnil uspeh
                fr.resInteger = 0;
                PerunOdcitekID = 999999;
            }

            if (NacinOddaje != (int)NaciniOddaje.Perun)
            {
                if ((fr.resBool) && (fr.resInteger == 0))
                {
                    // oddaja v čakalnico
                    var statusCakalnica = (NacinOddaje == (int)NaciniOddaje.Cakalnica ? StatusiCakalnica.Neobdelan : StatusiCakalnica.Evidencno);                    
                    cbie_sp bie_sp = new cbie_sp(cSettings.Get_3Tav_ConnString());
                    fr = bie_sp.Set_VpisOdcitka_OdcitkiPerun(MerilnoMesto, (Int32)DataMM.SODO_SMM, DatumOdbirkaOdDatuma, DecStanjeET, DecStanjeDVT, DecStanjeDMT, "", Uporabnik, NacinPridobitveOdcitka, Vir, statusCakalnica);
                    if (fr.resInteger != 0)
                    {
                        // error se hendla spodaj v Get_FromFunctionResult
                    }
                }
            }
            
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_StanjeNaPerun(Int32 MerilnoMesto, String DatumOdbirka, String StanjeET, String StanjeVT, String StanjeMT, Int32 NacinPridobitveOdcitka, Int32 Uporabnik, String Vir, out Int32? PerunOdcitekID)
        {

            return Set_StanjeNaPerun(MerilnoMesto, DatumOdbirka, StanjeET, StanjeVT, StanjeMT, NacinPridobitveOdcitka, Uporabnik, Vir, (Int32)NaciniOddaje.Privzeto, out PerunOdcitekID);

            // krovna metoda za oddajo odčitka  + vir
            sFunctionResult fr;

            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            PerunOdcitekID = null;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            if (!cCommon.ToDate(DatumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + DatumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Set_VpisOdcitka);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            // Sanitiranje inputa                
            Decimal? DecStanjeET = null, DecStanjeDVT = null, DecStanjeDMT = null;
            Decimal et = 0, vt = 0, mt = 0;

            if (Decimal.TryParse(StanjeET, out et))
                DecStanjeET = et;

            if (Decimal.TryParse(StanjeVT, out vt))
                DecStanjeDVT = vt;

            if (Decimal.TryParse(StanjeMT, out mt))
                DecStanjeDMT = mt;


            if (DecStanjeET != null)
            {
                if (DecStanjeET > 0)
                {
                    DecStanjeDMT = null;
                    DecStanjeDVT = null;
                    StanjeMT = null;
                    StanjeVT = null;
                }
            }

            if (DecStanjeDVT != null)
            {
                if (DecStanjeDVT > 0)
                {
                    DecStanjeET = null;
                    StanjeET = null;
                }
            }

            if (DecStanjeDMT != null)
            {
                if (DecStanjeDMT > 0)
                {
                    DecStanjeET = null;
                    StanjeET = null;
                }
            }

            // po novem enoten klic web servisa, ne delam razlik med distribucijami
            fr = PerunEG.Set_VpisOdcitkaZaMerilnoMesto((Int32)DataMM.SODO, (Int32)DataMM.SODO_SMM, DatumOdbirka, 1 /*Redni odčitek*/, StanjeET, StanjeVT, StanjeMT, Vir);
            if ((fr.resBool) && (fr.resInteger == 0))
            {
                PerunOdcitekID = 999999;
            }

            if ((fr.resBool) && (fr.resInteger == 0))
            {
                cbie_sp bie_sp = new cbie_sp(cSettings.Get_3Tav_ConnString());
                fr = bie_sp.Set_VpisOdcitka_OdcitkiPerun(MerilnoMesto, (Int32)DataMM.SODO_SMM, DatumOdbirkaOdDatuma, DecStanjeET, DecStanjeDVT, DecStanjeDMT, "", Uporabnik, NacinPridobitveOdcitka);
            }
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_StanjeNaPerun(string smm, String DatumOdbirka, String StanjeET, String StanjeVT, String StanjeMT, Int32 NacinPridobitveOdcitka, Int32 Uporabnik, out Int32? PerunOdcitekID)
        {

            // wrapper funkcija za klic tiste preko ID odjemnega mesta
            sFunctionResult fr;

            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            PerunOdcitekID = null;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());            
            if (!cCommon.ToDate(DatumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + DatumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }
            
            // tukaj ne grem s smm naprej, zato ker se po oddaji proži stored procedura z id kot argument
            int idOdjemnegaMesta = -1;
            fr = EGP_BIS.GetMerilnoMestoId(smm, out idOdjemnegaMesta);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            
            return Set_StanjeNaPerun(idOdjemnegaMesta, DatumOdbirka, StanjeET, StanjeVT, StanjeMT, NacinPridobitveOdcitka, Uporabnik, out PerunOdcitekID);
          
        }

        public dcResult Set_StanjeNaPerun(string smm, String DatumOdbirka, String StanjeET, String StanjeVT, String StanjeMT, Int32 NacinPridobitveOdcitka, Int32 Uporabnik, String Vir, Int32 NacinOddaje, out Int32? PerunOdcitekID)
        {
            // wrapper funkcija za klic tiste preko ID odjemnega mesta + vir
            sFunctionResult fr;

            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            PerunOdcitekID = null;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            if (!cCommon.ToDate(DatumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + DatumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            // tukaj ne grem s smm naprej, zato ker se po oddaji proži stored procedura z id kot argument
            int idOdjemnegaMesta = -1;
            fr = EGP_BIS.GetMerilnoMestoId(smm, out idOdjemnegaMesta);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return Set_StanjeNaPerun(idOdjemnegaMesta, DatumOdbirka, StanjeET, StanjeVT, StanjeMT, NacinPridobitveOdcitka, Uporabnik, Vir, NacinOddaje, out PerunOdcitekID);

        }

        public dcResult Set_StanjePlin(Int32 merilnoMesto, String datumOdbirka, String stanjeET, Int32 tipOdcitka, string opomba)
        {
            sFunctionResult fr;
            
            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            if (!cCommon.ToDate(datumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + datumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Set_VpisOdcitka);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM_Plin(merilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            if (!string.IsNullOrEmpty(stanjeET))
            {
                if (stanjeET.Contains(','))
                {
                    stanjeET = stanjeET.Replace(',', '.');
                }
            }
                      
            //decimal stanje = 0;
            //if (decimal.TryParse(stanjeET, out stanje) == false)
            //    throw new Exception("Stanje mora biti večje od 0!");

            OdcitkiPlinService svc = new OdcitkiPlinService();
            OdcitekPlin odcitek = new OdcitekPlin(merilnoMesto, DatumOdbirkaOdDatuma, stanjeET);

            odcitek.TipOdcitka = tipOdcitka;
            odcitek.Opomba = opomba;
           
            try
            {
                fr = svc.VnosOdcitka(odcitek);
            }
            catch (Exception ex)
            {
                fr = cFunctionResult.Set(false, (int)efrErrorCodes.SQLError, "Vnos odčitka", ex.Message, "");
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_StanjePlin(Int32 merilnoMesto, String datumOdbirka, String stanjeET, Int32 tipOdcitka, string opomba, string vir)
        {
            sFunctionResult fr;



            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            if (!cCommon.ToDate(datumOdbirka, out DatumOdbirkaOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + datumOdbirka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Set_VpisOdcitka);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM_Plin(merilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }


            if (!string.IsNullOrEmpty(stanjeET))
            {
                if (stanjeET.Contains(','))
                {
                    stanjeET = stanjeET.Replace(',', '.');
                }
            }

            //decimal stanje = 0;
            //if (Decimal.TryParse(stanjeET, out stanje) == false)
            //    throw new Exception("Stanje mora biti večje od 0!");

            OdcitkiPlinService svc = new OdcitkiPlinService();
            OdcitekPlin odcitek = new OdcitekPlin(merilnoMesto, DatumOdbirkaOdDatuma, stanjeET);

            odcitek.TipOdcitka = tipOdcitka;
            odcitek.Opomba = opomba;

            try
            {
                fr = svc.VnosOdcitka(odcitek, vir);
            }
            catch (Exception ex)
            {
                fr = cFunctionResult.Set(false, (int)efrErrorCodes.SQLError, "Vnos odčitka", ex.Message, "");
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_ZadnjiOdbirekPlin(Int32 merilnoMesto, out dcPerunOdcitek odcitek)
        {
            sFunctionResult fr;
            DateTime DatumOdbirkaOdDatuma = System.DateTime.Now;
            odcitek = null;

            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Get_ZadnjiOdcitek);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM_Plin(merilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            OdcitkiPlinService svc = new OdcitkiPlinService();

            try
            {
                fr = svc.GetZadnjiOdcitek(merilnoMesto, out odcitek);
            }
            catch (Exception ex)
            {
                fr = cFunctionResult.Set(false, (int)efrErrorCodes.SQLError, "Vnos odčitka", ex.Message, "");
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }
        
        public dcResult Get_InformativniIzracunPorabePlin(Int32 om, string stanje, out List<dcPostavkaIzracuna> PostavkeObracuna)
        {  
            sFunctionResult fr = new sFunctionResult();
            KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList SeznamPostavkaIzracuna;
            PostavkeObracuna = new List<dcPostavkaIzracuna>(0);
            Double KolicinaEnergijeSkupaj = 0;
            cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());


            if (!string.IsNullOrEmpty(stanje))
            {
                if (stanje.Contains(','))
                {
                    stanje = stanje.Replace(',', '.');
                }
            }
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Get_InformativniIzracunPorabe);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            
            using (SqlConnection conn = new SqlConnection(cSettings.Get_3Tav_ConnString()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "bis_PlinInformativniObracun";
                    cmd.Parameters.AddWithValue("@om", om);
                    cmd.Parameters.AddWithValue("@stanje", stanje);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            dcPostavkaIzracuna p = new dcPostavkaIzracuna();
                            p.PostavkaCenikID = (rdr.IsDBNull(0) ? (int?)null : (int?)rdr.GetInt32(0));
                            p.PostavkaNaziv = rdr.GetString(1);
                            p.PovprecjeNaDan = (rdr.IsDBNull(2) ? (double?)null : (double?)rdr.GetDecimal(2));
                            p.SteviloDni = (rdr.IsDBNull(3) ? (int?)null : (int?)rdr.GetInt32(3));
                            p.ObdobjeOd = (rdr.IsDBNull(4) ? (DateTime?)null : (DateTime?)rdr.GetDateTime(4));
                            p.ObdobjeDo = (rdr.IsDBNull(5) ? (DateTime?)null : (DateTime?)rdr.GetDateTime(5));
                            p.Kolicina = (rdr.IsDBNull(6) ? (double?)null : (double?)rdr.GetDecimal(6));
                            p.CenaNaEnoto = (rdr.IsDBNull(7) ? (double?)null : (double?)rdr.GetDecimal(7));
                            p.StopnjaDDV = (rdr.IsDBNull(8) ? (double?)null : (double?)rdr.GetDecimal(8));
                            p.ZnesekBrezDDV = (rdr.IsDBNull(9) ? (double?)null : (double?)rdr.GetDecimal(9));
                            p.PostavkaOznaka = (rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10).Trim());
                            PostavkeObracuna.Add(p);
                        }                    
                    }
                }            
            }
                                                                
            return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, 0, string.Empty, string.Empty, string.Empty));
        }

        public dcResult Set_OpomboNaStanjaNaPerun(Int32 PerunOdcitekID, String Opomba)
        {
            sFunctionResult fr;

            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Set_VpisOdcitka);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            cInf_StenjaStevca Inf_StenjaStevca = new cInf_StenjaStevca();
            fr = Inf_StenjaStevca.Send_OpomboNaStanjaNaPerun(PerunOdcitekID,
                                                             Opomba);
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_ZadnjiOdbirekZaMerilnoMesto(Int32 MerilnoMesto, out dcPerunOdcitek PerunOdcitek)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            PerunOdcitek = new dcPerunOdcitek();
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Get_PerunStanje);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            var sodo = (Int32)DataMM.SODO;
            var odjemnoMestoId = (Int32)DataMM.SODO_SMM;

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Get_ZadnjiOdcitekZaMerilnoMesto(sodo, odjemnoMestoId, out PerunOdcitek);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
           
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_ZadnjiOdbirekZaMerilnoMesto(string smm, out dcPerunOdcitek PerunOdcitek)
        {
            sFunctionResult fr;            
            PerunOdcitek = new dcPerunOdcitek();
           
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Get_ZadnjiOdcitekZaMerilnoMesto(smm, out PerunOdcitek);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_ZadnjiOdbirekZaMerilnoMestoVir(string smm, string vir, out dcPerunOdcitek perunOdcitek)
        {
            sFunctionResult fr;
           
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());

            perunOdcitek = new dcPerunOdcitek();
        
            // step 1: čakalnica
            //PerunOdcitek =  EGP_BIS.
            fr = EGP_BIS.GetZadnjiOdcitekCakalnica(smm, out perunOdcitek);
            if (fr.resInteger != (int)efrErrorCodes.OK)
                throw new Exception(string.Format("{0} [L1]", fr.resError));

            if (perunOdcitek != null)
                return cdcResult.Get_FromFunctionResult(fr);

            // step 2: perun cache
            //fr = EGP_BIS.GetZadnjiOdcitekCache(smm, out perunOdcitek);
            //if (fr.resInteger != (int)efrErrorCodes.OK)
            //    throw new Exception(string.Format("{0} [L2]", fr.resError));

            //if (perunOdcitek != null)
            //    return cdcResult.Get_FromFunctionResult(fr);

            // step 3: dejanski klic ws
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Get_ZadnjiOdcitekZaMerilnoMesto(smm, out perunOdcitek);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_PovprecnoPoraboZaMerilnoMestoOld(Int32 MerilnoMesto, out dcPovprecnaPoraba PovprecnaPoraba)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            PovprecnaPoraba = new dcPovprecnaPoraba();
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Get_PerunStanje);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }

            dcOdcitek Odcitek = new dcOdcitek();

            fr = EGP_BIS.Get_ZadnjiOdcitek((Int32)DataMM.SMM, out Odcitek);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            PovprecnaPoraba.SMM = (Int32)DataMM.SMM;
            if (Odcitek.PorabaNaDanDVT != null)
            {
                PovprecnaPoraba.PorabaVT = Math.Max((double)(Odcitek.PorabaNaDanDVT ?? 0), 1);
                PovprecnaPoraba.PorabaMT = Math.Max((double)(Odcitek.PorabaNaDanDMT ?? 0), 1); ;
                PovprecnaPoraba.PorabaET = PovprecnaPoraba.PorabaVT + PovprecnaPoraba.PorabaMT;
            }
            else
            {
                PovprecnaPoraba.PorabaET = Math.Max((double)(Odcitek.PorabaNaDanET ?? 0), 1);
                PovprecnaPoraba.PorabaVT = Math.Max(0.6 * (double)PovprecnaPoraba.PorabaET, 1);
                PovprecnaPoraba.PorabaMT = Math.Max(0.4 * (double)PovprecnaPoraba.PorabaET, 1); ;
            }
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_PovprecnoPoraboZaMerilnoMesto(Int32 MerilnoMesto, out dcPovprecnaPoraba PovprecnaPoraba)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            PovprecnaPoraba = new dcPovprecnaPoraba();
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Get_PerunStanje);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }

            dcOdcitek Odcitek = new dcOdcitek();

            fr = EGP_BIS.Get_ZadnjiOdcitek((Int32)DataMM.SMM, out Odcitek);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            PovprecnaPoraba.SMM = (Int32)DataMM.SMM;
            if (Odcitek.PorabaNaDanDVT != null)
            {
                PovprecnaPoraba.PorabaVT = Math.Max((double)(Odcitek.PorabaNaDanDVT ?? 0), 1);
                PovprecnaPoraba.PorabaMT = Math.Max((double)(Odcitek.PorabaNaDanDMT ?? 0), 1); ;
                PovprecnaPoraba.PorabaET = PovprecnaPoraba.PorabaVT + PovprecnaPoraba.PorabaMT;
            }
            else
            {
                PovprecnaPoraba.PorabaET = Math.Max((double)(Odcitek.PorabaNaDanET ?? 0), 1);
                PovprecnaPoraba.PorabaVT = Math.Max(0.6 * (double)PovprecnaPoraba.PorabaET, 1);
                PovprecnaPoraba.PorabaMT = Math.Max(0.4 * (double)PovprecnaPoraba.PorabaET, 1); ;
            }
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_OdbirekZaMerilnoMestoInDatum(Int32 MerilnoMesto, DateTime DatumStanja, out dcPerunOdcitek PerunOdcitek)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            PerunOdcitek = new dcPerunOdcitek();
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Get_PerunStanje);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }

            cInf_StenjaStevca Inf_StenjaStevca = new cInf_StenjaStevca();
            fr = Inf_StenjaStevca.Get_OdbirekZaMerilnoMestoInDatum((short)DataMM.SODO,
                                                                   (Int32)DataMM.SODO_SMM,
                                                                   DatumStanja,
                                                                   out PerunOdcitek);
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_PodatkiMM(string smm, out Perun3WsLib.PridobivanjePodatkovMMService.PodatkiMerilnegaMesta podatkiMM)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            podatkiMM = null;
            //dcMerilnoMesto DataMM;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsPerun, ewsPrivilegeForRoutineID.Get_PerunStanje);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            /*
            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            var sodo = (Int32)DataMM.SODO;
            var odjemnoMestoId = (Int32)DataMM.SODO_SMM;
            */
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            //fr = PerunEG.Get_ZadnjiOdcitekZaMerilnoMesto(sodo, odjemnoMestoId, out PerunOdcitek);
            fr = PerunEG.Get_PodatkiMM(smm, out podatkiMM);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_PDP(string smm, out dcPovprecnaPorabaPerun3 pdp)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            pdp = null;
          
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");            

            fr = PerunEG.Get_PDP(smm, out pdp);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Vnos_PDP(dcPovprecnaPorabaPerun3 pdp)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());            
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            fr = PerunEG.Vnos_PDP(pdp);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Vnos_PDP_Cakalnica(dcPovprecnaPorabaPerun3 pdp)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            fr = PerunEG.Vnos_PDP_Cakalnica(pdp);
            if ((!fr.resBool) || (fr.resInteger != 0))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }


        public dcResult Get_ObracunskiPaketZaMerilnoMesto(Int32 MerilnoMesto, DateTime Datum, Int32? PaketID)
        {
            sFunctionResult fr;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            PaketID = null;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;
            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            fr = EGP_BIS.Get_PaketZaOdjemnoMesto((Int32)DataMM.SODO_SMM, Datum, out PaketID);
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_InformativniIzracunPorabe2(string smm, Int32 paketID, String datumOdcitkaStari, String datumOdcitkaNovi, Int32 obracunskaMoc, Int32? kolicinaET, Int32? kolicinaDVT, Int32? kolicinaDMT, Boolean eRacun, Boolean trajnik, out List<dcPostavkaIzracuna> postavkeObracuna, out Int32? prilogaa_id)
        {
            sFunctionResult fr;
            KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList seznamPostavkaIzracuna;
            postavkeObracuna = new List<dcPostavkaIzracuna>(0);
            
            cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Get_InformativniIzracunPorabe);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            
            // klic perun servisa
            cPerun_EG Perun_EG = new cPerun_EG("EGPws", "3gpws$");
            fr = Perun_EG.Get_InformativniIzracunPorabe(smm, datumOdcitkaStari,
                                                        datumOdcitkaNovi,
                                                        obracunskaMoc,
                                                        kolicinaET,
                                                        kolicinaDVT,
                                                        kolicinaDMT,
                                                        out seznamPostavkaIzracuna,
                                                        out prilogaa_id);

            if (fr.resBool == false)
                throw new Exception(fr.resError);

            if (prilogaa_id.HasValue == false)
                throw new Exception("Napaka pri pridobivanju informativnega izračuna! PrilogaA ID je NULL");

            var datumOdcitkaNoviDt = Convert.ToDateTime(datumOdcitkaNovi);
            var datumOdcitkaStariDt = Convert.ToDateTime(datumOdcitkaStari);
            
            var obracun = datumOdcitkaNoviDt.Year * 100 + datumOdcitkaNoviDt.Month;
            var dt = EGP_BIS.GetInformativniIzracun(datumOdcitkaNoviDt, obracun, datumOdcitkaNoviDt, datumOdcitkaNoviDt, -1, prilogaa_id.Value);
            postavkeObracuna = new List<dcPostavkaIzracuna>(0);
                     
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var p = new dcPostavkaIzracuna();

                var znesekNetto = Convert.ToDouble(dt.Rows[i]["neto"]);
                var steviloDni = (datumOdcitkaNoviDt - datumOdcitkaStariDt).Days;
                var povprecje = (double)(steviloDni > 0 ? (znesekNetto / steviloDni) : znesekNetto);

                p.PostavkaCenikID = 0;
                p.PostavkaNaziv = Convert.ToString(dt.Rows[i]["naziv"]);
                p.PostavkaOznaka = Convert.ToString(dt.Rows[i]["analitska_oznaka"]);
                p.PovprecjeNaDan = povprecje;
                p.SteviloDni = steviloDni;
                p.ObdobjeOd = datumOdcitkaStariDt;
                p.ObdobjeDo = datumOdcitkaNoviDt;
                p.Kolicina = Convert.ToDouble(dt.Rows[i]["kolicina"]);
                p.CenaNaEnoto = Convert.ToDouble(dt.Rows[i]["cena"]);
                p.StopnjaDDV = Convert.ToDouble(dt.Rows[i]["davcna_stopnja"]);
                p.ZnesekBrezDDV = znesekNetto;

                postavkeObracuna.Add(p);               
            }

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_InformativniIzracunPorabe(string smm, Int32 paketID, String datumOdcitkaStari, String datumOdcitkaNovi, Int32 obracunskaMoc, Int32? kolicinaET, Int32? kolicinaDVT, Int32? kolicinaDMT, Boolean eRacun, Boolean trajnik, out List<dcPostavkaIzracuna> postavkeObracuna, out Int32? prilogaa_id)
        {
            sFunctionResult fr;
            KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList seznamPostavkaIzracuna;
            postavkeObracuna = new List<dcPostavkaIzracuna>(0);
            Double KolicinaEnergijeSkupaj = 0;
            cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Get_InformativniIzracunPorabe);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            
            // klic perun servisa
            cPerun_EG Perun_EG = new cPerun_EG("EGPws", "3gpws$");
            fr = Perun_EG.Get_InformativniIzracunPorabe(smm, datumOdcitkaStari,
                                                        datumOdcitkaNovi,
                                                        obracunskaMoc,
                                                        kolicinaET,
                                                        kolicinaDVT,
                                                        kolicinaDMT,
                                                        out seznamPostavkaIzracuna,
                                                        out prilogaa_id);
            
            if (seznamPostavkaIzracuna != null)
            {
                
                for (Int32 i = 0; i < seznamPostavkaIzracuna.Data.Length; i++)
                {
                    var p = new dcPostavkaIzracuna();
                    p.PostavkaCenikID = seznamPostavkaIzracuna.Data[i].PostavkaCenikID;
                    p.PostavkaNaziv = seznamPostavkaIzracuna.Data[i].PostavkaNaziv;
                    p.PovprecjeNaDan = seznamPostavkaIzracuna.Data[i].PovprecjeNaDan;
                    p.SteviloDni = seznamPostavkaIzracuna.Data[i].SteviloDni;
                    p.ObdobjeOd = seznamPostavkaIzracuna.Data[i].ObdobjeOd;
                    p.ObdobjeDo = seznamPostavkaIzracuna.Data[i].ObdobjeDo;
                    p.Kolicina = seznamPostavkaIzracuna.Data[i].Kolicina;
                    p.CenaNaEnoto = seznamPostavkaIzracuna.Data[i].CenaNaEnoto;
                    p.StopnjaDDV = seznamPostavkaIzracuna.Data[i].StopnjaDDV;
                    p.ZnesekBrezDDV = seznamPostavkaIzracuna.Data[i].ZnesekBrezDDV;
                    postavkeObracuna.Add(p);
                }

                eTarifaZaCenik Tarifa;
                String TarifaNaziv;

                Double? CenaNaEnoto;

                for (Int32 i = 0; i < postavkeObracuna.Count; i++)
                {
                    Tarifa = eTarifaZaCenik.SeNeZaracunava;
                    TarifaNaziv = "";
                    if (postavkeObracuna[i].PostavkaCenikID == 3)
                    {
                        Tarifa = eTarifaZaCenik.VT;
                        TarifaNaziv = "Energija VT";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)postavkeObracuna[i].Kolicina;
                    }
                    else if (postavkeObracuna[i].PostavkaCenikID == 4)
                    {
                        Tarifa = eTarifaZaCenik.VT;
                        TarifaNaziv = "Energija VT";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)postavkeObracuna[i].Kolicina;
                    }
                    else if (postavkeObracuna[i].PostavkaCenikID == 5)
                    {
                        Tarifa = eTarifaZaCenik.MT;
                        TarifaNaziv = "Energija MT";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)postavkeObracuna[i].Kolicina;
                    }
                    else if (postavkeObracuna[i].PostavkaCenikID == 6)
                    {
                        Tarifa = eTarifaZaCenik.ET;
                        TarifaNaziv = "Energija ET";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)postavkeObracuna[i].Kolicina;
                    }
                    if (Tarifa != eTarifaZaCenik.SeNeZaracunava)
                    {
                        var p = new dcPostavkaIzracuna();
                        EGP_BIS.Get_CenoZaPaket(null, (DateTime)postavkeObracuna[i].ObdobjeDo, paketID, Tarifa, out CenaNaEnoto);
                        p.PostavkaCenikID = (Int32)Tarifa;
                        p.PovprecjeNaDan = postavkeObracuna[i].PovprecjeNaDan;
                        p.PostavkaNaziv = TarifaNaziv;
                        p.PovprecjeNaDan = postavkeObracuna[i].PovprecjeNaDan;
                        p.SteviloDni = postavkeObracuna[i].SteviloDni;
                        p.ObdobjeOd = postavkeObracuna[i].ObdobjeOd;
                        p.ObdobjeDo = postavkeObracuna[i].ObdobjeDo;
                        p.CenaNaEnoto = CenaNaEnoto;
                        p.Kolicina = postavkeObracuna[i].Kolicina;
                        p.StopnjaDDV = postavkeObracuna[i].StopnjaDDV;
                        p.ZnesekBrezDDV = CenaNaEnoto * p.Kolicina;
                        postavkeObracuna.Add(p);
                    }
                }
                //P67
                var pos = new dcPostavkaIzracuna();
                EGP_BIS.Get_CenoZaP67(out CenaNaEnoto);
                pos.PostavkaCenikID = 11;
                pos.PovprecjeNaDan = 0;
                pos.PostavkaNaziv = "Prispevek po 67. členu EZ";
                pos.PovprecjeNaDan = 0;
                pos.SteviloDni = postavkeObracuna[0].SteviloDni;
                pos.ObdobjeOd = postavkeObracuna[0].ObdobjeOd;
                pos.ObdobjeDo = postavkeObracuna[0].ObdobjeDo;
                pos.CenaNaEnoto = CenaNaEnoto;
                pos.Kolicina = KolicinaEnergijeSkupaj;
                pos.StopnjaDDV = 22;
                pos.ZnesekBrezDDV = CenaNaEnoto * pos.Kolicina;
                postavkeObracuna.Add(pos);
                
                //Trosarina
                pos = new dcPostavkaIzracuna();
                EGP_BIS.Get_CenoZaTrosarina(out CenaNaEnoto);
                pos.PostavkaCenikID = 11;
                pos.PovprecjeNaDan = 0;
                pos.PostavkaNaziv = "Trošarina";
                pos.PovprecjeNaDan = 0;
                pos.SteviloDni = postavkeObracuna[0].SteviloDni;
                pos.ObdobjeOd = postavkeObracuna[0].ObdobjeOd;
                pos.ObdobjeDo = postavkeObracuna[0].ObdobjeDo;
                pos.CenaNaEnoto = CenaNaEnoto;
                pos.Kolicina = KolicinaEnergijeSkupaj;
                pos.StopnjaDDV = 22;
                pos.ZnesekBrezDDV = CenaNaEnoto * pos.Kolicina;
                postavkeObracuna.Add(pos);
                //Mesečno nadomestilo
                if (!eRacun)
                {
                    pos = new dcPostavkaIzracuna();
                    EGP_BIS.Get_CenoZaMesecnoNadomestilo(trajnik, out CenaNaEnoto);
                    pos.PostavkaCenikID = 11;
                    pos.PovprecjeNaDan = 0;
                    pos.PostavkaNaziv = "Mesečno nadomestilo";
                    pos.PovprecjeNaDan = 0;
                    pos.SteviloDni = postavkeObracuna[0].SteviloDni;
                    pos.ObdobjeOd = postavkeObracuna[0].ObdobjeOd;
                    pos.ObdobjeDo = postavkeObracuna[0].ObdobjeDo;
                    pos.CenaNaEnoto = CenaNaEnoto;
                    pos.Kolicina = 1;
                    pos.StopnjaDDV = 22;
                    pos.ZnesekBrezDDV = CenaNaEnoto * pos.Kolicina;
                    postavkeObracuna.Add(pos);
                }
            }
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_InformativniIzracunPorabeOld(Int32 PaketID, String DatumOdcitka_Stari, String DatumOdcitka_Novi, Int32 ObracunskaMoc, Int32? Kolicina_ET, Int32? Kolicina_DVT, Int32? Kolicina_DMT, Boolean eRacun, Boolean Trajnik, out List<dcPostavkaIzracuna> PostavkeObracuna)
        {
            sFunctionResult fr;
            KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList SeznamPostavkaIzracuna;
            PostavkeObracuna = new List<dcPostavkaIzracuna>(0);
            Double KolicinaEnergijeSkupaj = 0;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Get_InformativniIzracunPorabe);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            cPerun_EG Perun_EG = new cPerun_EG("EGPws", "3gpws$");
            fr = Perun_EG.Get_InformativniIzracunPorabeOld(DatumOdcitka_Stari,
                                                        DatumOdcitka_Novi,
                                                        ObracunskaMoc,
                                                        Kolicina_ET,
                                                        Kolicina_DVT,
                                                        Kolicina_DMT,
                                                        out SeznamPostavkaIzracuna);
            if (SeznamPostavkaIzracuna != null)
            {
                dcPostavkaIzracuna Postavka;
                for (Int32 i = 0; i < SeznamPostavkaIzracuna.SteviloPostavk; i++)
                {
                    Postavka = new dcPostavkaIzracuna();

                    Postavka.PostavkaCenikID = SeznamPostavkaIzracuna.Data[i].PostavkaCenikID;
                    Postavka.PostavkaNaziv = SeznamPostavkaIzracuna.Data[i].PostavkaNaziv;
                    Postavka.PovprecjeNaDan = SeznamPostavkaIzracuna.Data[i].PovprecjeNaDan;
                    Postavka.SteviloDni = SeznamPostavkaIzracuna.Data[i].SteviloDni;
                    Postavka.ObdobjeOd = SeznamPostavkaIzracuna.Data[i].ObdobjeOd;
                    Postavka.ObdobjeDo = SeznamPostavkaIzracuna.Data[i].ObdobjeDo;
                    Postavka.Kolicina = SeznamPostavkaIzracuna.Data[i].Kolicina;
                    Postavka.CenaNaEnoto = SeznamPostavkaIzracuna.Data[i].CenaNaEnoto;
                    Postavka.StopnjaDDV = SeznamPostavkaIzracuna.Data[i].StopnjaDDV;
                    Postavka.ZnesekBrezDDV = SeznamPostavkaIzracuna.Data[i].ZnesekBrezDDV;
                    PostavkeObracuna.Add(Postavka);
                }

                eTarifaZaCenik Tarifa;
                String TarifaNaziv;

                Double? CenaNaEnoto;

                for (Int32 i = 0; i < PostavkeObracuna.Count; i++)
                {
                    Tarifa = eTarifaZaCenik.SeNeZaracunava;
                    TarifaNaziv = "";
                    if (PostavkeObracuna[i].PostavkaCenikID == 3)
                    {
                        Tarifa = eTarifaZaCenik.VT;
                        TarifaNaziv = "Energija VT";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)PostavkeObracuna[i].Kolicina;
                    }
                    else if (PostavkeObracuna[i].PostavkaCenikID == 4)
                    {
                        Tarifa = eTarifaZaCenik.VT;
                        TarifaNaziv = "Energija VT";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)PostavkeObracuna[i].Kolicina;
                    }
                    else if (PostavkeObracuna[i].PostavkaCenikID == 5)
                    {
                        Tarifa = eTarifaZaCenik.MT;
                        TarifaNaziv = "Energija MT";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)PostavkeObracuna[i].Kolicina;
                    }
                    else if (PostavkeObracuna[i].PostavkaCenikID == 6)
                    {
                        Tarifa = eTarifaZaCenik.ET;
                        TarifaNaziv = "Energija ET";
                        KolicinaEnergijeSkupaj = KolicinaEnergijeSkupaj + (Double)PostavkeObracuna[i].Kolicina;
                    }
                    if (Tarifa != eTarifaZaCenik.SeNeZaracunava)
                    {
                        Postavka = new dcPostavkaIzracuna();
                        EGP_BIS.Get_CenoZaPaket(null, (DateTime)PostavkeObracuna[i].ObdobjeDo, PaketID, Tarifa, out CenaNaEnoto);
                        Postavka.PostavkaCenikID = (Int32)Tarifa;
                        Postavka.PovprecjeNaDan = PostavkeObracuna[i].PovprecjeNaDan;
                        Postavka.PostavkaNaziv = TarifaNaziv;
                        Postavka.PovprecjeNaDan = PostavkeObracuna[i].PovprecjeNaDan;
                        Postavka.SteviloDni = PostavkeObracuna[i].SteviloDni;
                        Postavka.ObdobjeOd = PostavkeObracuna[i].ObdobjeOd;
                        Postavka.ObdobjeDo = PostavkeObracuna[i].ObdobjeDo;
                        Postavka.CenaNaEnoto = CenaNaEnoto;
                        Postavka.Kolicina = PostavkeObracuna[i].Kolicina;
                        Postavka.StopnjaDDV = PostavkeObracuna[i].StopnjaDDV;
                        Postavka.ZnesekBrezDDV = CenaNaEnoto * Postavka.Kolicina;
                        PostavkeObracuna.Add(Postavka);
                    }
                }
                //P67
                Postavka = new dcPostavkaIzracuna();
                EGP_BIS.Get_CenoZaP67(out CenaNaEnoto);
                Postavka.PostavkaCenikID = 11;
                Postavka.PovprecjeNaDan = 0;
                Postavka.PostavkaNaziv = "Prispevek po 67. členu EZ";
                Postavka.PovprecjeNaDan = 0;
                Postavka.SteviloDni = PostavkeObracuna[0].SteviloDni;
                Postavka.ObdobjeOd = PostavkeObracuna[0].ObdobjeOd;
                Postavka.ObdobjeDo = PostavkeObracuna[0].ObdobjeDo;
                Postavka.CenaNaEnoto = CenaNaEnoto;
                Postavka.Kolicina = KolicinaEnergijeSkupaj;
                Postavka.StopnjaDDV = 22;
                Postavka.ZnesekBrezDDV = CenaNaEnoto * Postavka.Kolicina;
                PostavkeObracuna.Add(Postavka);
                //Trosarina
                Postavka = new dcPostavkaIzracuna();
                EGP_BIS.Get_CenoZaTrosarina(out CenaNaEnoto);
                Postavka.PostavkaCenikID = 11;
                Postavka.PovprecjeNaDan = 0;
                Postavka.PostavkaNaziv = "Trošarina";
                Postavka.PovprecjeNaDan = 0;
                Postavka.SteviloDni = PostavkeObracuna[0].SteviloDni;
                Postavka.ObdobjeOd = PostavkeObracuna[0].ObdobjeOd;
                Postavka.ObdobjeDo = PostavkeObracuna[0].ObdobjeDo;
                Postavka.CenaNaEnoto = CenaNaEnoto;
                Postavka.Kolicina = KolicinaEnergijeSkupaj;
                Postavka.StopnjaDDV = 22;
                Postavka.ZnesekBrezDDV = CenaNaEnoto * Postavka.Kolicina;
                PostavkeObracuna.Add(Postavka);
                //Mesečno nadomestilo
                if (!eRacun)
                {
                    Postavka = new dcPostavkaIzracuna();
                    EGP_BIS.Get_CenoZaMesecnoNadomestilo(Trajnik, out CenaNaEnoto);
                    Postavka.PostavkaCenikID = 11;
                    Postavka.PovprecjeNaDan = 0;
                    Postavka.PostavkaNaziv = "Mesečno nadomestilo";
                    Postavka.PovprecjeNaDan = 0;
                    Postavka.SteviloDni = PostavkeObracuna[0].SteviloDni;
                    Postavka.ObdobjeOd = PostavkeObracuna[0].ObdobjeOd;
                    Postavka.ObdobjeDo = PostavkeObracuna[0].ObdobjeDo;
                    Postavka.CenaNaEnoto = CenaNaEnoto;
                    Postavka.Kolicina = 1;
                    Postavka.StopnjaDDV = 22;
                    Postavka.ZnesekBrezDDV = CenaNaEnoto * Postavka.Kolicina;
                    PostavkeObracuna.Add(Postavka);
                }
            }
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_ObracunOdcitek(string smm, string datumOdcitka, out Int32? PrilogaA_ID)
        {
            sFunctionResult fr;
            PrilogaA_ID = null;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_InteraktivniObracun);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
          
            /*
            fr = EGP_BIS.Get_MM(merilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + merilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + merilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }
            */
            DateTime datumOdcitkaDatum;
            if (!cCommon.ToDate(datumOdcitka, out datumOdcitkaDatum))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOdbirka: Napačen format datuma :" + datumOdcitka + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            fr = PerunEG.Set_ObracunOdcitek(smm, datumOdcitkaDatum, out PrilogaA_ID);
            
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_ObracunOdcitekOld(Int32 MerilnoMesto, short DodajAkontacijo, out Int32? PrilogaA_ID)
        {
            sFunctionResult fr;
            PrilogaA_ID = null;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_InteraktivniObracun);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Set_ObracunOdcitekOld((Int32)DataMM.SODO_SMM, (Int32)DataMM.SODO, DodajAkontacijo, out PrilogaA_ID);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_ObracunObrok(Int32 MerilnoMesto, Int32 Leto, Int32 Mesec, out Int32? PrilogaA_ID)
        {
            sFunctionResult fr;
            PrilogaA_ID = null;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_InteraktivniObracun);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Set_ObracunObrok((Int32)DataMM.SODO_SMM, (Int32)DataMM.SODO, Leto, Mesec, out PrilogaA_ID);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_StornirajFakturoZaPrilogoAID(Int32 PrilogaAID, Int32 StevilkaMerilnegaMesta)
        {
            sFunctionResult fr;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_StornirajFakturoZaPrilogoAID);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            fr = EGP_BIS.Get_MM(StevilkaMerilnegaMesta, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Set_StornirajFakturoZaPrilogoAID(PrilogaAID, (Int32)DataMM.SODO_SMM);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_StornirajOdcitek(Int32 StevilkaMerilnegaMesta, String DatumOdcitka)
        {
            sFunctionResult fr;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_StornirajOdcitek);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            fr = EGP_BIS.Get_MM(StevilkaMerilnegaMesta, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Set_StornirajOdcitek((Int32)DataMM.SODO_SMM, DatumOdcitka);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_DolociAkontacijskiNeakontacijski(Int32 StevilkaMerilnegaMesta, Boolean Neakontacijski)
        {
            sFunctionResult fr;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_DolociAkontacijskiNeakontacijski);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            fr = EGP_BIS.Get_MM(StevilkaMerilnegaMesta, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Set_DolociAkontacijskiNeakontacijski((Int32)DataMM.SODO_SMM, Neakontacijski);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Set_DolociVisinoAkontacije(Int32 StevilkaMerilnegaMesta, Decimal? MesecnoPovprecjeVT, Decimal? MesecnoPovprecjeMT, Decimal? MesecnoPovprecjeET)
        {
            sFunctionResult fr;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Set_DolociVisinoAkontacije);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}
            fr = EGP_BIS.Get_MM(StevilkaMerilnegaMesta, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + StevilkaMerilnegaMesta.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Set_DolociVisinoAkontacije((Int32)DataMM.SODO_SMM, MesecnoPovprecjeVT, MesecnoPovprecjeMT, MesecnoPovprecjeET);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_PrilogeAZaMerilnoMesto(Int32 MerilnoMesto, String DatumOd, String DatumDo, out KomunikatorEGP.ws_EG_PerunEG.dcParametriPrilogeAList ParametriPrilogeAList)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            DateTime DatumOdDatuma;
            DateTime? DatumDoDatuma = null;
            ParametriPrilogeAList = new KomunikatorEGP.ws_EG_PerunEG.dcParametriPrilogeAList();

            if (!cCommon.ToDate(DatumOd, out DatumOdDatuma))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOd: Napačen format datuma :" + DatumOd + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }
            if ((DatumDo != "") && (DatumDo != null))
            {
                if (!cCommon.ToDateNull(DatumDo, out DatumDoDatuma))
                {
                    return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumDo: Napačen format datuma :" + DatumDo + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
                }
            }
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Get_PrilogeAZaMerilnoMesto);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Get_PrilogeAZaMerilnoMesto((Int32)DataMM.SODO_SMM, (Int32)DataMM.SODO, DatumOdDatuma, DatumDoDatuma, out ParametriPrilogeAList);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_PrilogoAZaPaketID(Int32 MerilnoMesto, Int32 PrilogaA_ID, out byte[] FakturaPrilogaA)
        {
            sFunctionResult fr;
            cEGP_BIS EGP_BIS = new cEGP_BIS(cSettings.Get_3Tav_ConnString());
            dcMerilnoMesto DataMM;

            DateTime DatumOdcitkaOdDatuma = System.DateTime.Now;
            FakturaPrilogaA = null;
            //cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());
            //fr = Authentic.Check_Authentication(ewsPrivilegeForWebServiceID.wsStoritveEG, ewsPrivilegeForRoutineID.Get_PrilogoAZaPaketID);
            //if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            //{
            //    return cdcResult.Get_FromFunctionResult(fr);
            //}

            fr = EGP_BIS.Get_MM(MerilnoMesto, out DataMM);
            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return cdcResult.Get_FromFunctionResult(fr);
            }
            if (DataMM.SODO == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Šifra distribucije za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO_SMM == null)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", "Distributerjevo MM za SMM " + MerilnoMesto.ToString() + "ni najdena.", ""));
            }
            if (DataMM.SODO != cSettings.DistribucijaId)
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola podatkov", string.Format("Storitev je na voljo samo za merilna mesta distribucije {0}.", cSettings.DistribucijaId), ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.Get_PrilogoAZaPaketID((Int32)DataMM.SODO_SMM, PrilogaA_ID, out FakturaPrilogaA);

            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult MenjavaDob_OddajVlogo(dcMenjavaDobZahteva zahteva, out Int32? zahtevaId)
        {
            sFunctionResult fr;
            zahtevaId = null;

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.MenjavaDob_OddajVlogo(zahteva, out zahtevaId);

            return cdcResult.Get_FromFunctionResult(fr);
            
        }

        public dcResult NovaPoD(dcNovaPoD zahteva)
        {
            sFunctionResult fr = new sFunctionResult();
          

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.NovaPoD(zahteva);

            return cdcResult.Get_FromFunctionResult(fr);
            
        }

        public dcResult MenjavaDob_NajdiVlogo(string smm, string datumOd, string datumDo, string status, out List<dcMenjavaDobVloga> seznamVlog)
        {
            sFunctionResult fr;
            seznamVlog = null;

            DateTime datumOdDt, datumDoDt;

            if (!cCommon.ToDate(datumOd, out datumOdDt))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOd: Napačen format datuma :" + datumOd + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            if (!cCommon.ToDate(datumDo, out datumDoDt))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumDo: Napačen format datuma :" + datumDo + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }


            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.MenjavaDob_NajdiVlogo(smm, datumOdDt, datumDoDt, status, out seznamVlog);
            //fr = PerunEG.me

            return cdcResult.Get_FromFunctionResult(fr);

        }

        public dcResult Set_OdpovedPogodbeODobavi(string smm, string sifraDobavitelja, string datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result)
        {
            sFunctionResult fr;
            DateTime datumDt;
            result = false;

            if (!cCommon.ToDate(datum, out datumDt))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOd: Napačen format datuma :" + datum + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.OdpovedPogodbeODobavi(smm, sifraDobavitelja, datumDt, priloga, prilogaNaziv, prilogaKoncnica, out result);

            return cdcResult.Get_FromFunctionResult(fr);

        }

        public dcResult Set_OdpovedPogodbeODobaviOdjemalec(string smm, string sifraDobavitelja, string datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result)
        {
            sFunctionResult fr;
            DateTime datumDt;
            result = false;

            if (!cCommon.ToDate(datum, out datumDt))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOd: Napačen format datuma :" + datum + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.OdpovedPogodbeODobaviOdjemalec(smm, sifraDobavitelja, datumDt, priloga, prilogaNaziv, prilogaKoncnica, out result);

            return cdcResult.Get_FromFunctionResult(fr);

        }


        public dcResult Set_OdpovedPogodbeODobaviPreklic(string smm, string sifraDobavitelja, string datum, string kontaktnaOseba, string opomba, out bool result)
        {
            sFunctionResult fr;
            DateTime datumDt;
            result = false;

            if (!cCommon.ToDate(datum, out datumDt))
            {
                return cdcResult.Get_FromFunctionResult(cFunctionResult.Set(true, (int)efrErrorCodes.IncorrectFormatInputParameter, "Kontrola vhodnih parametrov", "DatumOd: Napačen format datuma :" + datum + " Pričakovan format yyyy-mm-dd ali dd.mm.yyyy", ""));
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.OdpovedPogodbeODobaviPreklic(smm, sifraDobavitelja, datumDt, kontaktnaOseba, opomba, out result);

            return cdcResult.Get_FromFunctionResult(fr);

        }

        public dcResult Set_PrviPriklop(dcNovaPoD vloga)
        {
            sFunctionResult fr;
            DateTime datumDt;
 
            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            fr = PerunEG.PrviPriklop(vloga);

            return cdcResult.Get_FromFunctionResult(fr);

        }


        public dcResult Get_NajdiZahteve(string smm, string vrstaZahteve, string statusZahteve, string datumOd, string datumDo, out dcEvidencaZahtevZahteva[] zahteve)
        {
            sFunctionResult fr;
            DateTime datumOdDt, datumDoDt;
            DateTime? datumOdDtNull, datumDoDtNull;
            zahteve = null;

            datumOdDtNull = null;
            datumDoDtNull = null;

            if (DateTime.TryParse(datumOd, out datumOdDt) == true)
            {
                datumOdDtNull = datumOdDt;
            }

            if (DateTime.TryParse(datumDo, out datumDoDt) == true)
            {
                datumDoDtNull = datumDoDt;
            }

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            Perun3WsLib.IskanjeZahteveStoritevService.EvidencaZahtevVrni[] zahtevePerun = null;
            List<dcEvidencaZahtevZahteva> dcZahteve = new List<dcEvidencaZahtevZahteva>();

            fr = PerunEG.Get_NajdiZahteve(smm, vrstaZahteve, statusZahteve, datumOdDtNull, datumDoDtNull, out zahtevePerun);
            if (fr.resBool == false)
                return cdcResult.Get_FromFunctionResult(fr);

            if (zahtevePerun == null)
                return cdcResult.Get_FromFunctionResult(fr);

            foreach (var z in zahtevePerun)
            {
                var dcz = new dcEvidencaZahtevZahteva();

                
                dcz.CasIzvedbeFieldSpecified = z.casIzvedbeSpecified;
                if (dcz.CasIzvedbeFieldSpecified)
                    dcz.CasIzvedbe = z.casIzvedbe;

                dcz.CasZahteveFieldSpecified = z.casZahteveSpecified;
                if (dcz.CasZahteveFieldSpecified)
                    dcz.CasZahteve = z.casZahteve;


                dcz.DatumOdgovoraFieldSpecified = z.datumOdgovoraSpecified;
                if (dcz.DatumOdgovoraFieldSpecified)
                    dcz.DatumOdgovora = z.datumOdgovora;

                dcz.DatumPredvideneIzvedbeFieldSpecified = z.datumPredvideneIzvedbeSpecified;
                if (dcz.DatumPredvideneIzvedbeFieldSpecified)
                    dcz.DatumPredvideneIzvedbe = z.datumPredvideneIzvedbe;
                
                dcz.DatumZahteve = z.datumZahteve;                
                dcz.EnotniIdentMM = z.enotniIdentMM;
                dcz.IdMM = z.idMM;
                dcz.IdStatusZahteve = z.idStatusZahteve;
                dcz.IdVrstaZahteve = z.idVrstaZahteve;
                dcz.IdZahteva = z.idZahteva;
                dcz.KontaktnaOseba = z.kontaktnaOseba;

                dcz.NadstandardFieldSpecified = z.nadstandardSpecified;
                if (dcz.NadstandardFieldSpecified)
                    dcz.Nadstandard = z.nadstandard;

                dcz.NazivDistributerja = z.nazivDistributerja;
                dcz.NazivDobavitelja = z.nazivDobavitelja;
                dcz.Opomba = z.opomba;
                dcz.OpombaDistributerja = z.opombaDistributerja;
                dcz.OznakaDistributerja = z.oznakaDistributerja;
                dcz.OznakaDobavitelja = z.oznakaDobavitelja;
                dcz.StatusZahteve = z.statusZahteve;
                dcz.UporabnikDis = z.uporabnikDis;
                dcz.UporabnikDob = z.uporabnikDob;
                dcz.VrstaZahteve = z.vrstaZahteve;
                                
                dcZahteve.Add(dcz);            
            }


            zahteve = dcZahteve.ToArray();

            return cdcResult.Get_FromFunctionResult(fr);
        }
        
        public dcResult set_NeAkontacijski(string smm, bool neAkontacijski, out bool result)
        {
            sFunctionResult fr;
            result = false;

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");
            
            // datum se nastavi kot prvi v naslednjem mesecu    
            //DateTime dt = DateTime.Now;
            //DateTime DatumOd = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1);
            DateTime DatumOd = DateTime.Now; // današnji datum, mail Nosan 31.8.2015
            fr = PerunEG.Set_NeAkontacijski(smm, neAkontacijski, DatumOd);
            result = fr.resBool;
            
            return cdcResult.Get_FromFunctionResult(fr);
        }

        public dcResult Get_FakturiranaRealizacija(string smm, out List<dcFakturiranaRealizacija> realizacije)
        {
            sFunctionResult fr;
            realizacije = null;

            cPerun_EG PerunEG = new cPerun_EG("EGPws", "3gpws$");

            Perun3WsLib.FakturiranaRealizacijaService.PodatkiORealizacijiZaMesec[] real = null;

            // 
            fr = PerunEG.Get_FakturiranaRealizacija(smm, out real);
            if (real != null)
            {
                realizacije = new List<dcFakturiranaRealizacija>();
                foreach (var r in real)
                {
                    var realizacija = new dcFakturiranaRealizacija() { Leto = r.leto, Mesec = r.mesec };

                    realizacija.Postavke = new List<dcFakturiranaRealizacijaPos>();
                    foreach (var rp in r.zaracunljiviElementiInKolicine)
                    {
                        realizacija.Postavke.Add(new dcFakturiranaRealizacijaPos() { idZaracunljiviElement = rp.idZaracunljiviElement, zaracunljiviElementNaziv = rp.zaracunljiviElementNaziv, kolicina = rp.kolicina });
                    }

                    realizacije.Add(realizacija);
                }
            }
            
            return cdcResult.Get_FromFunctionResult(fr);
        }     

        public Int32 Test_KomunikatorEGP()
        {
            return 0;
        }

    }
}
