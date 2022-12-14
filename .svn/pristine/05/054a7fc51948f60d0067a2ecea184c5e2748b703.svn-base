using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Komunikator3TavLib
{
    public class cInf_EvidencaMM
    {
        public cInf_EvidencaMM()
        {

        }

        public sFunctionResult Send_NovoZahtevoNaPerun(short _DistribucijaID, Int32 _MerilnoMesto, Int32 _ObdobjeMeritev, Boolean _ZahtevaZaMerilnePodatke, byte[] _PooblastiloPDF, String _PooblastiloIme, byte[] _NarocilnicaPDF, String _NarocilnicaIme, out Int32? _PerunZahtevaID)
        {
            sFunctionResult fr = cFunctionResult.Init();
            _PerunZahtevaID = null;
            KomunikatorEGP.ws_Inf_EvidencaMM.EvidencaMMDobaviteljWSClient EvidencaMM = new KomunikatorEGP.ws_Inf_EvidencaMM.EvidencaMMDobaviteljWSClient();
            try
            {
                EvidencaMM.ClientCredentials.UserName.UserName = "DGWS01";
                EvidencaMM.ClientCredentials.UserName.Password = "Gor2012";
                EvidencaMM.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                KomunikatorEGP.ws_Inf_EvidencaMM.oddajEvidencoZahtevek EvicencaZahtevek = new KomunikatorEGP.ws_Inf_EvidencaMM.oddajEvidencoZahtevek();
                KomunikatorEGP.ws_Inf_EvidencaMM.dokument DocPooblastilo = new KomunikatorEGP.ws_Inf_EvidencaMM.dokument();
                KomunikatorEGP.ws_Inf_EvidencaMM.dokument DocNarocilnica = new KomunikatorEGP.ws_Inf_EvidencaMM.dokument();
                KomunikatorEGP.ws_Inf_EvidencaMM.evidencaOdgovor EvicencaOdgovor = new KomunikatorEGP.ws_Inf_EvidencaMM.evidencaOdgovor();

                EvicencaZahtevek.dis = _DistribucijaID;
                EvicencaZahtevek.disSpecified = true;
                EvicencaZahtevek.smm = _MerilnoMesto;
                EvicencaZahtevek.smmSpecified = true;
                EvicencaZahtevek.obdobjeMeritev = _ObdobjeMeritev;
                EvicencaZahtevek.obdobjeMeritevSpecified = true;
                EvicencaZahtevek.zahtevamMerilnePodatke = _ZahtevaZaMerilnePodatke;

                if (_PooblastiloPDF != null)
                {
                    DocPooblastilo.ime = _PooblastiloIme;
                    DocPooblastilo.vsebina = _PooblastiloPDF;
                    DocPooblastilo.tip = "PDF";
                    DocPooblastilo.vrstaDokumenta = "";
                }
                else
                {
                    DocPooblastilo.ime = "";
                    DocPooblastilo.vsebina = null;
                    DocPooblastilo.tip = "";
                    DocPooblastilo.vrstaDokumenta = "";
                }

                if (_NarocilnicaPDF != null)
                {
                    DocNarocilnica.ime = _NarocilnicaIme;
                    DocNarocilnica.vsebina = _NarocilnicaPDF;
                    DocNarocilnica.tip = "PDF";
                    DocNarocilnica.vrstaDokumenta = "";
                }
                else
                {
                    DocNarocilnica.ime = "";
                    DocNarocilnica.vsebina = null;
                    DocNarocilnica.tip = "";
                    DocNarocilnica.vrstaDokumenta = "";
                }

                EvicencaOdgovor = EvidencaMM.oddajEvidenco(EvicencaZahtevek, DocPooblastilo, DocNarocilnica);
                try
                {
                    EvidencaMM.Close();
                    EvidencaMM = null;
                }
                catch (Exception)
                {
                    EvidencaMM.Abort();
                    EvidencaMM = null;
                }

                if (EvicencaOdgovor.rc != 0)
                {
                    fr = cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, "RTC = " + EvicencaOdgovor.rc.ToString(), EvicencaOdgovor.msg, "");
                    return fr;
                }
                else
                {
                    fr = cFunctionResult.Set(true, 0, "", "", "");
                }
                _PerunZahtevaID = EvicencaOdgovor.id;
            }
            catch (Exception ex)
            {
                EvidencaMM.Abort();
                fr = cFunctionResult.Set(false, 1, "Napaka: cInf_EvidencaMM.Send_NovoZahtevoNaPerun ", ex.Message.ToString(), "");
            }
            return fr;
        }

        public sFunctionResult Get_EvidencoZaID(Int32 _EvidencaID)
        {
            sFunctionResult fr = cFunctionResult.Init();
            KomunikatorEGP.ws_Inf_EvidencaMM.EvidencaMMDobaviteljWSClient EvidencaMM = new KomunikatorEGP.ws_Inf_EvidencaMM.EvidencaMMDobaviteljWSClient();
            try
            {
                EvidencaMM.ClientCredentials.UserName.UserName = "DGWS01";
                EvidencaMM.ClientCredentials.UserName.Password = "Gor2012";
                EvidencaMM.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                KomunikatorEGP.ws_Inf_EvidencaMM.evidencaOdgovor EvicencaOdgovor = new KomunikatorEGP.ws_Inf_EvidencaMM.evidencaOdgovor();

                EvicencaOdgovor = EvidencaMM.najdiEvidencoPodrobnosti(_EvidencaID);
                try
                {
                    EvidencaMM.Close();
                    EvidencaMM = null;
                }
                catch (Exception)
                {
                    EvidencaMM.Abort();
                    EvidencaMM = null;
                }
                if (EvicencaOdgovor.rc != 0)
                {
                    fr = cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, "RTC = " + EvicencaOdgovor.rc.ToString(), EvicencaOdgovor.msg, "");
                    return fr;
                }
                else
                {
                    fr = cFunctionResult.Set(true, 0, "", "", "");
                }
            }
            catch (Exception ex)
            {
                EvidencaMM.Abort();
                fr = cFunctionResult.Set(false, 1, "Napaka: cInf_EvidencaMM.Get_EvidencoZaID ", ex.Message.ToString(), "");
            }
            return fr;
        }
    }
}