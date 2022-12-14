using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZIEStoritveLib
{
    public class DobiDokument_response
    {
        public bool Error;
        public string Error_str;

        public DobiDokument_response(bool error , string error_str)
        {
            Error = error;
            Error_str = error_str;
        }
    }
}
