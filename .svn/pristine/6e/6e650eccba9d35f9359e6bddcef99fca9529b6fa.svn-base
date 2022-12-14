using System;
using System.Web;
using System.Web.UI;
using System.Runtime.Serialization;
using System.Collections.Generic;

public enum efrErrorCodes
{
    OK = 0,

    NoDataFound = 100001,
    MissingInputParameter = 100002,
    IncorrectFormatInputParameter = 100003,
    MP_InvalidOrNotActive = 100004,

    UnknownError = 110000,
    SQLError = 110001,
    NotAuthorized = 110002,
    WebServiceError = 110003
}

[DataContract]
public class dcResult
{
    [DataMember]
    public Int32 RTC { get; set; }
    [DataMember]
    public String CONTROL { get; set; }
    [DataMember]
    public String MSG { get; set; }
}

[DataContract]
public class dcResultDobiDokumente
{
    [DataMember]
    public Int32 RTC { get; set; }
    [DataMember]
    public String CONTROL { get; set; }
    [DataMember]
    public String MSG { get; set; }
    [DataMember]
    public List<dcResultZZIDokumentData> Dokument { get; set; }
}
[DataContract]
public class dcResultZZIDokumentData
{
    [DataMember]
    public int Id;
    [DataMember]
    public string Extern_id;
    [DataMember]
    public string Title;
    [DataMember]
    public string CreationTime;
    [DataMember]
    public string Creation_location;
    [DataMember]
    public string Filename;
    [DataMember]
    public string MineType;
    [DataMember]
    public string Organization;
    [DataMember]
    public string Insert_date;
    [DataMember]
    public string Classification_name;
    [DataMember]
    public string Account_name;
    [DataMember]
    public string Usr_id;
    [DataMember]
    public string Status;
    [DataMember]
    public string Usr_id_uporabnik;
    [DataMember]
    public int Size;
    [DataMember]
    public string Type;
    [DataMember]
    public int Acc_id;
    [DataMember]
    public string Ver_description;
    [DataMember]
    public int Doc_id;
    [DataMember]
    public string Data_reference;
}


public class cdcResult
{
    public static dcResult Set(Int32 _RTC, String _CONTROL, String _MSG)
    {
        dcResult _Result = new dcResult();
        _Result.RTC = _RTC;
        _Result.CONTROL = _CONTROL;
        _Result.MSG = _MSG;
        return _Result;
    }

    public static dcResult Get_FromFunctionResult(sFunctionResult _fr)
    {
        dcResult wsResult = new dcResult();
        if (!_fr.resBool)
        {
            //wsResult = Set((int)efrErrorCodes.SQLError, _fr.resInfo, _fr.resError);
            wsResult = Set(_fr.resInteger, _fr.resInfo, _fr.resError);
        }
        else if (_fr.resInteger == 0)
        {
            wsResult = Set(0, _fr.resInfo, _fr.resError);
        }
        else
        {
            if (Enum.IsDefined(typeof(efrErrorCodes), _fr.resInteger))
            {
                wsResult = Set(_fr.resInteger, _fr.resInfo, _fr.resError);
            }
            else if (_fr.resInteger < (Int32)efrErrorCodes.NoDataFound)
            {
                wsResult = Set(_fr.resInteger, _fr.resInfo, _fr.resError);
            }
            else
            {
                String Control = "Neznana kontrola";
                String Msg = "Neznana napaka";
                if ((_fr.resInfo != null) && (_fr.resInfo != ""))
                {
                    Control = _fr.resInfo;
                }
                if ((_fr.resError != null) && (_fr.resError != ""))
                {
                    Msg = _fr.resError;
                }
                wsResult = Set((int)efrErrorCodes.UnknownError, Control, Msg);
            }
        }
        return wsResult;
    }
}