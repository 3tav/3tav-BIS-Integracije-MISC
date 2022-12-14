using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceBase
{
    public class RequestContext
    {
        public string Url;
        public string Method;

        public RequestContext(string url, string method)
        {
            Url = url;
            Method = method;
        }
    }
}
