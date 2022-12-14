using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Reflection;

using Komunikator3TavLib;
using EppsClient;
using ServiceBase;

namespace EppsProxyASP
{    
    [WebService(Namespace = "http://3tav.si/epps")]    
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]    
    public class EppsProxyService : ServiceProxyBase
    {


        [WebMethod]
        [SoapHeader("Credentials", Required = true)]
        public string TestLogin()
        {
            var status = string.Empty;            
            try
            {
                status = GetServiceProxy().TestLogin();
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return status;
        }


        [WebMethod]     
        [SoapHeader("Credentials", Required = true)]
        public dcResult Login(string username, string password, out string token)
        {
            dcResult response = new dcResult();
            var request = new EppsRequest(username, password);
            request.SetContext(GetRequestUrl(), MethodInfo.GetCurrentMethod().Name);
            token = string.Empty;            
            try
            {
                OnBeginRequest(request);
                token = GetServiceProxy().Login(username, password);
            }
            catch (Exception ex)
            {
                response = CreateResponse((int)efrErrorCodes.UnknownError, "Error", ex.Message);  
            }

            OnEndRequest(request, response);
            return response;
        }

        [WebMethod]
        [SoapHeader("Credentials", Required = true)]
        public dcResult GetFakturaPdf(int izpisId, string fileName, string fileContent, out byte[] fakturaPdf)
        {
            dcResult response = new dcResult();
            var request = new EppsRequest(izpisId);
            request.SetContext(GetRequestUrl(), MethodInfo.GetCurrentMethod().Name);
            request.Filename = fileName;

            fakturaPdf = null;
                        
            try
            {
                var c = GetServiceProxy();
                OnBeginRequest(request);                
                fakturaPdf = c.CreatePDF(izpisId, fileName, fileContent);                
            }
            catch (Exception ex)
            {
                response = CreateResponse((int)efrErrorCodes.UnknownError, "Error", ex.Message);
            }

            OnEndRequest(request, response);
            return response;
        }

        
        [WebMethod]
        [SoapHeader("Credentials", Required = true)]
        public dcResult GetFakturaPdfLocalXML(int izpisId, string fileName, int? idZbirnika, int? idFakture, DateTime? paket, out byte[] fakturaPdf)
        {
            dcResult response = new dcResult();
            var request = new EppsRequest(izpisId);
            request.SetContext(GetRequestUrl(), MethodInfo.GetCurrentMethod().Name);            
            fakturaPdf = null;

            try
            {            
                var c = GetServiceProxy();
                request.Filename = string.Format("ece{0}.xml", izpisId);
                OnBeginRequest(request);
                fakturaPdf = c.CreatePDF(izpisId, request.Filename, idZbirnika, idFakture, paket);                                
            }
            catch (Exception ex)
            {
                response = CreateResponse((int)efrErrorCodes.UnknownError, "Error", ex.Message);
            }

            OnEndRequest(request, response);
            return response;
        }
        /*
        [WebMethod]
        [SoapHeader("Credentials", Required = true)]
        public dcResult GetFakturaPdfLocalXMLForceVPO(int izpisId, string fileName, int? idZbirnika, int? idFakture, DateTime? paket, out byte[] fakturaPdf)
        {
            dcResult response = new dcResult();
            var request = new EppsRequest(izpisId);
            request.SetContext(GetRequestUrl(), MethodInfo.GetCurrentMethod().Name);
            fakturaPdf = null;

            try
            {
                var c = GetServiceProxy();
                request.Filename = string.Format("ece{0}.xml", izpisId);
                OnBeginRequest(request);
                fakturaPdf = c.CreatePDF(izpisId, request.Filename, idZbirnika, idFakture, paket);
            }
            catch (Exception ex)
            {
                response = CreateResponse((int)efrErrorCodes.UnknownError, "Error", ex.Message);
            }

            OnEndRequest(request, response);
            return response;
        }
        */
        public EppsClient.EppsService GetServiceProxy()
        {
            var c = new EppsClient.EppsService();
            c.Init();
            //c.ClientIp = GetIPAddress();
            return c;            
            //return new EppsClient.EppsService();
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        private dcResult CreateResponse(int rtc, string control, string msg)
        {
            var response = new dcResult();
            response.RTC = rtc;
            response.CONTROL = control;
            response.MSG = msg;
            return response;
        }

        public void OnEndRequest(string url, string method, EppsRequest request, EppsResponse response)
        {
            /*
            try
            {
                if (base.Elapsed > 0)
                    request.Duration = Environment.TickCount - Elapsed;

                _log.Write(url, method, request, new Perun3Response(response.RTC == 0 ? true : false, GetMessage(response)));
            }
            catch (Exception ex)
            {
                if (Settings.GetSuppressExceptions() == false)
                    throw new Exception(ex.Message);
            }
            */ 
        }

    }           
}