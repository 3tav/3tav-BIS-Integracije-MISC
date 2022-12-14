using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Reflection;

using ServiceBase;
using KroClientLib;



namespace KroProxyASP
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://3tav.si/kro")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class KroProxyService : ServiceProxyBase
    {

        [WebMethod]
        public string Test()
        {
            return "Kro service up";
        }

        [WebMethod]
        public dcResult GetFakturaPdf(int tip, string xmlName,string xmlContent, int idFakture, string posiljatelj, out byte[] fakturaPdf)
        {
            dcResult response = new dcResult();
            var request = new KroRequest(tip);
            request.SetContext(GetRequestUrl(), MethodInfo.GetCurrentMethod().Name);
            request.XmlName = xmlName;

            fakturaPdf = null;

            try
            {
                var c = GetServiceProxy();
                OnBeginRequest(request);
                fakturaPdf = c.GetFakturaPdf(tip, xmlName, idFakture, xmlContent);
            }
            catch (Exception ex)
            {
                response = CreateResponse((int)efrErrorCodes.UnknownError, "Error", ex.Message);
            }

            OnEndRequest(request, response);
            return response;
        }

        // vrne inicializiran service proxy
        public KroService GetServiceProxy()
        {
            var c = new KroService();
            c.Init();            
            return c;            
        }

        private dcResult CreateResponse(int rtc, string control, string msg)
        {
            var response = new dcResult();
            response.RTC = rtc;
            response.CONTROL = control;
            response.MSG = msg;
            return response;
        }
    }
}