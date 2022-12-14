using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Perun3WsLib;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Komunikator3TavLib
{
    public class cPerun_EG
    {
        String EGws_UserName;
        String EGws_Password;
        public cPerun_EG(String _UserName, String _Password)
        {
            EGws_UserName = _UserName;
            EGws_Password = _Password;
        }

        /* backup stare funkcije */
        public sFunctionResult Set_VpisOdcitkaZaMerilnoMestoOld(Int32 DobaviteljID, Int32 StevilkaMerilnegaMesta, String DatumOdcitka, Int32 VrstaOdcitka, String ET, String DVT, String DMT)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            try
            {

                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_VpisOdcitkaZaMerilnoMesto_KontrolaDobavitelja(DobaviteljID, StevilkaMerilnegaMesta, DatumOdcitka, VrstaOdcitka, ET, DVT, DMT, null, null, null, null);

                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }

                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }


        public sFunctionResult Set_VpisOdcitkaZaMerilnoMesto(Int32 DobaviteljID, Int32 StevilkaMerilnegaMesta, String DatumOdcitka, Int32 VrstaOdcitka, String ET, String DVT, String DMT)
        {
            Perun3Request request = new Perun3Request();
            Perun3Response response = new Perun3Response();
            Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

            try
            {
                response = svc.VnosOdbirkaPerun2Compatible(DobaviteljID, StevilkaMerilnegaMesta, DatumOdcitka, VrstaOdcitka, ET, DVT, DMT);
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }

            if (response.Result == false)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, "", response.Message, "");
            }

            return cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, (response.Data == null ? string.Empty : response.Data.ToString()), "", "");
        }

        public sFunctionResult Set_VpisOdcitkaZaMerilnoMesto(Int32 DobaviteljID, Int32 StevilkaMerilnegaMesta, String DatumOdcitka, Int32 VrstaOdcitka, String ET, String DVT, String DMT, String Vir)
        {
            Perun3Request request = new Perun3Request();
            Perun3Response response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

            try
            {
                response = svc.VnosOdbirkaEnostavni(DobaviteljID, StevilkaMerilnegaMesta, DatumOdcitka, VrstaOdcitka, ET, DVT, DMT, Vir);
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }

            if (response.Result == false)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, "", response.Message, "");
            }

            return cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, (response.Data == null ? string.Empty : response.Data.ToString()), "", "");
        }


        public sFunctionResult Set_ObracunOdcitek(string smm, DateTime datumOdcitka, out Int32? PrilogaA_ID)
        {
            PrilogaA_ID = null;
            Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                        
            Perun3Request request = new Perun3Request(smm, datumOdcitka);
            Perun3Response response = new Perun3Response();
                        
            try
            {     
                // dobi ustrezno obračunsko obdobje glede na datumOdbirka
                var r1 = svc.GetObracunskaObdobja(new Perun3Request(smm, datumOdcitka));
                if (r1.Result == false)
                {
                    //  išči še za eno leto nazaj
                    if (r1.Message.Contains("Ni najdenih obračunskih obdobij."))
                     {
                        var datumOd = new DateTime(datumOdcitka.AddYears(-1).Year, 1, 1);
                        var datumDo = new DateTime(datumOdcitka.Year, 12, 31);
                        r1 = svc.GetObracunskaObdobja(new Perun3Request(smm, datumOd, datumDo));
                        if (r1.Result == false)
                        {
                            throw new Exception(string.Format("{0} [10]", r1.Message));
                        }
                     }
                    else
                    {
                        throw new Exception(string.Format("{0} [20]", r1.Message));
                    }                    
                }
                
                var obracunskaObdobja = (Perun3WsLib.ObracunPartnerObdobjaService.VrstaObracuna[])r1.Data;

                if (obracunskaObdobja == null)
                    throw new Exception("Za merilno mesto ni najdenih obračunskih obdobij.");

                Perun3WsLib.ObracunPartnerObdobjaService.VrstaObracuna vo = null;

                if (obracunskaObdobja.Length == 1)
                {
                    vo = obracunskaObdobja[0];
                }
                else
                {
                    foreach (var o in obracunskaObdobja)
                    {
                        if (datumOdcitka >= o.porabaOd && datumOdcitka <= o.porabaDo)
                        {
                            vo = o;
                            break;
                        }
                    }
                }
                

                if (vo == null)
                    throw new Exception(string.Format("Ne najdem ustreznega obračunskega obdobja za datum!"));

                var vop = new Perun3WsLib.ObracunPartnerService.VrstaObracuna();
                vop.enotniIdMM = vo.enotniIdMM;                
                vop.idMM = vo.idMM;
                vop.idMMSpecified = true;
                vop.idVrstaObdelave = vo.idVrstaObdelave;
                vop.idVrstaObdelaveSpecified = true;
                vop.nazivVrstaObdelave = vo.nazivVrstaObdelave;                
                vop.odjava = vo.odjava;
                vop.odjavaSpecified = true;
                vop.porabaOd = vo.porabaOd;
                vop.porabaOdSpecified = true;
                vop.porabaDo = vo.porabaDo;
                vop.porabaDoSpecified = true;
                vop.stPogRac = vo.stPogRac;
                vop.stPogRacSpecified = true;
                
                var r2 = svc.ObracunPartner(new Perun3Request(smm, vop));
                if (r2.Result == false)
                    throw new Exception(string.Format("ObracunPartner: {0}", r2.Message));

                var xml = (string)r2.Data;
                // insert prilogeA v BIS
                BisLib bis = new BisLib(cSettings.Get_3Tav_ConnString());

                // return od responsa = id insertirane priloge A
                response.Data = bis.InsertPrilogaA(xml);
                //response = svc.ObracunPartnerPerun2Compatible(request);
                PrilogaA_ID = (Int32)response.Data;
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, "", ex.Message, "");
            }
            if (response.Result == false)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, "", response.Message, "");
            }

            return cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, (response.Data == null ? string.Empty : response.Data.ToString()), "", "");
        }


        public sFunctionResult Set_ObracunOdcitekOld(Int32 _MerilnoMesto, Int32 _DobaviteljID, short _DodajAkontacijo, out Int32? PrilogaA_ID)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            PrilogaA_ID = null;
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_ObracunOdcitek(out PrilogaA_ID, _DobaviteljID, _MerilnoMesto, _DodajAkontacijo);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }

        public sFunctionResult Set_ObracunObrok(Int32 _MerilnoMesto, Int32 _DobaviteljID, Int32 _Leto, Int32 _Mesec, out Int32? PrilogaA_ID)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            PrilogaA_ID = null;
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_ObracunObrok(out PrilogaA_ID, _DobaviteljID, _MerilnoMesto, _Leto, _Mesec);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }



        public sFunctionResult Set_StornirajFakturoZaPrilogoAID(Int32 PrilogaAID, Int32 StevilkaMerilnegaMesta)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_StornirajFakturoZaPrilogoAID(PrilogaAID, StevilkaMerilnegaMesta, cSettings.DistribucijaId);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }

        public sFunctionResult Set_StornirajOdcitek(Int32 StevilkaMerilnegaMesta, String DatumOdcitka)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_StornirajOdcitek(StevilkaMerilnegaMesta, DatumOdcitka, cSettings.DistribucijaId);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }

        public sFunctionResult Set_DolociAkontacijskiNeakontacijski(Int32 StevilkaMerilnegaMesta, Boolean Neakontacijski)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_DolociAkontacijskiNeakontacijski(StevilkaMerilnegaMesta, cSettings.DistribucijaId, Neakontacijski);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }

        public sFunctionResult Set_DolociVisinoAkontacije(Int32 StevilkaMerilnegaMesta, Decimal? MesecnoPovprecjeVT, Decimal? MesecnoPovprecjeMT, Decimal? MesecnoPovprecjeET)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                Res = PerunEG.Set_DolociVisinoAkontacije(StevilkaMerilnegaMesta, cSettings.DistribucijaId, MesecnoPovprecjeVT, MesecnoPovprecjeMT, MesecnoPovprecjeET);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }


        public sFunctionResult Get_PrilogeAZaMerilnoMesto(Int32 _MerilnoMesto, Int32 _DobaviteljID, DateTime _DatumOd, DateTime? _DatumDo, out KomunikatorEGP.ws_EG_PerunEG.dcParametriPrilogeAList ParametriPrilogeAList)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            ParametriPrilogeAList = new KomunikatorEGP.ws_EG_PerunEG.dcParametriPrilogeAList();
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                String DatumDoStr = null;
                if (_DatumDo != null)
                {
                    DatumDoStr = ((DateTime)_DatumDo).ToString("dd.MM.yyyy");
                }
                Res = PerunEG.Get_PrilogeAZaMerilnoMesto(out ParametriPrilogeAList, _MerilnoMesto, _DobaviteljID, _DatumOd.ToString("dd.MM.yyyy"), DatumDoStr);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }

        public sFunctionResult Get_PrilogoAZaPaketID(Int32 _MerilnoMesto, Int32 _PrilogaA_ID, out byte[] _FakturaPrilogaA)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            _FakturaPrilogaA = null;
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;

                Res = PerunEG.Get_PrilogoAZaPaketID(out _FakturaPrilogaA, _MerilnoMesto, _PrilogaA_ID);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }

        public sFunctionResult Get_ZadnjiOdcitekZaMerilnoMestoOld(Int32 _IdOdjemnagaMesta, out dcPerunOdcitek _Odcitek)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            _Odcitek = new dcPerunOdcitek();
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
                KomunikatorEGP.ws_EG_PerunEG.dcOdcitek OdcitekData;

                Res = PerunEG.Get_ZadnjiOdcitek(out OdcitekData, _IdOdjemnagaMesta);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    _Odcitek.OdcitekID = null;
                    _Odcitek.SMM = OdcitekData.SMM;
                    _Odcitek.DatumStanja = OdcitekData.DatumStanja;
                    _Odcitek.StanjeET = OdcitekData.SlikaET;
                    _Odcitek.StanjeVT = OdcitekData.SlikaDVT;
                    _Odcitek.StanjeMT = OdcitekData.SlikaDMT;
                    _Odcitek.Status = "S";
                    _Odcitek.VrstaOdcitka = 2;
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }
        public sFunctionResult Get_ZadnjiOdcitekZaMerilnoMesto(Int32 _IdDistribucije, Int32 _IdOdjemnagaMesta, out dcPerunOdcitek odcitek)
        {
            odcitek = new dcPerunOdcitek();

            try
            {
                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.GetZadnjiOdbirek(new Perun3Request(svc.GetSmm(_IdDistribucije, _IdOdjemnagaMesta)));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                var data = (Perun3WsLib.PridobivanjeZadnjegaOdbirkaService.OdbirekZOpisi)response.Data;
                odcitek = GetDcOdcitek(data);
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult Get_ZadnjiOdcitekZaMerilnoMesto(string smm, out dcPerunOdcitek odcitek)
        {
            odcitek = new dcPerunOdcitek();

            try
            {
                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.GetZadnjiOdbirek(new Perun3Request(smm));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

                var data = (Perun3WsLib.PridobivanjeZadnjegaOdbirkaService.OdbirekZOpisi)response.Data;
                odcitek = GetDcOdcitek(data);
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        private dcPerunOdcitek GetDcOdcitek(Perun3WsLib.PridobivanjeZadnjegaOdbirkaService.OdbirekZOpisi odcitekWs)
        { 
            dcPerunOdcitek odcitek = new dcPerunOdcitek();
            odcitek.OdcitekID = null;
            odcitek.SMM = odcitekWs.idMM;
            odcitek.DatumStanja = odcitekWs.datumOdcitavanja;

            odcitek.StanjeET = string.Empty;
            odcitek.StanjeVT = string.Empty;
            odcitek.StanjeMT = string.Empty;

            foreach (var o in odcitekWs.registerOdbirek)
            {
                switch ((RegisterOdbirekEnum)o.idRegister)
                {
                    case RegisterOdbirekEnum.ET:
                        odcitek.StanjeET = o.stanjeOdbirka.ToString(); break;
                    case RegisterOdbirekEnum.VT:
                        odcitek.StanjeVT = o.stanjeOdbirka.ToString(); break;
                    case RegisterOdbirekEnum.MT:
                        odcitek.StanjeMT = o.stanjeOdbirka.ToString(); break;
                    default:
                        break;
                        //throw new Exception("Neznan tip odbirka!");
                }
            }
            odcitek.Status = "S";
            odcitek.VrstaOdcitka = 2;
            
            return odcitek;
        }


        public sFunctionResult Get_InformativniIzracunPorabeOld(String DatumOdcitka_Stari, String DatumOdcitka_Novi, Int32 ObracunskaMoc, Int32? Kolicina_ET, Int32? Kolicina_DVT, Int32? Kolicina_DMT, out KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList SeznamcPostavkaIzracuna)
        {
            KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
            SeznamcPostavkaIzracuna = new KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList();
            try
            {
                PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
                PerunEG.ClientCredentials.UserName.Password = EGws_Password;
                PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                KomunikatorEGP.ws_EG_PerunEG.dcResult Res;

                Res = PerunEG.Get_InformativniIzracunPorabe(out SeznamcPostavkaIzracuna, DatumOdcitka_Stari, DatumOdcitka_Novi, ObracunskaMoc, Kolicina_ET, Kolicina_DVT, Kolicina_DMT);
                try
                {
                    PerunEG.Close();
                    PerunEG = null;
                }
                catch (Exception)
                {
                    PerunEG.Abort();
                    PerunEG = null;
                }
                if (Res.RTC == 0)
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
                else
                {
                    return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
                }
            }
            catch (Exception ex)
            {
                PerunEG.Abort();
                return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
            }
        }
    
        public sFunctionResult Get_InformativniIzracunPorabe(string smm, string datumOdcitkaStari, string datumOdcitkaNovi, int obracunskaMoc, Int32? kolicinaET, Int32? kolicinaDVT, Int32? kolicinaDMT, out KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList seznamPostavkaIzracuna, out Int32? prilogaa_id)
        {
            
            Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
            Perun3Request request = new Perun3Request();
            Perun3Response response = new Perun3Response();
            seznamPostavkaIzracuna = null;
            prilogaa_id = null;
            //seznamPostavkaIzracuna = new KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList();
            int porabaEt = 0, porabaVt = 0, porabaMt = 0;
            try
            {
                // response = svc.GetInformativniObracunPerun2Compatible(smm, datumOdcitkaStari, datumOdcitkaNovi, obracunskaMoc, kolicinaET, kolicinaDVT, kolicinaDMT);
                response = svc.GetInformativniObracunPerun2Compatible(smm, datumOdcitkaStari, datumOdcitkaNovi, obracunskaMoc, kolicinaET, kolicinaDVT, kolicinaDMT, ref porabaEt, ref porabaVt, ref porabaMt);
            }
            catch (Exception ex)
            {
                response.Result = false;
                //return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError,string.Empty, ex.Message, "");
            }
            // bis lib init
            BisLib bis = new BisLib(cSettings.Get_3Tav_ConnString());
            var xml = string.Empty;
            PrilogeA pa = new PrilogeA();
            XmlSerializer serializer = new XmlSerializer(pa.GetType());

            
            //response.Result = false;

            if (response.Result == false)
            {
                try
                {
                    //xml = bis.GetPrilogaAZadnja(smm);
                    if (kolicinaET.HasValue && kolicinaET > 0)
                    {
                        xml = bis.GetPrilogaATemplate1T();
                    }
                    else
                    {
                        xml = bis.GetPrilogaATemplate();
                    }
                    
                }
                catch (Exception ex)
                {
                    return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, "", ex.Message, "");
                }

                if (string.IsNullOrEmpty(xml))                
                    return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, "", response.Message, "");

                //                
                using (TextReader rdr = new StringReader(xml))
                {
                    pa = (PrilogeA)serializer.Deserialize(rdr);
                }
               
                // popravi header
                pa.PrilogaA[0].EnotniIdentifikatorMerilnegaMesta = smm;
                pa.PrilogaA[0].Distribucija = (short)Convert.ToInt32(smm.Substring(0, 1));
                pa.PrilogaA[0].StevilkaMerilnegaMesta = smm.Substring(2, smm.Length -2);
                pa.PrilogaA[0].Splosno.ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                pa.PrilogaA[0].Splosno.ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);

                decimal vt = 0, mt = 0, et = 0;

                if (kolicinaDVT.HasValue)
                    vt = (decimal)kolicinaDVT;

                if (kolicinaDMT.HasValue)
                    mt = (decimal)kolicinaDMT;

                if (kolicinaET.HasValue)
                    et = (decimal)kolicinaET;


                var sumOmreznina = porabaEt + porabaMt + porabaVt;

                // nove vrednosti za količine in zneske, za ceno predpostavljam da se ni spremenila
                for (var i = 0; i < pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica.Length; i++)
                {
                    var sifraElementa = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].SifraZaracunljivegaElementa;

                    switch (sifraElementa)
                    {
                        case 4:
                          
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina =  vt;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;                        
                                                  
                            break;
                        case 5:                           
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = mt;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;                          
                            break;
                        case 6:
                            
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = et;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;
                            
                            break;
                        case 1:     //  Obračunska moč se nastavi v proceduri
                            break;  
                        case 10:    //  Prispevek OVE+SPTE
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = 0;
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;
                            break;
                        case 12:    //  Prispevek za URE
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = sumOmreznina;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;
                            break;
                        case 21:    //  Dodatek za BORZEN
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = sumOmreznina;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;                            
                            break;
                        default:
                            // ostale postavke naj ne vplivajo na informativni izračun
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = 0;
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = 0;
                            break;
                    }

                  
                }
 
                // zapis popravljenih količin nazaj v XML
                using (var sww = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sww))
                    {
                        serializer.Serialize(writer, pa);
                        xml = sww.ToString();  

                        // dodaj ustrezni namespace
                        xml =  xml.Replace(@"<PrilogeA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>", 
                                           @"<PrilogeA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:noNamespaceSchemaLocation='http://www.informatika.si/schema/prilogaA_v2.4.xsd'>" );
                    }
                }
            }
            else
            {
                // service OK, priloga A je to kar vrne Perun
                xml = (string)response.Data;
                using (TextReader rdr = new StringReader(xml))
                {
                    pa = (PrilogeA)serializer.Deserialize(rdr);
                }
            }
         
            int pos = 0;
            seznamPostavkaIzracuna = new KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList();
            seznamPostavkaIzracuna.SteviloPostavk = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica.Length;
            seznamPostavkaIzracuna.Data = new KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracuna[seznamPostavkaIzracuna.SteviloPostavk];
            foreach (var v in pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica)
            { 
                var steviloDni = (v.ObdobjeDo - v.ObdobjeOd).Days;
                var povprecje = (double)(steviloDni > 0 ? (v.Znesek / steviloDni) : v.Znesek);
                seznamPostavkaIzracuna.Data[pos] = new KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracuna();
                seznamPostavkaIzracuna.Data[pos].PostavkaCenikID = v.PoCeniku;
                seznamPostavkaIzracuna.Data[pos].PostavkaNaziv = bis.GetPostavkaObracunaNaziv(v.SifraZaracunljivegaElementa);
                seznamPostavkaIzracuna.Data[pos].PostavkaOznaka = bis.GetPostavkaObracunaOznaka(v.SifraZaracunljivegaElementa);
                seznamPostavkaIzracuna.Data[pos].PovprecjeNaDan = (double?)povprecje;
                seznamPostavkaIzracuna.Data[pos].SteviloDni = (int?)steviloDni;
                seznamPostavkaIzracuna.Data[pos].ObdobjeOd = (DateTime?)v.ObdobjeOd;
                seznamPostavkaIzracuna.Data[pos].ObdobjeDo = (DateTime?)v.ObdobjeDo;
                seznamPostavkaIzracuna.Data[pos].Kolicina = (double?)v.Kolicina;
                seznamPostavkaIzracuna.Data[pos].CenaNaEnoto = (double?)v.Cena.Cena;
                seznamPostavkaIzracuna.Data[pos].StopnjaDDV = (double?)v.StopnjaDDV;
                seznamPostavkaIzracuna.Data[pos].ZnesekBrezDDV = (double?)v.Znesek;
                pos++;
                
            }
            
            // return od responsa = id insertirane priloge A            
            prilogaa_id = (Int32)bis.InsertPrilogaAIInformativni(xml);

            return cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, string.Empty, string.Empty, string.Empty);
        }

        public sFunctionResult Get_PodatkiMM(string smm, out Perun3WsLib.PridobivanjePodatkovMMService.PodatkiMerilnegaMesta podatkiMM)
        {
            podatkiMM = null;

            try
            {
                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());                       
                var response = svc.GetPodatkiMM(new Perun3Request(smm));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                podatkiMM = (Perun3WsLib.PridobivanjePodatkovMMService.PodatkiMerilnegaMesta)response.Data;
             
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult Get_PDP(string smm, out dcPovprecnaPorabaPerun3 pdp)
        {
            pdp = null;

            try
            {
                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.GetPDP(new Perun3Request(smm));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                var p = (Perun3WsLib.PridobivanjePDPService.Pdp)response.Data;

                pdp = CreateDcPDP(smm, p);                
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult Vnos_PDP(dcPovprecnaPorabaPerun3 pdp)
        {
            
            try
            {
                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.VnosPDP(new Perun3Request(pdp.SMM, CreatePerun3Pdp(pdp)));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                //pdp = (Perun3WsLib.VnosPDPService.Pdp)response.Data;
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult Vnos_PDP_Cakalnica(dcPovprecnaPorabaPerun3 pdp)
        {

            try
            {
                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                                   
                var response = svc.VnosZahtevePDP(new Perun3Request(pdp.SMM, CreatePerun3Pdp(pdp)));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
             
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        private string Serialize<T>(T value)
        {
            if (value == null)
                return string.Empty;

            string serializeXml = string.Empty;

            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
                StringWriter stringWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(stringWriter);
                xmlserializer.Serialize(writer, value);
                serializeXml = stringWriter.ToString();
                writer.Close();
            }
            catch (Exception ex)
            {
                serializeXml = string.Format(@"<serializationError>{0}</serializationError>", ex.Message);
            }

            return serializeXml;
        }

        private dcPovprecnaPorabaPerun3 CreateDcPDP(string smm, Perun3WsLib.PridobivanjePDPService.Pdp p)
        {
            var pdp = new dcPovprecnaPorabaPerun3();
            pdp.SMM = smm;
            pdp.idMM = p.idMM;
            pdp.idPodjetje = p.idPodjetje;
            pdp.IdRazlPovpPor = p.idRazlPovpPor;
            pdp.NazivRazloga = p.nazivRazloga;
            pdp.ObdobjeOd = Convert.ToString(p.obdobjeOd);
            pdp.ObdobjeDo = Convert.ToString(p.obdobjeDo);

            foreach (var m in p.tipMeritveMnVrednost)
            {
                switch ((RegisterOdbirekEnum)m.id)
                {
                    case RegisterOdbirekEnum.ET:
                        pdp.PorabaET = (double?)m.vrednost; break;
                    case RegisterOdbirekEnum.MT:
                        pdp.PorabaMT = (double?)m.vrednost; break;
                    case RegisterOdbirekEnum.VT:
                        pdp.PorabaVT = (double?)m.vrednost; break;
                    default:
                        break;
                }
            }
            return pdp;
        }

        private Perun3WsLib.VnosPDPService.Pdp CreatePerun3Pdp(dcPovprecnaPorabaPerun3 p)
        {
            var pdp = new Perun3WsLib.VnosPDPService.Pdp();
            
            pdp.idMM = p.idMM;
            pdp.idPodjetje = p.idPodjetje;
            pdp.idRazlPovpPor = p.IdRazlPovpPor;
            pdp.nazivRazloga = p.NazivRazloga;
            pdp.obdobjeOd = Convert.ToDateTime(p.ObdobjeOd);
            pdp.obdobjeDo = Convert.ToDateTime(p.ObdobjeDo);
            pdp.userGui = string.Empty;

            int i = 0;
            pdp.tipMeritveMnVrednost = new Perun3WsLib.VnosPDPService.TipMeritveMnVrednost[GetPerun3PdpMeritveCount(p)];            
            if (p.PorabaET.HasValue)
            {
                if (p.PorabaET.Value > 0)
                {
                    pdp.tipMeritveMnVrednost[i] = new Perun3WsLib.VnosPDPService.TipMeritveMnVrednost();
                    pdp.tipMeritveMnVrednost[i].id = (int)RegisterOdbirekEnum.ET;
                    pdp.tipMeritveMnVrednost[i].vrednost = (decimal)p.PorabaET.Value;
                    pdp.tipMeritveMnVrednost[i].nazivRegistra = "Energija DET";
                    pdp.tipMeritveMnVrednost[i].oznakaEM = "DET";
                    i++;
                }            
            }

            if (p.PorabaVT.HasValue)
            {
                if (p.PorabaVT.Value > 0)
                {
                    pdp.tipMeritveMnVrednost[i] = new Perun3WsLib.VnosPDPService.TipMeritveMnVrednost();
                    pdp.tipMeritveMnVrednost[i].id = (int)RegisterOdbirekEnum.VT;
                    pdp.tipMeritveMnVrednost[i].vrednost = (decimal)p.PorabaVT.Value;
                    pdp.tipMeritveMnVrednost[i].nazivRegistra = "Energija DVT";
                    pdp.tipMeritveMnVrednost[i].oznakaEM = "DVT";
                    i++;
                }
            }

            if (p.PorabaMT.HasValue)
            {
                if (p.PorabaMT.Value > 0)
                {
                    pdp.tipMeritveMnVrednost[i] = new Perun3WsLib.VnosPDPService.TipMeritveMnVrednost();
                    pdp.tipMeritveMnVrednost[i].id = (int)RegisterOdbirekEnum.MT;
                    pdp.tipMeritveMnVrednost[i].vrednost = (decimal)p.PorabaMT.Value;                    
                    pdp.tipMeritveMnVrednost[i].nazivRegistra = "Energija DMT";
                    pdp.tipMeritveMnVrednost[i].oznakaEM = "DMT";
                    i++;
                }
            }

            return pdp;
        }

        private int GetPerun3PdpMeritveCount(dcPovprecnaPorabaPerun3 p)
        {
            int count = 0;
            if (p.PorabaET.HasValue)
            {
                if (p.PorabaET.Value > 0)
                {
                    count++;
                }
            }

            if (p.PorabaVT.HasValue)
            {
                if (p.PorabaVT.Value > 0)
                {
                    count++;
                }
            }

            if (p.PorabaMT.HasValue)
            {
                if (p.PorabaMT.Value > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public sFunctionResult MenjavaDob_OddajVlogo(dcMenjavaDobZahteva zahteva, out Int32? zahtevaId)
        {
            zahtevaId = null;
            
            var response = new Perun3Response();

            var v = new Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.VlogaZaMenjavoDobaviteljaTip();
            
            var message = string.Empty;
            try
            {

                v.enotniIdentifikatorMM = zahteva.Smm;
                v.vkljucitevPodatkovMMSpecified = true;
                v.vkljucitevPodatkovMM = zahteva.PodatkiMM;
                v.datumMenjave = zahteva.DatumZacetka;
                v.datumVloge = DateTime.Now;

                v.placnik = new Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.PoslovniPartnerTip();

                //v.

                // optional                
                v.placnik.naziv = "";
                v.placnik.naslov = new Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.NaslovTip();
                v.placnik.naslov.hisnaStevilka = "";
                v.placnik.naslov.kraj = "";
                v.placnik.naslov.posta = "";
                v.placnik.naslov.postnaStevilka = "";
                v.placnik.naslov.ulica = "";

                v.placnik.davcniPodatki = new Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.DavcniPodatkiTip();
                v.placnik.davcniPodatki.davcnaStevilka = "";
                //v.placnik.davcniPodatki.davcniZavezanec = true;

                var datoteka = (zahteva.ImePriloge == null ? string.Empty : zahteva.ImePriloge.Trim());

                var koncnica = string.Empty;
                if (datoteka.Contains('.') && datoteka.Length > 3)
                    koncnica = datoteka.Substring(datoteka.LastIndexOf('.') + 1);

                v.pogodbaODobavi = new Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.Priloga[1];
                v.pogodbaODobavi[0] = new Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.Priloga();
                v.pogodbaODobavi[0].casovniZig = DateTime.Now;
                v.pogodbaODobavi[0].dokumentKoncnica = koncnica;
                v.pogodbaODobavi[0].dokumentNaziv = zahteva.ImePriloge;
                v.pogodbaODobavi[0].dokument = (byte[])zahteva.Priloga;

                v.vrstaRacuna = Perun3WsLib.MenjavaDobaviteljaOddajaVlogeService.VlogaZaMenjavoDobaviteljaTipVrstaRacuna.S;
                

                Perun3Request req = new Perun3Request(v);
                req.Smm = zahteva.Smm;

                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                
                response = svc.MenjavaDobaviteljaOddajaVloge(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                

                // chain link ws call #2
                //var responseNajdi = _svc.MenjavaDobaviteljaNajdiVloge(new Perun3Request(v.enotniIdentifikatorMM, v.datumMenjave.AddDays(-1), v.datumMenjave.AddDays(1)));
                //if (responseNajdi.Result == false)
                //{
                //    throw new Exception(string.Format("{0} [20]", responseNajdi.Message));
                //}

                //var vloga = (Perun3WsLib.MenjavaDobaviteljaNajdiVlogeService.NajdiVlogaTip)responseNajdi.Data;
                //var status = vloga.statusVloge;
                //var idOpravila = vloga.idOpravila;


                // brezveze, servis ne vrne 
                //int.TryParse(response.Data.ToString(), out odgovor.zahtevaId);

            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");
        }

        public sFunctionResult MenjavaDob_NajdiVlogo(string smm, DateTime datumOd, DateTime datumDo, string status, out List<dcMenjavaDobVloga> seznamVlog)
        {
            seznamVlog = null;

            try
            {
                Perun3Request req = new Perun3Request();
                req.Smm = smm;

                req.Parm1 = smm;
                req.Parm2 = datumOd;
                req.Parm3 = datumDo;
                req.Parm4 = status;

                Perun3Service svc = new Perun3Service(cSettings.Get_3Tav_ConnString());                
                var response = svc.MenjavaDobaviteljaNajdiVloge(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

                var vloge = (Perun3WsLib.MenjavaDobaviteljaNajdiVlogeService.NajdiVlogaTip[])response.Data;
                if (vloge != null)
                {
                    seznamVlog = new List<dcMenjavaDobVloga>();
                    foreach (var v in vloge)
                    {
                        seznamVlog.Add(new dcMenjavaDobVloga() { DatumMenjave = v.datumMenjave, DatumVloge = v.datumVloge, IdOpravila = v.idOpravila, Smm = v.enotniIdentifikatorMM, Status = Convert.ToString(v.statusVloge) });
                    }
                }               
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");
        }

        /*
        public sFunctionResult Get_FakturaOnDemandPdf(string stRacuna, out byte[] fakturaPdf)
        {
            var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
            var req = new Perun3Request(stRacuna);
            fakturaPdf = null;

            try
            {
                var response = svc.GetFakturaOnDemandPdf(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

                fakturaPdf = (byte[])response.Data;
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");                
        }
        */
        public sFunctionResult OdpovedPogodbeODobavi(string smm,string sifraDobavitelja, DateTime datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result)
        {
            result = false;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobavi(new Perun3Request(smm, sifraDobavitelja, datum, priloga, prilogaNaziv, prilogaKoncnica));
                if (response.Result == false)                
                    throw new Exception(response.Message);
                
                result = (bool)response.Data;
         
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult OdpovedPogodbeODobaviOdjemalec(string smm, string sifraDobavitelja, DateTime datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result)
        {
            result = false;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobaviOdjemalec(new Perun3Request(smm, sifraDobavitelja, datum, priloga, prilogaNaziv, prilogaKoncnica));
                if (response.Result == false)
                    throw new Exception(response.Message);

                result = (bool)response.Data;

            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }


        public sFunctionResult PrviPriklop(dcNovaPoD vloga)
        {
            var response = new Perun3Response();

            var v = new Perun3WsLib.PrviPriklopStoritevService.PrviPriklopZahteva();

            var message = string.Empty;
            try
            {
                
                v.osnovniPodatki = new Perun3WsLib.PrviPriklopStoritevService.osnovniPodatkiVloge();
                v.osnovniPodatki.datumVloge = vloga.DatumVloge;
                
                v.osnovniPodatki.enotniIdentifikatorMM = vloga.Smm;
                v.osnovniPodatki.opomba = vloga.Opomba;



                v.pogodbaODobavi = new Perun3WsLib.PrviPriklopStoritevService.PogodbaODobavi();
                v.pogodbaODobavi.dokument = vloga.PogodbaODobavi;
                v.pogodbaODobavi.dokumentNaziv = vloga.ImePogodbe;
                v.pogodbaODobavi.dokumentKoncnica = GetKoncnica(vloga.ImePogodbe);

                if (!string.IsNullOrEmpty(vloga.ImePriloge))
                {

                    v.osnovniPodatki.priloge = new Perun3WsLib.PrviPriklopStoritevService.PrilogaVlogeZaPoD[1];
                    v.osnovniPodatki.priloge[0] = new Perun3WsLib.PrviPriklopStoritevService.PrilogaVlogeZaPoD();
                    v.osnovniPodatki.priloge[0].dokument = vloga.Priloga;
                    v.osnovniPodatki.priloge[0].dokumentNaziv = vloga.ImePriloge;
                    v.osnovniPodatki.priloge[0].dokumentKoncnica = GetKoncnica(vloga.ImePriloge);
                }
                
                v.zahtevaneVrsteSpremembe = new Perun3WsLib.PrviPriklopStoritevService.spremembePoD();
                v.zahtevaneVrsteSpremembe.spremembaLastnika = vloga.SpremembaLastnika;
                v.zahtevaneVrsteSpremembe.spremembaNaslovnika = vloga.SpremembaNaslovnika;
                v.zahtevaneVrsteSpremembe.spremembaPlacnika = vloga.SpremembaPlacnika;

                if (!string.IsNullOrEmpty(vloga.Bremenitev))
                {
                    v.zahtevaneVrsteSpremembe.bremenitevSpecified = false;
                    if (vloga.Bremenitev == "L")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.PrviPriklopStoritevService.bremenitev.L;
                        v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    if (vloga.Bremenitev == "S")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.PrviPriklopStoritevService.bremenitev.S;
                        v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    if (!v.zahtevaneVrsteSpremembe.bremenitevSpecified)
                        throw new Exception("Neveljavna vrsta bremenitve!");
                }

                if (!string.IsNullOrEmpty(vloga.VrstaObracuna))
                {
                    v.zahtevaneVrsteSpremembe.tarifaSpecified = false;

                    if (vloga.VrstaObracuna == "1T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.PrviPriklopStoritevService.tarifa.Enotarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (vloga.VrstaObracuna == "2T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.PrviPriklopStoritevService.tarifa.Dvotarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (vloga.VrstaObracuna == "3T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.PrviPriklopStoritevService.tarifa.Tritarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (!v.zahtevaneVrsteSpremembe.tarifaSpecified)
                        throw new Exception(string.Format("Neveljavna vrsta obračuna [{0}]!", vloga.VrstaObracuna));
                }

          


                var req = new Perun3Request(v);
                req.Smm = vloga.Smm;
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

                response = svc.PrviPriklop(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult OdpovedPogodbeODobaviPreklic(string smm,string sifraDobavitelja, DateTime datum, string kontaktnaOseba, string opomba, out bool result)
        {
            result = false;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobaviPreklic(new Perun3Request(smm, sifraDobavitelja, datum, kontaktnaOseba, opomba));
                if (response.Result == false)                
                    throw new Exception(response.Message);
                
                result = (bool)response.Data;
         
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        /*
        public sFunctionResult Get_NajdiZahteve(string smm, string vrstaZahteve, string statusZahteve, DateTime? datumOd, DateTime? datumDo, out Perun3WsLib.IskanjeZahteveStoritevService.EvidencaZahtevVrni[] zahteve)
        {
            zahteve = null;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());
                var response = svc.IskanjeZahteve(new Perun3Request(smm, vrstaZahteve, statusZahteve, datumOd, datumDo));
                if (response.Result == false)                
                    throw new Exception(response.Message);

                zahteve = (Perun3WsLib.IskanjeZahteveStoritevService.EvidencaZahtevVrni[])response.Data;
         
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }
        */
        public sFunctionResult Set_NeAkontacijski(string smm, bool neAkontacijski, DateTime datumOd)
        {            
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

                // servis dobi trenutno stanje
                var response = svc.NeAkontacijskiNajdi(new Perun3Request(smm));
                if (response.Result == false)                
                    throw new Exception(response.Message);
                
                var naciniObracuna = (Perun3WsLib.NeakontacijskiNacinObracunaNajdiService.NeakontacijskiNacinObracuna[])response.Data;
                if (naciniObracuna == null)
                    throw new Exception("Ni najdenih načinov obračuna!");

                if (!(naciniObracuna.Length > 0))
                    throw new Exception("Ni najdenih načinov obračuna!");

                var idPogRac = naciniObracuna[0].idPogRac;

                // servis za sprememebo
                if (neAkontacijski == true)
                {
                    response = svc.NeAkontacijskiPrijavi(new Perun3Request(smm, idPogRac, datumOd));
                }
                else
                {
                    response = svc.NeAkontacijskiOdjavi(new Perun3Request(smm, idPogRac, datumOd));
                }
                
                if (response.Result == false)
                    throw new Exception(response.Message);    
         
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }
        
        public sFunctionResult Get_NajdiZahteve(string smm, string vrstaZahteve, string statusZahteve, DateTime? datumOd, DateTime? datumDo, out Perun3WsLib.IskanjeZahteveStoritevService.EvidencaZahtevVrni[] zahteve)
        {
            zahteve = null;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

                // servis dobi trenutno stanje
                var response = svc.IskanjeZahteve(new Perun3Request(smm, vrstaZahteve, statusZahteve, datumOd, datumDo));
                if (response.Result == false)
                    throw new Exception(response.Message);

                zahteve = (Perun3WsLib.IskanjeZahteveStoritevService.EvidencaZahtevVrni[])response.Data;

            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult Get_FakturiranaRealizacija(string smm, out Perun3WsLib.FakturiranaRealizacijaService.PodatkiORealizacijiZaMesec[] real)
        {
            real = null;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

                // servis dobi trenutno stanje
                var response = svc.GetFakturiranaRealizacija(new Perun3Request(smm));
                if (response.Result == false)
                    throw new Exception(response.Message);

                real = (Perun3WsLib.FakturiranaRealizacijaService.PodatkiORealizacijiZaMesec[])response.Data;                
                //
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");
        }

        public sFunctionResult NovaPoD(dcNovaPoD zahteva)
        {
           
            var response = new Perun3Response();

            var v = new Perun3WsLib.NovaPoDService.NovaPoDZahteva();
           
            var message = string.Empty;
            try
            {
                v.datumSpremembe = zahteva.DatumSpremembe;               
                v.osnovniPodatki = new Perun3WsLib.NovaPoDService.osnovniPodatkiVloge();
                v.osnovniPodatki.datumVloge = zahteva.DatumVloge;
                v.osnovniPodatki.enotniIdentifikatorMM = zahteva.Smm;
                v.osnovniPodatki.opomba = zahteva.Opomba;
                v.osnovniPodatki.priloge = new Perun3WsLib.NovaPoDService.PrilogaVlogeZaPoD[1];
                v.osnovniPodatki.priloge[0] = new Perun3WsLib.NovaPoDService.PrilogaVlogeZaPoD();
                v.osnovniPodatki.priloge[0].dokument = zahteva.Priloga;
                v.osnovniPodatki.priloge[0].dokumentNaziv = zahteva.ImePriloge;
                v.osnovniPodatki.priloge[0].dokumentKoncnica = GetKoncnica(zahteva.ImePriloge); //Path.GetExtension(zahteva.ImePriloge);

                v.pogodbaODobavi = new Perun3WsLib.NovaPoDService.PogodbaODobavi();
                v.pogodbaODobavi.dokument = zahteva.PogodbaODobavi;
                v.pogodbaODobavi.dokumentNaziv = zahteva.ImePogodbe;
                v.pogodbaODobavi.dokumentKoncnica = GetKoncnica(zahteva.ImePogodbe); // Path.GetExtension(zahteva.ImePogodbe);

                v.zahtevaneVrsteSpremembe = new Perun3WsLib.NovaPoDService.spremembePoD();
                v.zahtevaneVrsteSpremembe.spremembaLastnika = zahteva.SpremembaLastnika;
                v.zahtevaneVrsteSpremembe.spremembaNaslovnika = zahteva.SpremembaNaslovnika;
                v.zahtevaneVrsteSpremembe.spremembaPlacnika = zahteva.SpremembaPlacnika;
               
                if (!string.IsNullOrEmpty(zahteva.Bremenitev))                            
                {
                    v.zahtevaneVrsteSpremembe.bremenitevSpecified = false;
                    if (zahteva.Bremenitev == "L")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.NovaPoDService.bremenitev.L;
                        v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    if (zahteva.Bremenitev == "S")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.NovaPoDService.bremenitev.S;
                        v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    if (!v.zahtevaneVrsteSpremembe.bremenitevSpecified)
                        throw new Exception("Neveljavna vrsta bremenitve!");
                }

                if (!string.IsNullOrEmpty(zahteva.VrstaObracuna))
                {
                    v.zahtevaneVrsteSpremembe.tarifaSpecified = false;
                    
                    if (zahteva.VrstaObracuna == "1T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.NovaPoDService.tarifa.Enotarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (zahteva.VrstaObracuna == "2T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.NovaPoDService.tarifa.Dvotarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (zahteva.VrstaObracuna == "3T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.NovaPoDService.tarifa.Tritarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (!v.zahtevaneVrsteSpremembe.tarifaSpecified)
                        throw new Exception(string.Format("Neveljavna vrsta obračuna [{0}]!", zahteva.VrstaObracuna));
                }

                if (!string.IsNullOrEmpty(zahteva.OdbirekEt))
                {
                    int et = 0;
                    if (int.TryParse(zahteva.OdbirekEt, out et))
                    {
                        v.odbirek = new Perun3WsLib.NovaPoDService.odbirek();
                        v.odbirek.ET = et;
                        v.odbirek.ETSpecified =true;
                    }                    
                }
                else if (!string.IsNullOrEmpty(zahteva.OdbirekMt) && !string.IsNullOrEmpty(zahteva.OdbirekVt))                      
                {
                    int vt = 0;
                    int mt = 0;
                    if (int.TryParse(zahteva.OdbirekVt, out vt) && int.TryParse(zahteva.OdbirekMt, out mt))
                    {
                        v.odbirek = new Perun3WsLib.NovaPoDService.odbirek();
                        v.odbirek.VT = vt;
                        v.odbirek.VTSpecified = true;

                        v.odbirek.MT = mt;
                        v.odbirek.MTSpecified = true;
                    }               
                }
              
                var req = new Perun3Request(v);
                req.Smm = zahteva.Smm;
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

                response = svc.NovaPoD(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");
        }

        private string GetKoncnica(string datoteka)
        {
            var koncnica = string.Empty;
            if (datoteka.Contains('.') && datoteka.Length > 3)
                koncnica = datoteka.Substring(datoteka.LastIndexOf('.') + 1);

            return koncnica;
        }

    }
}