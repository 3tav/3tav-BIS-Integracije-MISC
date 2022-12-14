using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Komunikator3TavLib
{
    public class cInf_ZamenjavaDobavitelja
    {
        public cInf_ZamenjavaDobavitelja()
        {

        }

        public sFunctionResult Send_NovoZahtevoNaPerun(short _DistribucijaID, Int32 _MerilnoMesto, DateTime _DatumZacetka, DateTime _DatumKonca, String _DavcnaStevilka, Boolean _SkupniRacun, Int32 _PrenosEnergije, Int32 _KonicnaObremenitev, byte[] _PrilogaPDF, String _PrilogaIme, out Int32? _PerunZahtevaID)
        {
            sFunctionResult fr = cFunctionResult.Init();
            _PerunZahtevaID = null;
            KomunikatorEGP.ws_Inf_ZamenjavaDobavitelja.ZamenjavaDobaviteljaWSClient ZamenjavaDobavitelja = new KomunikatorEGP.ws_Inf_ZamenjavaDobavitelja.ZamenjavaDobaviteljaWSClient();
            try
            {
                ZamenjavaDobavitelja.ClientCredentials.UserName.UserName = "DGWS01";
                ZamenjavaDobavitelja.ClientCredentials.UserName.Password = "Gor2012";
                ZamenjavaDobavitelja.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                KomunikatorEGP.ws_Inf_ZamenjavaDobavitelja.oddajZahtevoZahtevek MenjavaReq = new KomunikatorEGP.ws_Inf_ZamenjavaDobavitelja.oddajZahtevoZahtevek();
                KomunikatorEGP.ws_Inf_ZamenjavaDobavitelja.oddajZahtevoOdgovor MenjavaRes = new KomunikatorEGP.ws_Inf_ZamenjavaDobavitelja.oddajZahtevoOdgovor();

                MenjavaReq.dis = _DistribucijaID;
                MenjavaReq.smm = _MerilnoMesto;
                MenjavaReq.datumKonca = _DatumKonca;
                MenjavaReq.datumKoncaSpecified = true;
                MenjavaReq.datunZacetka = _DatumZacetka;
                MenjavaReq.datunZacetkaSpecified = true;
                MenjavaReq.davcnaSt = _DavcnaStevilka;
                MenjavaReq.placevanjeUporabeOmrezja = _SkupniRacun;
                MenjavaReq.prenosEnergije = _PrenosEnergije;
                MenjavaReq.konicnaObremenitev = _KonicnaObremenitev;

                if (_PrilogaPDF != null)
                {
                    MenjavaReq.imePriloge = _PrilogaIme;
                    MenjavaReq.priloga = _PrilogaPDF;
                    MenjavaReq.tipPriloge = "PDF";
                }
                else
                {
                    MenjavaReq.imePriloge = "";
                    MenjavaReq.priloga = null;
                    MenjavaReq.tipPriloge = "";
                }

                MenjavaRes = ZamenjavaDobavitelja.oddajZahtevo(MenjavaReq);
                try
                {
                    ZamenjavaDobavitelja.Close();
                    ZamenjavaDobavitelja = null;
                }
                catch (Exception)
                {
                    ZamenjavaDobavitelja.Abort();
                    ZamenjavaDobavitelja = null;
                }
                if (MenjavaRes.rc != 0)
                {
                    fr = cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, "RTC = " + MenjavaRes.rc.ToString(), MenjavaRes.msg, "");
                    return fr;
                }
                else
                {
                    fr = cFunctionResult.Set(true, 0, "", "", "");
                }
                if (MenjavaRes.zahtevaIdSpecified)
                {
                    _PerunZahtevaID = MenjavaRes.zahtevaId;
                }
            }
            catch (Exception ex)
            {
                ZamenjavaDobavitelja.Abort();
                fr = cFunctionResult.Set(false, 1, "Napaka: cInf_ZamenjavaDobavitelja.Send_NovoZahtevoNaPerun ", ex.Message.ToString(), "");
            }
            return fr;
        }       
    }
}