using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceBase
{
    public class RequestBase        
    {
        public string Url;
        public string Method;
        public int Duration;        
        public void SetContext(string url, string method)
        {
            Url = url;
            Method = method;

        }

        public object Parm1, Parm2, Parm3, Parm4, Parm5, Parm6, Parm7, Parm8, Parm9;

        public RequestBase()
        {
            Duration = 0;
        }

        public RequestBase(object parm1) : this()
        {            
            Parm1 = parm1;
        }

        public RequestBase(object parm1, object parm2) : this(parm1)
        {            
            Parm2 = parm2;
        }

        public RequestBase(object parm1, object parm2, object parm3) : this (parm1, parm2)            
        {
            Parm3 = parm3;
        }

        public RequestBase(object parm1, object parm2, object parm3, object parm4) : this(parm1, parm2, parm3)
        {
            Parm4 = parm4;
        }

        public RequestBase(object parm1, object parm2, object parm3, object parm4, object parm5) : this(parm1, parm2, parm3, parm4)
        {
            Parm5 = parm5;
        }

        public RequestBase(object parm1, object parm2, object parm3, object parm4, object parm5, object parm6) : this(parm1, parm2, parm3, parm4, parm5)
        {
            Parm6 = parm6;
        }

        public RequestBase(object parm1, object parm2, object parm3, object parm4, object parm5, object parm6, object parm7) : this(parm1, parm2, parm3, parm4, parm5, parm6)
        {
            Parm7 = parm7;
        }
        public RequestBase(object parm1, object parm2, object parm3, object parm4, object parm5, object parm6, object parm7, object parm8) : this(parm1, parm2, parm3, parm4, parm5, parm6, parm7)
        {
            Parm8 = parm8;
        }
        public RequestBase(object parm1, object parm2, object parm3, object parm4, object parm5, object parm6, object parm7, object parm8, object parm9) : this(parm1, parm2, parm3, parm4, parm5, parm6, parm7, parm8)
        {
            Parm9 = parm9;
        }

       
    }
}
