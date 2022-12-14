using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceBase
{
    public class ResponseBase : IResponseBase
    {
        public bool Result { get; set; }
        public string Message { get; set; }        
        public object Data{ get; set; }

        public ResponseBase() : this(true, string.Empty)
        {            
        }

        public ResponseBase(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
