using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceBase
{
    interface IServiceProxy
    {
        void OnBeginRequest();
        void OnEndRequest();

    }
}
