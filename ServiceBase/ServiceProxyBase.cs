using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.Services;

namespace ServiceBase
{
    public class UserCredentials : System.Web.Services.Protocols.SoapHeader
    {
        public string userName;
        public string password;
    }

    public class ServiceProxyBase : WebService
    {
        public UserCredentials Credentials;
        public int Elapsed {get {return _elapsed; }}
        private int _elapsed;

        public void OnBeginRequest(RequestBase request)
        {
            var message = string.Empty;
            var result = true;

            try
            {
             //   CheckCredentials();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = false;
            }

            if (result == false)
                throw new Exception(message);

            // start request timing
            _elapsed = Environment.TickCount;
        }

        public virtual void OnEndRequest(RequestBase request, object response)
        {
            try
            {
                if (_elapsed > 0)
                    request.Duration = Environment.TickCount - _elapsed;                
            }
            catch (Exception ex)
            {
                // supress
            }
        }
        
        public string GetRequestUrl()
        { 
         
            return HttpContext.Current.Request.Url.AbsoluteUri;        
        }

        private void CheckCredentials()
        {
            if (Credentials == null)
                throw new Exception("Avtentikacija: Missing Credentials data.");

            if (Credentials.userName.ToUpper() != "3tav".ToUpper() || Credentials.password != "3t@v")
                throw new Exception("Avtentikacija: Napačno uporabniško ime ali geslo.");                                                                

        }               
    }
}
