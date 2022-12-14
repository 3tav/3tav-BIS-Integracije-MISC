using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PripravljalecPrognozLib.PPServiceWCFClient
{
    public interface IAddOfftakePointMeasurement
    {
        ExtensionDataObject ExtensionData { get; set; }
        decimal? NewNm3ConversionFactor { get; set; }
        int? NewReadingValue { get; set; }
        decimal Nm3ConversionFactor { get; set; }
        DateTime ReadingTime { get; set; }
        int ReadingValue { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}