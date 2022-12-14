﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Perun3WsLib;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;



namespace Komunikator3TavLib
{
    public class cPerun_EG2
    {
        String EGws_UserName;
        String EGws_Password;
        public cPerun_EG2(String _UserName, String _Password)
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
            var request = new Perun3Request();
            var response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

            try
            {
                //response = svc.VnosOdbirka(DobaviteljID, StevilkaMerilnegaMesta, DatumOdcitka, VrstaOdcitka, ET, DVT, DMT);
                response = svc.VnosOdbirkaEnostavni(DobaviteljID, StevilkaMerilnegaMesta, DatumOdcitka,VrstaOdcitka, ET, DVT, DMT, "B");
                //response = svc.VnosOdbirkaPerun2Compatible(DobaviteljID, StevilkaMerilnegaMesta, DatumOdcitka, VrstaOdcitka, ET, DVT, DMT);
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
            var request = new Perun3Request();
            var response = new Perun3Response();
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

        public sFunctionResult Set_VpisOdcitkaZaMerilnoMestoMenjava(string smm, String DatumOdcitka, Int32 VrstaOdcitka, String ET, String DVT, String DMT, String Vir)
        {
            var request = new Perun3Request();
            var response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

            try
            {
                response = svc.VnosOdbirkaEnostavniMenjava(smm, DatumOdcitka, VrstaOdcitka, ET, DVT, DMT, Vir);
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
        
        public sFunctionResult Set_ZahtevajRocnoOdcitavanjeEDP(string smm, string idPostopka)
        {
            var request = new Perun3Request(idPostopka);
            var response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

            try
            {
                request.Smm = smm;
                response = svc.MenjavaDobaviteljaZahtevajRocnoOdcitavanjeEDP(request);
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

            var request = new Perun3Request(smm, datumOdcitka);
            var response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());                                    
                        
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


        //public sFunctionResult Set_ObracunOdcitekOld(Int32 _MerilnoMesto, Int32 _DobaviteljID, short _DodajAkontacijo, out Int32? PrilogaA_ID)
        //{
        //    KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient PerunEG = new KomunikatorEGP.ws_EG_PerunEG.IwsPerunEGClient("BasicHttpBinding_IwsPerunEG");
        //    PrilogaA_ID = null;
        //    try
        //    {
        //        PerunEG.ClientCredentials.UserName.UserName = EGws_UserName;
        //        PerunEG.ClientCredentials.UserName.Password = EGws_Password;
        //        PerunEG.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        //        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //        KomunikatorEGP.ws_EG_PerunEG.dcResult Res;
        //        Res = PerunEG.Set_ObracunOdcitek(out PrilogaA_ID, _DobaviteljID, _MerilnoMesto, _DodajAkontacijo);
        //        try
        //        {
        //            PerunEG.Close();
        //            PerunEG = null;
        //        }
        //        catch (Exception)
        //        {
        //            PerunEG.Abort();
        //            PerunEG = null;
        //        }
        //        if (Res.RTC == 0)
        //        {
        //            return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
        //        }
        //        else
        //        {
        //            return cFunctionResult.Set(true, Res.RTC, Res.CONTROL, Res.MSG, "");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PerunEG.Abort();
        //        return cFunctionResult.Set(true, (Int32)efrErrorCodes.WebServiceError, ex.Message, ex.Message, "");
        //    }
        //}

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
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.GetZadnjiOdbirek3(new Perun3Request(Perun3Helpers.GetSmm(_IdDistribucije, _IdOdjemnagaMesta)));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                var data = (Perun3WsLib.PridobivanjeOdbirka3Service.OdbirekPridobi)response.Data;
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
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.GetZadnjiOdbirek3(new Perun3Request(smm));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

                var data = (Perun3WsLib.PridobivanjeOdbirka3Service.OdbirekPridobi)response.Data;
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

        private dcPerunOdcitek GetDcOdcitek(Perun3WsLib.PridobivanjeOdbirka3Service.OdbirekPridobi odcitekWs)
        {
            dcPerunOdcitek odcitek = new dcPerunOdcitek();
            odcitek.OdcitekID = null;
            //odcitek.SMM = odcitekWs.idMM;
            odcitek.DatumStanja = odcitekWs.datumOdcitavanja;

            odcitek.StanjeET = string.Empty;
            odcitek.StanjeVT = string.Empty;
            odcitek.StanjeMT = string.Empty;

            foreach (var o in odcitekWs.registerVrednost)
            {
                switch ((RegisterOdbirekEnum)o.idRegister)
                {
                    case RegisterOdbirekEnum.ET:
                        odcitek.StanjeET = o.vrednost.ToString(); break;
                    case RegisterOdbirekEnum.VT:
                        odcitek.StanjeVT = o.vrednost.ToString(); break;
                    case RegisterOdbirekEnum.MT:
                        odcitek.StanjeMT = o.vrednost.ToString(); break;
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
            var request = new Perun3Request();
            var response = new Perun3Response();         
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
   
            seznamPostavkaIzracuna = null;
            prilogaa_id = null;
            int porabaEt = 0, porabaVt = 0, porabaMt = 0;
            try
            {
                response = svc.GetInformativniObracunPerunV2(smm, datumOdcitkaStari, datumOdcitkaNovi, obracunskaMoc, kolicinaET, kolicinaDVT, kolicinaDMT, ref porabaEt, ref porabaVt, ref porabaMt, 16000);
                //response.Result = false;
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
                    //if (kolicinaET.HasValue && kolicinaET > 0)
                    //{
                    //    xml = bis.GetPrilogaATemplate1T();
                    //}
                    //else
                    //{
                    //    xml = bis.GetPrilogaATemplate();
                    //}

                    xml = bis.GetPrilogaATemplate();
                    if (kolicinaET.HasValue)
                    {
                        if (kolicinaET.Value > 0)
                            xml = bis.GetPrilogaATemplate1T();
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

                //decimal vt = 0, mt = 0, et = 0;


                decimal vt = (kolicinaDVT.HasValue ? kolicinaDVT.Value : 0);
                decimal mt = (kolicinaDMT.HasValue ? kolicinaDMT.Value : 0);
                decimal et = (kolicinaET.HasValue ? kolicinaET.Value : 0);

                // handler za negativne vrednosti - tako pošljejo iz mobilne app
                vt = (vt < 0 ? 0 : vt);
                mt = (mt < 0 ? 0 : mt);
                et = (et < 0 ? 0 : et);

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
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina =  porabaVt;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = vt;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;                        
                                                  
                            break;
                        case 5:                           
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = porabaMt;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = mt;
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;                          
                            break;
                        case 6:
                            
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                            pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                            //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = porabaEt;
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

        public sFunctionResult Get_InformativniIzracunInfo(string smm, string datumOdcitkaStari, string datumOdcitkaNovi, int obracunskaMoc, Int32? kolicinaET, Int32? kolicinaDVT, Int32? kolicinaDMT, out KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList seznamPostavkaIzracuna, out Int32? prilogaa_id)
        {
            var request = new Perun3Request();
            var response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

            seznamPostavkaIzracuna = null;
            prilogaa_id = null;
            int porabaEt = 0, porabaVt = 0, porabaMt = 0;
            // bis lib init
            BisLib bis = new BisLib(cSettings.Get_3Tav_ConnString());

            var xml = string.Empty;
            PrilogeA pa = new PrilogeA();
            XmlSerializer serializer = new XmlSerializer(pa.GetType());


            try
            {
                xml = bis.GetPrilogaATemplateInfo();
                if (kolicinaET.HasValue)
                {
                    if (kolicinaET.Value > 0)
                        xml = bis.GetPrilogaATemplate1TInfo();
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
            pa.PrilogaA[0].StevilkaMerilnegaMesta = smm.Substring(2, smm.Length - 2);
            pa.PrilogaA[0].Splosno.ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
            pa.PrilogaA[0].Splosno.ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);

            //decimal vt = 0, mt = 0, et = 0;


            decimal vt = (kolicinaDVT.HasValue ? kolicinaDVT.Value : 0);
            decimal mt = (kolicinaDMT.HasValue ? kolicinaDMT.Value : 0);
            decimal et = (kolicinaET.HasValue ? kolicinaET.Value : 0);

            // handler za negativne vrednosti - tako pošljejo iz mobilne app
            vt = (vt < 0 ? 0 : vt);
            mt = (mt < 0 ? 0 : mt);
            et = (et < 0 ? 0 : et);

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
                        //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina =  porabaVt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = vt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;

                        break;
                    case 5:
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                        //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = porabaMt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = mt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;
                        break;
                    case 6:

                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                        //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = porabaEt;
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
                    xml = xml.Replace(@"<PrilogeA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>",
                                        @"<PrilogeA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:noNamespaceSchemaLocation='http://www.informatika.si/schema/prilogaA_v2.4.xsd'>");
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

        public sFunctionResult Get_InformativniIzracunPaket(string smm, Int32 paketID, string datumOdcitkaStari, string datumOdcitkaNovi, int obracunskaMoc, Int32? kolicinaET, Int32? kolicinaDVT, Int32? kolicinaDMT, out KomunikatorEGP.ws_EG_PerunEG.dcPostavkaIzracunaList seznamPostavkaIzracuna, out Int32? prilogaa_id)
        {
            var request = new Perun3Request();
            var response = new Perun3Response();
            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

            seznamPostavkaIzracuna = null;
            prilogaa_id = null;
            int porabaEt = 0, porabaVt = 0, porabaMt = 0;
            // bis lib init
            BisLib bis = new BisLib(cSettings.Get_3Tav_ConnString());

            var xml = string.Empty;
            PrilogeA pa = new PrilogeA();
            XmlSerializer serializer = new XmlSerializer(pa.GetType());

            try
            {
                //xml = bis.GetPrilogaAZadnja(smm);
                if (kolicinaET.HasValue && kolicinaET > 0)
                {
                    xml = bis.GetPrilogaATemplate1TPaket();
                }
                else
                {
                    xml = bis.GetPrilogaATemplatePaket();
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
            pa.PrilogaA[0].StevilkaMerilnegaMesta = smm.Substring(2, smm.Length - 2);
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
                        //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina =  porabaVt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = vt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;

                        break;
                    case 5:
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                        //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = porabaMt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = mt;
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Znesek = pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina * pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Cena.Cena;
                        break;
                    case 6:

                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeOd = Convert.ToDateTime(datumOdcitkaStari);
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].ObdobjeDo = Convert.ToDateTime(datumOdcitkaNovi);
                        //pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = porabaEt;
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
                        pa.PrilogaA[0].ObracunskiPodatki.ObracunVrstica[i].Kolicina = sumOmreznina > 0 ? sumOmreznina : et > 0 ? et : vt + mt;
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
                    xml = xml.Replace(@"<PrilogeA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>",
                                        @"<PrilogeA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:noNamespaceSchemaLocation='http://www.informatika.si/schema/prilogaA_v2.4.xsd'>");
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

        public sFunctionResult Get_PodatkiMM(string smm, out Perun3WsLib.PridobivanjePodatkovMM5Service.PodatkiMerilnegaMesta podatkiMM)
        {
            podatkiMM = null;

            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());                       
                var response = svc.GetPodatkiMM5(new Perun3Request(smm));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                podatkiMM = ((Perun3WsLib.PridobivanjePodatkovMM5Service.VrniPodatkeMMOdgovor)response.Data).podatkiMM;
             
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
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.GetPDP2(new Perun3Request(smm));
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                var p = (Perun3WsLib.Pridobivanje2PDPService.Pdp)response.Data;

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
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.VnosPDP2(new Perun3Request(pdp.SMM, CreatePerun3Pdp2(pdp)));
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

        public sFunctionResult Vnos_PDP_Cakalnica(dcPovprecnaPorabaPerun3 pdp)
        {
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                                
                var request = CreatePDPRequest(pdp).OuterXml;

                var filename = string.Format("{0}{1}.xml", @"c:\\temp\\", string.Format("{0}_{1}", "Vnos_PDP_Cakalnica", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                File.WriteAllText(filename, request);


                var response = new Perun3Response();
                try
                {
                    var webRequest = CreateWebRequest(Settings.GetUrl("Vnos_PDP_Cakalnica"));

                    var sb = new StringBuilder();

                    sb.Append(request);


                    string postHeader = sb.ToString();
                    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
                    long length = postHeaderBytes.Length;


                    webRequest.ContentLength = length;
                    webRequest.AutomaticDecompression = DecompressionMethods.GZip;

                    using (var requestStream = webRequest.GetRequestStream())
                    {
                        requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    }


                    var responseXml = string.Empty;
                    using (var res = webRequest.GetResponse())
                    {
                        using (Stream stream = res.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                            responseXml = reader.ReadToEnd();
                        }
                    }
                                   
                }
                catch (WebException wex)
                {
                    var message = new StreamReader(wex.Response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                    message = HttpUtility.HtmlDecode(message);
                    message = GetErrorMessage(message);         
                    throw new Exception(string.Format("{0} [{1}]", message, 10));
                }

                if (response.Result == false)
                {
                    throw new Exception(string.Format("{0} [{1}]", response.Message, 20));
                }
             
            }
            catch (Exception ex)
            {
                
                var filename = string.Format("{0}{1}.xml", @"c:\\temp\\", string.Format("{0}_{1}", "Vnos_PDP_Cakalnica_Response", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                File.WriteAllText(filename, ex.Message);
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public string GetErrorMessage(string perunError)
        {
            if (perunError == null)
                perunError = "null";

            // errorji nove verzije
            try
            {
                if (perunError.Contains("BusinessObject"))
                {
                    var identifier = ", Sporocilo=";
                    var startIndex = perunError.IndexOf(identifier) + identifier.Length;
                    var length = perunError.IndexOf("]", startIndex) - startIndex - 1;
                    perunError = perunError.Substring(startIndex, length);
                }
            }
            catch (Exception ex)
            {
                // silent fail, vrne originalno sporočilo
            }

            return perunError;
        }

        private HttpWebRequest CreateWebRequest(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/soap+xml;charset=UTF-8";
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");             
            webRequest.Headers.Add("MIME-Version", "1.0");

            //webRequest.Method = "POST";
            //webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            //webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            //webRequest.Headers.Add("SOAPAction", "http://informatika.si/podatkovnaDomena/evidencaZahtev/storitve/4.0/dodaj");
            //webRequest.ContentLength = data.Length;


            return webRequest;
        }

        public XmlDocument CreatePDPRequest(dcPovprecnaPorabaPerun3 pdp)
        {
            var template = string.Empty;
            var username = Settings.GetUsername();
            var password = Settings.GetPassword();

            var envelope = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns=""http://informatika.si/izmenjavaPodatkov/evidencaZahtev/storitve/sporocila/4.0"" xmlns:ns1=""http://informatika.si/izmenjavaPodatkov/evidencaZahtev/sheme/4.0"">";
            var header = string.Format(@"<soapenv:Header>
                                            <wsse:Security xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"">
                                                <wsse:UsernameToken xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
                                                <wsse:Username>{0}</wsse:Username>
                                                <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{1}</wsse:Password>
                                                </wsse:UsernameToken>
                                            </wsse:Security>
                                        </soapenv:Header>", username, password);


            if (pdp.PorabaET > 0)
            {
                template = envelope + header + @"<soapenv:Body>
                                    <ns:DodajZahteva>
                                        <ns:evidencaZahtev>            
                                        <ns1:oznakaDobavitelja>6</ns1:oznakaDobavitelja>
                                        <ns1:idVrstaZahteve>12</ns1:idVrstaZahteve>
                                        <ns1:identifikatorMM>               
                                            <ns1:enotniIdentifikatorMM>{0}</ns1:enotniIdentifikatorMM>               
                                        </ns1:identifikatorMM>
                                        <ns1:datumZahteve>{1}</ns1:datumZahteve>            
                                        <ns1:xmlZahteve>
                                        <![CDATA[
				                            <ns:VnosPDPZahteva xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns=""http://informatika.si/izmenjavaPodatkov/pridobivanjeInVnosPDP/storitve/sporocila/2.0"" xmlns:ns1=""http://informatika.si/izmenjavaPodatkov/pridobivanjeInVnosPDP/sheme/2.0"" xmlns:ns2=""http://informatika.si/obracun/povprecnaDnevnaPoraba/sheme/1.0""> 
					                            <ns:identifikatorMM>            
						                            <ns1:enotniIdentifikatorMM>{2}</ns1:enotniIdentifikatorMM>            
					                            </ns:identifikatorMM>         
					                            <ns:oznakaDobavitelja>6</ns:oznakaDobavitelja>
					                            <ns:pdp>
						                            <ns2:idMM>{3}</ns2:idMM>
						                            <ns2:idPodjetje>{4}</ns2:idPodjetje>
						                            <ns2:obdobjeOd>{5}</ns2:obdobjeOd>
						                            <ns2:obdobjeDo>{6}</ns2:obdobjeDo>
						                            <ns2:idRazlPovpPor>{7}</ns2:idRazlPovpPor>                        
						                            <ns2:tipMeritveMnVrednost>
						                                <ns2:id>1</ns2:id>
						                                <ns2:vrednost>{8}</ns2:vrednost>               
						                                <ns2:oznakaEM>kWh</ns2:oznakaEM>               
						                                <ns2:nazivRegistra>Energija ET</ns2:nazivRegistra>
						                            </ns2:tipMeritveMnVrednost>
						                            <ns2:userGui></ns2:userGui>
					                            </ns:pdp>
				                            </ns:VnosPDPZahteva>
				                            ]]>
                                        </ns1:xmlZahteve>            
                                        </ns:evidencaZahtev>
                                    </ns:DodajZahteva>
                                </soapenv:Body>
                            </soapenv:Envelope>";
            }
            else
            {
                template = envelope + header + @"<soapenv:Body>                                
                                    <ns:DodajZahteva>
                                        <ns:evidencaZahtev>            
                                        <ns1:oznakaDobavitelja>6</ns1:oznakaDobavitelja>
                                        <ns1:idVrstaZahteve>12</ns1:idVrstaZahteve>
                                        <ns1:identifikatorMM>               
                                            <ns1:enotniIdentifikatorMM>{0}</ns1:enotniIdentifikatorMM>               
                                        </ns1:identifikatorMM>
                                        <ns1:datumZahteve>{1}</ns1:datumZahteve>            
                                        <ns1:xmlZahteve>
                                        <![CDATA[
				                            <ns:VnosPDPZahteva xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns=""http://informatika.si/izmenjavaPodatkov/pridobivanjeInVnosPDP/storitve/sporocila/2.0"" xmlns:ns1=""http://informatika.si/izmenjavaPodatkov/pridobivanjeInVnosPDP/sheme/2.0"" xmlns:ns2=""http://informatika.si/obracun/povprecnaDnevnaPoraba/sheme/1.0""> 
					                            <ns:identifikatorMM>            
						                            <ns1:enotniIdentifikatorMM>{2}</ns1:enotniIdentifikatorMM>            
					                            </ns:identifikatorMM>         
					                            <ns:oznakaDobavitelja>6</ns:oznakaDobavitelja>
					                            <ns:pdp>
						                            <ns2:idMM>{3}</ns2:idMM>
						                            <ns2:idPodjetje>{4}</ns2:idPodjetje>
						                            <ns2:obdobjeOd>{5}</ns2:obdobjeOd>
						                            <ns2:obdobjeDo>{6}</ns2:obdobjeDo>
						                            <ns2:idRazlPovpPor>{7}</ns2:idRazlPovpPor>                        
						                            <ns2:tipMeritveMnVrednost>
						                                <ns2:id>2</ns2:id>
						                                <ns2:vrednost>{8}</ns2:vrednost>               
						                                <ns2:oznakaEM>kWh</ns2:oznakaEM>               
						                                <ns2:nazivRegistra>Energija VT</ns2:nazivRegistra>
						                            </ns2:tipMeritveMnVrednost>
                                                    <ns2:tipMeritveMnVrednost>
						                                <ns2:id>3</ns2:id>
						                                <ns2:vrednost>{9}</ns2:vrednost>               
						                                <ns2:oznakaEM>kWh</ns2:oznakaEM>               
						                                <ns2:nazivRegistra>Energija MT</ns2:nazivRegistra>
						                            </ns2:tipMeritveMnVrednost>
						                            <ns2:userGui></ns2:userGui>
					                            </ns:pdp>
				                            </ns:VnosPDPZahteva>
				                            ]]>
                                        </ns1:xmlZahteve>            
                                        </ns:evidencaZahtev>
                                    </ns:DodajZahteva>
                                </soapenv:Body>
                            </soapenv:Envelope>";
            }
            var dateFormat = "yyyy-MM-dd";
            var request = string.Empty;
            if (pdp.PorabaET > 0)
            {
                request = string.Format(template, pdp.SMM, DateTime.Now.ToString(dateFormat), pdp.SMM, pdp.idMM, pdp.idPodjetje, Convert.ToDateTime(pdp.ObdobjeOd).ToString(dateFormat), Convert.ToDateTime(pdp.ObdobjeDo).ToString(dateFormat), pdp.IdRazlPovpPor, pdp.PorabaET.ToString().Replace(",", "."));
            }
            else
            {
                request = string.Format(template, pdp.SMM, DateTime.Now.ToString(dateFormat), pdp.SMM, pdp.idMM, pdp.idPodjetje, Convert.ToDateTime(pdp.ObdobjeOd).ToString(dateFormat), Convert.ToDateTime(pdp.ObdobjeDo).ToString(dateFormat), pdp.IdRazlPovpPor, pdp.PorabaVT.ToString().Replace(",", "."), pdp.PorabaMT.ToString().Replace(",", "."));
            }

            var soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml(request);

            return soapEnvelope;
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

        private dcPovprecnaPorabaPerun3 CreateDcPDP(string smm, Perun3WsLib.Pridobivanje2PDPService.Pdp p)
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


        private Perun3WsLib.VnosPDP2Service.Pdp CreatePerun3Pdp2(dcPovprecnaPorabaPerun3 p)
        {
            var pdp = new Perun3WsLib.VnosPDP2Service.Pdp();

            pdp.idMM = p.idMM;
            pdp.idPodjetje = p.idPodjetje;
            pdp.idRazlPovpPor = p.IdRazlPovpPor;
            pdp.nazivRazloga = p.NazivRazloga;
            pdp.obdobjeOd = Convert.ToDateTime(p.ObdobjeOd);
            pdp.obdobjeDo = Convert.ToDateTime(p.ObdobjeDo);
            pdp.userGui = string.Empty;

            int i = 0;
            pdp.tipMeritveMnVrednost = new Perun3WsLib.VnosPDP2Service.TipMeritveMnVrednost[GetPerun3PdpMeritveCount(p)];
            if (p.PorabaET.HasValue)
            {
                if (p.PorabaET.Value > 0)
                {
                    pdp.tipMeritveMnVrednost[i] = new Perun3WsLib.VnosPDP2Service.TipMeritveMnVrednost();
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
                    pdp.tipMeritveMnVrednost[i] = new Perun3WsLib.VnosPDP2Service.TipMeritveMnVrednost();
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
                    pdp.tipMeritveMnVrednost[i] = new Perun3WsLib.VnosPDP2Service.TipMeritveMnVrednost();
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

        public sFunctionResult MenjavaDob_OddajVlogoOld(dcMenjavaDobZahteva zahteva, out Int32? zahtevaId)
        {
            zahtevaId = null;
            
            var response = new Perun3Response();

            var v = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.VlogaZaMenjavoDobaviteljaTip();
            
            var message = string.Empty;
            try
            {

                v.identifikatorMM = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.IdentifikatorMM() { Item = zahteva.Smm };
                v.vkljucitevPodatkovMMSpecified = true;
                v.vkljucitevPodatkovMM = zahteva.PodatkiMM;
                v.datumMenjave = zahteva.DatumZacetka;
                v.datumVloge = DateTime.Now;

                v.placnik = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.PoslovniPartnerTip();

                // optional                
                v.placnik.naziv = "";
                v.placnik.naslov = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.NaslovTip();
                v.placnik.naslov.hisnaStevilka = "";
                v.placnik.naslov.kraj = "";
                v.placnik.naslov.posta = "";
                v.placnik.naslov.postnaStevilka = "";
                v.placnik.naslov.ulica = "";

                v.placnik.davcniPodatki = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.DavcniPodatkiTip();
                v.placnik.davcniPodatki.davcnaStevilka = "";
                //v.placnik.davcniPodatki.davcniZavezanec = true;

                var datoteka = (zahteva.ImePriloge == null ? string.Empty : zahteva.ImePriloge.Trim());

                var koncnica = string.Empty;
                if (datoteka.Contains('.') && datoteka.Length > 3)
                    koncnica = datoteka.Substring(datoteka.LastIndexOf('.') + 1);

                v.pogodbaODobavi = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.Priloga[1];
                v.pogodbaODobavi[0] = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.Priloga();
                v.pogodbaODobavi[0].casovniZig = DateTime.Now;
                v.pogodbaODobavi[0].dokumentKoncnica = koncnica;
                v.pogodbaODobavi[0].dokumentNaziv = zahteva.ImePriloge;
                v.pogodbaODobavi[0].dokument = (byte[])zahteva.Priloga;

                v.vrstaRacuna = Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.VlogaZaMenjavoDobaviteljaTipVrstaRacuna.S;
                
                if (zahteva.Bremenitev == "L")
                {
                    v.vrstaRacuna = Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.VlogaZaMenjavoDobaviteljaTipVrstaRacuna.L;
                }                                                

                Perun3Request req = new Perun3Request(v);
                req.Smm = zahteva.Smm;

                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

                response = svc.MenjavaDobaviteljaOddajaVloge2Old(req);
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

        public sFunctionResult MenjavaDob_OddajVlogo(dcMenjavaDobZahteva zahteva, out Int32? zahtevaId, out string idPostopka)
        {
            zahtevaId = null;
            idPostopka = null;

            var response = new Perun3Response();

            //var v = new Perun3WsLib.MenjavaDobaviteljaOddajaVloge2Service.VlogaZaMenjavoDobaviteljaTip();
            var v = new Perun3WsLib.MenjavaDobaviteljaService.OddajVlogoType();

            var z = new Perun3WsLib.MenjavaDobaviteljaService.OddajVlogoZahteva();


            var message = string.Empty;
            try
            {

                var distribucijaId = Convert.ToInt32(zahteva.Smm.Substring(0, 1));
                v.identifikatorMM = new Perun3WsLib.MenjavaDobaviteljaService.IdentifikatorMMType() { Item = zahteva.Smm };
                
                v.zahtevaniPodatkiMM = true;
                v.dis = distribucijaId;
                v.placnik = new Perun3WsLib.MenjavaDobaviteljaService.PoslovniPartnerType();

                //  zahtevano v novi verziji                
                v.placnik.naziv = zahteva.PlacnikNaziv;
                v.placnik.ulica = zahteva.PlacnikUlica;
                v.placnik.hisnaStevilka = zahteva.PlacnikHisnaStevilka;
                v.placnik.postnaStevilka = zahteva.PlacnikPostnaStevilka;
                v.placnik.posta = zahteva.PlacnikPosta;
                v.placnik.davcnaStevilka = zahteva.DavcnaSt;

                if (!string.IsNullOrEmpty(zahteva.PlacnikDavcniZavezanec))
                {
                    v.placnik.davcniZavezanec = (zahteva.PlacnikDavcniZavezanec.ToLower() == "true");
                }
                else
                {
                    v.placnik.davcniZavezanec = (zahteva.PlacnikDavcniZavezanec.ToLower() == "false");
                }
                v.placnik.davcniZavezanecSpecified = true;
                    

                 
                var datoteka = (zahteva.ImePriloge == null ? string.Empty : zahteva.ImePriloge.Trim());

                var koncnica = string.Empty;
                if (datoteka.Contains('.') && datoteka.Length > 3)
                    koncnica = datoteka.Substring(datoteka.LastIndexOf('.') + 1);

                
                v.pogodbe = new Perun3WsLib.MenjavaDobaviteljaService.DatotekaType[1];
                v.pogodbe[0] = new Perun3WsLib.MenjavaDobaviteljaService.DatotekaType();
                v.pogodbe[0].naziv = zahteva.ImePriloge.ToLower();
                v.pogodbe[0].datoteka = (byte[])zahteva.Priloga;

                // podvoji prilogo
                //v.priloge = new Perun3WsLib.MenjavaDobaviteljaService.DatotekaType[1];
                //v.pogodbe[0] = new Perun3WsLib.MenjavaDobaviteljaService.DatotekaType();
                //v.pogodbe[0].naziv = zahteva.ImePriloge;
                //v.pogodbe[0].datoteka = (byte[])zahteva.Priloga;



                v.vrstaMenjave = Perun3WsLib.MenjavaDobaviteljaService.VrstaMenjaveType.DOBAVITELJ_ODJEMA;
                


                //v.vrstaRacuna = new Perun3WsLib.MenjavaDobaviteljaService.VrstaRacunaType();
                v.vrstaRacuna = Perun3WsLib.MenjavaDobaviteljaService.VrstaRacunaType.SKUPNI;
                if (zahteva.Bremenitev == "L")                
                    v.vrstaRacuna = Perun3WsLib.MenjavaDobaviteljaService.VrstaRacunaType.LOCEN;
                v.vrstaRacunaSpecified = true;

                z.vloga = new Perun3WsLib.MenjavaDobaviteljaService.OddajVlogoType();
                z.vloga = v;

                Perun3Request req = new Perun3Request(z);
                req.Smm = zahteva.Smm;

                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());


                response = svc.MenjavaDobaviteljaOddajaVloge2(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }
                var ret = (string[])response.Data;

                zahtevaId = Convert.ToInt32(ret[0]);
                idPostopka = ret[1];

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

                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.MenjavaDobaviteljaNajdiVlogeCEPPS(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

                var vlogeOdgovor = (Perun3WsLib.MenjavaDobaviteljaService.NajdiVlogeOdgovor)response.Data;

                if (vlogeOdgovor != null)
                {
                    seznamVlog = new List<dcMenjavaDobVloga>();
                    foreach (var v in vlogeOdgovor.vloga)
                    {
                        seznamVlog.Add(new dcMenjavaDobVloga() { DatumMenjave = v.datumMenjave, DatumVloge = v.datumVloge, IdOpravila = v.idPostopka, Smm = v.enotniIdentifikatorMM, Status = Convert.ToString(v.status) });
                    }
                }
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");
        }


        public sFunctionResult MenjavaDob_NajdiVlogoSONDSEE(string smm, DateTime datumOd, DateTime datumDo, string nacinPridobitveOdbirka, string status, out List<dcMenjavaDobVloga> seznamVlog)
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

                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.MenjavaDobaviteljaNajdiVlogeCEPPS(req);
                if (response.Result == false)
                {
                    throw new Exception(response.Message);
                }

                var vlogeOdgovor = (Perun3WsLib.MenjavaDobaviteljaService.NajdiVlogeOdgovor)response.Data;

                if (vlogeOdgovor != null)
                {
                    seznamVlog = new List<dcMenjavaDobVloga>();
                    foreach (var v in vlogeOdgovor.vloga)
                    {
                        seznamVlog.Add(new dcMenjavaDobVloga() { DatumMenjave = v.datumMenjave, DatumVloge = v.datumVloge, IdOpravila = v.idPostopka, Smm = v.enotniIdentifikatorMM, Status = Convert.ToString(v.status) });
                    }
                }               
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");
        }
 
        public sFunctionResult OdpovedPogodbeODobavi(string smm,string sifraDobavitelja, DateTime datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result)
        {
            result = false;
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobavi3(new Perun3Request(smm, sifraDobavitelja, datum, priloga, prilogaNaziv, prilogaKoncnica));
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
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobaviOdjemalec3(new Perun3Request(smm, sifraDobavitelja, datum, priloga, prilogaNaziv, prilogaKoncnica));
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

        public sFunctionResult OdpovedPogodbeODobaviPretek(string smm, string sifraDobavitelja, DateTime datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, out bool result)
        {
            result = false;
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobaviPretek3(new Perun3Request(smm, sifraDobavitelja, datum, priloga, prilogaNaziv, prilogaKoncnica));
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

        public sFunctionResult OdpovedPogodbeODobaviSplosna(string smm, string sifraDobavitelja, DateTime datum, byte[] priloga, string prilogaNaziv, string prilogaKoncnica, int razlogOdpovedi, bool dolgOmreznina, out bool result)
        {
            result = false;
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobaviSplosna(new Perun3Request(smm, sifraDobavitelja, datum, priloga, prilogaNaziv, prilogaKoncnica, razlogOdpovedi, dolgOmreznina));
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

            var v = new Perun3WsLib.PrviPriklop2Service.PrviPriklopZahteva();

            var message = string.Empty;
            try
            {

                v.osnovniPodatki = new Perun3WsLib.PrviPriklop2Service.OsnovniPodatkiVloge();
                v.osnovniPodatki.datumVloge = vloga.DatumVloge;

                v.osnovniPodatki.identifikatorMM = new Perun3WsLib.PrviPriklop2Service.IdentifikatorMM() { Item = vloga.Smm };
                v.osnovniPodatki.opomba = vloga.Opomba;

                v.pogodbaODobavi = new Perun3WsLib.PrviPriklop2Service.PogodbaODobavi();
                v.pogodbaODobavi.dokument = vloga.PogodbaODobavi;
                v.pogodbaODobavi.dokumentNaziv = vloga.ImePogodbe;
                v.pogodbaODobavi.dokumentKoncnica = GetKoncnica(vloga.ImePogodbe);

                if (!string.IsNullOrEmpty(vloga.ImePriloge))
                {

                    v.osnovniPodatki.priloge = new Perun3WsLib.PrviPriklop2Service.PrilogaVlogeZaPoD[1];
                    v.osnovniPodatki.priloge[0] = new Perun3WsLib.PrviPriklop2Service.PrilogaVlogeZaPoD();
                    v.osnovniPodatki.priloge[0].dokument = vloga.Priloga;
                    v.osnovniPodatki.priloge[0].dokumentNaziv = vloga.ImePriloge;
                    v.osnovniPodatki.priloge[0].dokumentKoncnica = GetKoncnica(vloga.ImePriloge);
                }

                v.zahtevaneVrsteSpremembe = new Perun3WsLib.PrviPriklop2Service.spremembePoD();
                v.zahtevaneVrsteSpremembe.spremembaLastnika = vloga.SpremembaLastnika;
                v.zahtevaneVrsteSpremembe.spremembaNaslovnika = vloga.SpremembaNaslovnika;
                v.zahtevaneVrsteSpremembe.spremembaPlacnika = vloga.SpremembaPlacnika;

                if (!string.IsNullOrEmpty(vloga.Bremenitev))
                {
                    v.zahtevaneVrsteSpremembe.bremenitevSpecified = false;
                    if (vloga.Bremenitev == "L")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.PrviPriklop2Service.bremenitev.L;
                        v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    if (vloga.Bremenitev == "S")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.PrviPriklop2Service.bremenitev.S;
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
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.PrviPriklop2Service.tarifa.Enotarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (vloga.VrstaObracuna == "2T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.PrviPriklop2Service.tarifa.Dvotarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (vloga.VrstaObracuna == "3T")
                    {
                        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.PrviPriklop2Service.tarifa.Tritarifni;
                        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                    }

                    if (!v.zahtevaneVrsteSpremembe.tarifaSpecified)
                        throw new Exception(string.Format("Neveljavna vrsta obračuna [{0}]!", vloga.VrstaObracuna));
                }

          


                var req = new Perun3Request(v);
                req.Smm = vloga.Smm;
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

                response = svc.PrviPriklop2(req);
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
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var response = svc.OdpovedPogodbeODobaviPreklic2(new Perun3Request(smm, sifraDobavitelja, datum, kontaktnaOseba, opomba));
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

        public sFunctionResult Set_NeAkontacijski(string smm, bool neAkontacijski, DateTime datumOd)
        {            
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

                // servis dobi trenutno stanje
                var response = svc.NeAkontacijskiNajdi2(new Perun3Request(smm));
                if (response.Result == false)                
                    throw new Exception(response.Message);
                
                var naciniObracuna = (Perun3WsLib.NeakontacijskiNacinObracunaNajdi2Service.NeakontacijskiNacinObracuna[])response.Data;
                if (naciniObracuna == null)
                    throw new Exception("Ni najdenih načinov obračuna!");

                if (!(naciniObracuna.Length > 0))
                    throw new Exception("Ni najdenih načinov obračuna!");

                var idPogRac = naciniObracuna[0].idPogRac;

                // servis za sprememebo
                if (neAkontacijski == true)
                {
                    response = svc.NeAkontacijskiPrijavi2(new Perun3Request(smm, idPogRac, datumOd));
                }
                else
                {
                    response = svc.NeAkontacijskiOdjavi2(new Perun3Request(smm, idPogRac, datumOd));
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

        public sFunctionResult Get_NajdiZahteve(string smm, string vrstaZahteve, string statusZahteve, DateTime? datumOd, DateTime? datumDo, out Perun3WsLib.EvidencaZahtevNajdiZahteva2Service.EvidencaZahtevVrni[] zahteve)
        {
            zahteve = null;
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

                // servis dobi trenutno stanje
                var response = svc.IskanjeZahteve2(new Perun3Request(smm, vrstaZahteve, statusZahteve, datumOd, datumDo));
                if (response.Result == false)
                    throw new Exception(response.Message);

                zahteve = (Perun3WsLib.EvidencaZahtevNajdiZahteva2Service.EvidencaZahtevVrni[])response.Data;

            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(false, (Int32)efrErrorCodes.WebServiceError, string.Empty, ex.Message, "");
            }

            return cFunctionResult.Set(true, 0, string.Empty, string.Empty, "");

        }

        public sFunctionResult Get_FakturiranaRealizacija(string smm, out Perun3WsLib.FakturiranaRealizacija2Service.PodatkiORealizacijiZaMesec[] real)
        {
            real = null;
            try
            {
                var svc = new Perun3Service(cSettings.Get_3Tav_ConnString());

                // servis dobi trenutno stanje
                var response = svc.GetFakturiranaRealizacija(new Perun3Request(smm));
                if (response.Result == false)
                    throw new Exception(response.Message);

                real = (Perun3WsLib.FakturiranaRealizacija2Service.PodatkiORealizacijiZaMesec[])response.Data;                
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

            var v = new Perun3WsLib.NovaPoD2Service.NovaPoDZahteva();
           
            var message = string.Empty;
            try
            {
                v.datumSpremembe = zahteva.DatumSpremembe;
                v.osnovniPodatki = new Perun3WsLib.NovaPoD2Service.OsnovniPodatkiVloge();
                v.osnovniPodatki.datumVloge = zahteva.DatumVloge;
                v.osnovniPodatki.identifikatorMM = new Perun3WsLib.NovaPoD2Service.IdentifikatorMM() { Item = zahteva.Smm };
                v.osnovniPodatki.opomba = (String.IsNullOrEmpty(zahteva.Opomba) ? string.Empty : zahteva.Opomba);

                if (!string.IsNullOrEmpty(zahteva.ImePriloge))
                {
                    v.osnovniPodatki.priloge = new Perun3WsLib.NovaPoD2Service.PrilogaVlogeZaPoD[1];
                    v.osnovniPodatki.priloge[0] = new Perun3WsLib.NovaPoD2Service.PrilogaVlogeZaPoD();
                    v.osnovniPodatki.priloge[0].dokument = zahteva.Priloga;
                    v.osnovniPodatki.priloge[0].dokumentNaziv = zahteva.ImePriloge;
                    v.osnovniPodatki.priloge[0].dokumentKoncnica = GetKoncnica(zahteva.ImePriloge); //Path.GetExtension(zahteva.ImePriloge);
                }
                
                v.pogodbaODobavi = new Perun3WsLib.NovaPoD2Service.PogodbaODobavi();
                v.pogodbaODobavi.dokument = zahteva.PogodbaODobavi;
                v.pogodbaODobavi.dokumentNaziv = zahteva.ImePogodbe;
                v.pogodbaODobavi.dokumentKoncnica = GetKoncnica(zahteva.ImePogodbe); // Path.GetExtension(zahteva.ImePogodbe);

                v.zahtevaneVrsteSpremembe = new Perun3WsLib.NovaPoD2Service.spremembeNovaPoD();
                v.zahtevaneVrsteSpremembe.spremembaLastnika = zahteva.SpremembaLastnika;
                v.zahtevaneVrsteSpremembe.spremembaNaslovnika = zahteva.SpremembaNaslovnika;
                v.zahtevaneVrsteSpremembe.spremembaPlacnika = zahteva.SpremembaPlacnika;
               
                if (!string.IsNullOrEmpty(zahteva.Bremenitev))                            
                {
                    //v.zahtevaneVrsteSpremembe.b= false;
                    if (zahteva.Bremenitev == "L")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.NovaPoD2Service.bremenitev.L;
                        //v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    if (zahteva.Bremenitev == "S")
                    {
                        v.zahtevaneVrsteSpremembe.bremenitev = Perun3WsLib.NovaPoD2Service.bremenitev.S;
                        //v.zahtevaneVrsteSpremembe.bremenitevSpecified = true;
                    }

                    //if (!v.zahtevaneVrsteSpremembe.bremenitevSpecified)
                    //    throw new Exception("Neveljavna vrsta bremenitve!");
                }

                

                //if (!string.IsNullOrEmpty(zahteva.VrstaObracuna))
                //{
                //    //v.zahtevaneVrsteSpremembe.tarifaSpecified = false;
                    
                //    if (zahteva.VrstaObracuna == "1T")
                //    {
                //        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.NovaPoD2Service.tarifa.Enotarifni;
                //        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                //    }

                //    if (zahteva.VrstaObracuna == "2T")
                //    {
                //        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.NovaPoD2Service.tarifa.Dvotarifni;
                //        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                //    }

                //    if (zahteva.VrstaObracuna == "3T")
                //    {
                //        v.zahtevaneVrsteSpremembe.tarifa = Perun3WsLib.NovaPoD2Service.tarifa.Tritarifni;
                //        v.zahtevaneVrsteSpremembe.tarifaSpecified = true;
                //    }

                //    if (!v.zahtevaneVrsteSpremembe.tarifaSpecified)
                //        throw new Exception(string.Format("Neveljavna vrsta obračuna [{0}]!", zahteva.VrstaObracuna));
                //}

                if (!string.IsNullOrEmpty(zahteva.OdbirekEt))
                {
                    int et = 0;
                    if (int.TryParse(zahteva.OdbirekEt, out et))
                    {
                        v.odbirek = new Perun3WsLib.NovaPoD2Service.odbirek();
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
                        v.odbirek = new Perun3WsLib.NovaPoD2Service.odbirek();
                        v.odbirek.VT = vt;
                        v.odbirek.VTSpecified = true;

                        v.odbirek.MT = mt;
                        v.odbirek.MTSpecified = true;
                    }               
                }
              
                var req = new Perun3Request(v);
                req.Smm = zahteva.Smm;
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());

                response = svc.NovaPoD2(req);
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

        public sFunctionResult OddajaDopolnjeneVloge(dcNovaPoD zahteva)
        {

            var response = new Perun3Response();

            var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
            var z = new Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.VlogaZaMenjavoDobaviteljaTip();

            var message = string.Empty;
            try
            {
                /***********************************************
                *  translacija na Perun podatkovne tipe
                ***********************************************/      
                z.datumMenjave = zahteva.DatumSpremembe;
                z.datumVloge = zahteva.DatumVloge;

                z.identifikatorMM = new Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.IdentifikatorMM();
                z.identifikatorMM.Item = zahteva.Smm;

                if (zahteva.PogodbaODobavi != null)
                {
                    z.pogodbaODobavi = new Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.Priloga[1];
                    z.pogodbaODobavi[0] = new Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.Priloga();
                    z.pogodbaODobavi[0].casovniZig = DateTime.Now;
                    z.pogodbaODobavi[0].dokument = zahteva.PogodbaODobavi;
                    z.pogodbaODobavi[0].dokumentKoncnica = GetKoncnica(zahteva.ImePogodbe);  
                    z.pogodbaODobavi[0].dokumentNaziv = zahteva.ImePogodbe;

                }

                if (zahteva.Priloga != null)
                {
                    z.priloge = new Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.Priloga[1];
                    z.priloge[0] = new Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.Priloga();
                    z.priloge[0].casovniZig = DateTime.Now;
                    z.priloge[0].dokument = zahteva.Priloga;
                    z.priloge[0].dokumentKoncnica = GetKoncnica(zahteva.ImePriloge);  
                    z.priloge[0].dokumentNaziv = zahteva.ImePriloge;
                }
                
                z.vkljucitevPodatkovMM = true;
                z.vkljucitevPodatkovMMSpecified = true;

                z.vrstaRacuna = Perun3WsLib.MenjavaDobaviteljOddajaDopolnjeneVloge2Service.VlogaZaMenjavoDobaviteljaTipVrstaRacuna.S;

                bool prekinitevPostopka = (zahteva.PrekinitevPostopka == 1);


                /***********************************************
                 *  splošni request
                ***********************************************/
                var request = new Perun3Request();
                request.Smm = zahteva.Smm;
                request.Parm1 = z;
                request.Parm2 = zahteva.IdOpravila;
                request.Parm3 = prekinitevPostopka;

                response = svc.OddajaDopolnjeneVloge2(request);
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

        public sFunctionResult VnosZahteveSplosno(dcNovaPoD zahteva)
        {
            var response = new Perun3Response();
            var message = string.Empty;
            try
            {
                var svc = new Perun3Service2(cSettings.Get_3Tav_ConnString());
                var request = new Perun3Request(zahteva.Smm, zahteva.SifraDobavitelja, zahteva.DatumVloge, zahteva.TipZahteve, zahteva.Opomba, zahteva.ImePogodbe, GetKoncnica(zahteva.ImePogodbe), zahteva.PogodbaODobavi);
                response = svc.VnosZahteveSplosno(request);
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