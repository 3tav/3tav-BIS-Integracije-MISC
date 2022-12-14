using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoteamClientLib
{
    public enum Korak { Priprava = 0, Nabor = 1, Xml = 2, Init = 3, Close = 4, XmlMenjave = 5}

    public static class Methods {
        public const string PostMeterReadData = "PostMeterReadData";
        public const string PostMeterChangeData = "PostMeterChangeData";
        public const string GetMeterReadData = "GetMeterReadData";
        public const string PostWarehouseData = "PostWarehouseData";
        public const string GetMeterReadDataRemote = "GetMeterReadDataRemote";
        public const string PostMeterReadDataRemote = "PostMeterReadDataRemote";         
    }
}
