using System;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;

public struct sFunctionResult
{
    public Boolean resBool;
    public Int32 resInteger;
    public String resInfo;
    public String resError;
    public String rescSQL;
}


public class cFunctionResult
{
    public static sFunctionResult Init()
    {
      sFunctionResult _resFuncRes;
      _resFuncRes.resBool     = false;
      _resFuncRes.resInteger  = 0;
      _resFuncRes.resInfo     = "";
      _resFuncRes.resError    = "";
      _resFuncRes.rescSQL      = "";
      return _resFuncRes;
    }

    public static sFunctionResult Init(Boolean _resBool)
    {
      sFunctionResult _resFuncRes;
      _resFuncRes.resBool    = _resBool;
      _resFuncRes.resInteger = 0;
      _resFuncRes.resInfo    = "";
      _resFuncRes.resError   = "";
      _resFuncRes.rescSQL     = "";
      return _resFuncRes;
    }

    public static sFunctionResult Set(Boolean _bool, Int32 _int, String _info, String _Err, String _cSQL)
    {
      sFunctionResult _resFuncRes;
      _resFuncRes.resBool     = _bool;
      _resFuncRes.resInteger  = _int;
      _resFuncRes.resInfo     = _info;
      _resFuncRes.resError    = _Err;
      _resFuncRes.rescSQL      = _cSQL;
      return _resFuncRes;    
    }
}


public class cCommon
{
    public static String CR()
    {
      return String.Concat(Convert.ToChar(10), Convert.ToChar(13));
    }  
      
    public static String Get_ApplicationRootFolder()
    {
        String RootFolder = AppDomain.CurrentDomain.BaseDirectory;
        if (RootFolder[RootFolder.Length -1] != '/')
        {
            RootFolder = RootFolder + "/";
        }
        return RootFolder;
    }

    public static String Encode_Text(String _InText)
    {  
      char[] charArray = _InText.ToCharArray();
      for (int i = 0; i < _InText.Length; i++)
      {
        charArray[i] = Convert.ToChar(Convert.ToInt32(charArray[i]) +  ( i % 3 + 1));
      }
      return new string(charArray);
    }

    public static String Decode_Text(String _InText)
    {
      char[] charArray = _InText.ToCharArray();
      for (int i = 0; i < _InText.Length; i++)
      {
        charArray[i] = Convert.ToChar(Convert.ToInt32(charArray[i]) - (i % 3 + 1));
      }
      return new string(charArray);
    }

    public static Boolean Log_SaveToFile(String _NewLogLine, String _FileName)
    {
        
        
        _NewLogLine = "ts=" + System.DateTime.Now.ToString() + ";" + _NewLogLine;

        using (System.IO.StreamWriter file = new System.IO.StreamWriter("C://KomunikatorEG//KomunikatorEG//" + _FileName, true))
        {
            file.WriteLine(_NewLogLine);
        }
        return true;
    }



    public static String GetCSVToken(ref String Txt, Char CSVDelimeter, Char QuotationMark, ref Boolean isTokenDelimeted)
    {
      String Token = "";
      Boolean isString = true;
      isTokenDelimeted = false;

      if (Txt != "")
      {
        if (Txt[0] == QuotationMark)
        {
          isString = true;
          Txt = Txt.Remove(0, 1);
          while ((Txt != "") && ( !(Txt.StartsWith(CSVDelimeter.ToString())) | isString))
          {
            if (Txt[0] == QuotationMark) 
            {
              if (! isString)
              {
                Token = Token + Txt[0];
              }
              isString = ! isString;
            }
            else
            {
              Token = Token + Txt[0];
            }
            Txt = Txt.Remove(0, 1);
          }
          if (Txt != "") 
          {
            Txt = Txt.Remove(0, 1);;
            isTokenDelimeted = true;
          }
        }
        else 
        {
          while ((Txt != "") && !(Txt.StartsWith(CSVDelimeter.ToString())))
          {
            Token = Token + Txt[0];
            Txt = Txt.Remove(0, 1);
          }
          if (Txt != "")
          {
            Txt = Txt.Remove(0, 1);;
            isTokenDelimeted = true;
          }
        }
      }  
      return Token;
    }      // method GetCSVToken

    public static String GetCSVToken(ref String Txt, Char CSVDelimeter, Char QuotationMark)
    {
        Boolean isTokenDelimeted = false;
        return GetCSVToken(ref Txt, CSVDelimeter, QuotationMark, ref isTokenDelimeted);
    }

    public static String GetCSVToken(ref String Txt, Char CSVDelimeter)
    {
        return GetCSVToken(ref Txt, CSVDelimeter, Convert.ToChar(34));
    }

    public static String GetCSVToken(ref String Txt)
    {
        return GetCSVToken(ref Txt, ';', Convert.ToChar(34));
    }

    public static Int32 CountCSVTokens(string _InTxt, Char _CSVDelimeter, char QuotationMark)
    {
      Boolean isTokenDelimeted = true;
      String  t;
      String _tmpTxt = _InTxt;
      Int32   _ResInt = 1;
            while (isTokenDelimeted==true)
      {
        t = GetCSVToken (ref _tmpTxt, _CSVDelimeter, QuotationMark, ref isTokenDelimeted);
        _ResInt = _ResInt + 1;        
      }
      return _ResInt;       
    }

    public static Int32 CountCSVTokens (string _InTxt)
    {
      return CountCSVTokens(_InTxt, ';', '"');
    }

    public static Int32 CountCSVTokens(string _InTxt, Char _CSVDelimeter)
    {
      return CountCSVTokens (_InTxt, _CSVDelimeter, '"');
    }

    public static System.Collections.ArrayList GetCSVToArrayList(String _CSVStr, char _Delimeter)
    {
      System.Collections.ArrayList _al = new System.Collections.ArrayList();
      String                       _CSVObj;
      while (_CSVStr != "")
      {
        _CSVObj = GetCSVToken( ref _CSVStr, ';' );
        _CSVObj = DeQuoteCSV( _CSVObj );
        _al.Add( _CSVObj );
      } 
      return _al;
    }

    public static System.Collections.ArrayList GetCSVToArrayList(String _CSVStr, String _Delimeter)
    {
        System.Collections.ArrayList _al = new System.Collections.ArrayList();

        string[] stringSeparators = new string[] {_Delimeter};
        String[] splited_CSVObj = _CSVStr.Split(stringSeparators,StringSplitOptions.None);

        foreach (String s in splited_CSVObj)
        {
            _al.Add(s);
        }

        return _al;
    }

    public static String GetStringTokenTab (ref String _Txt)
    {
      String _ResStr = "";
      while ((_Txt != "") && (_Txt[0] == Convert.ToChar(9)))
      {
        _Txt = _Txt.Remove(0, 1);
      }
      while ((_Txt != "") && (_Txt[0] != Convert.ToChar(9)))
      {
        _ResStr = _ResStr + _Txt[0];
        _Txt    = _Txt.Remove(0, 1);
      }
      while ((_Txt != "") && (_Txt[0] == Convert.ToChar(9)))
      {
        _Txt = _Txt.Remove(0, 1);
      }      
      return _ResStr;
    }

    public static String GetStringTokenByLen (ref String _Txt, Int32 _Length)
    {
      String _ResStr = "";
      _ResStr = _Txt.Substring(0, _Length);
      _Txt    = _Txt.Remove(0, _Length);
      return _ResStr;
    }

    public static String GetStringTokenByStringDelimeter (ref String _Txt,  String _StringDelimeter)
    {
      String _ResStr = "";
      Int32  _p;
      _p = _Txt.IndexOf(_StringDelimeter);
      if (_p > -1)
      {
        _ResStr = _Txt.Substring(0, _p);
        _Txt    = _Txt.Remove(0, _p + _StringDelimeter.Length);
      }
      else 
      {
        _ResStr = _Txt;
        _Txt    = "";
      }
      return _ResStr;
    }

    public static String EnQuoteCSV (String _InStr)
    {
      Int32   i;
      String _ResStr = "";
      if (_InStr.Length == 0)
      {
        return "";
      }
      for (i = 0; (i <= (_InStr.Length -1)); i++)
      {
        if (_InStr[i] == '"')
          _ResStr = _ResStr + Convert.ToChar(34).ToString() + Convert.ToChar(34).ToString();
        else
          _ResStr = _ResStr + _InStr[i];
      }
      _ResStr = Convert.ToChar(34).ToString() + _ResStr + Convert.ToChar(34).ToString();
      return _ResStr;
    }

    public static String DeQuoteCSV(String _InStr)
    {
      sFunctionResult _FuncRes = cFunctionResult.Init();
      return DeQuoteCSV(_InStr, ref _FuncRes);
    }
    
    public static String DeQuoteCSV(String _InStr, ref sFunctionResult _FuncRes)
    {
      Int32 i;
      String _ResStr = "";
      _FuncRes = cFunctionResult.Init();
      if (_InStr == "")
      {
        _FuncRes = cFunctionResult.Set(true, 0, "Funkcija uspešno izvedena", "", "");
        return "";
      }
      if (_InStr[0] == '"')
      {
        if (_InStr[_InStr.Length -1] != '"')
        {
          _FuncRes = cFunctionResult.Set(false, 1, "CSV String se ne zaključi z dvojnim narekovajem (" + Convert.ToChar(34).ToString() + ") ! " + _InStr, "", "");
          return "";
        }
        else
        {
          i = 1;
          while (i < (_InStr.Length - 1))
          {
            if (_InStr[i] == '"')
            {
              if (i+1 < (_InStr.Length -1)) 
              {
                if (_InStr[i+1] == '"')
                {
                  _ResStr = _ResStr + '"';
                  i = i + 1;
                }
                else 
                {
                  _FuncRes = cFunctionResult.Set(false, 2, "CSV String nima pravilno postavljenih dvojnih narekovajev (" + Convert.ToChar(34).ToString() + ") ! " + _InStr, "", "");
                  return "";
                }
              }
              else 
              {
                _FuncRes = cFunctionResult.Set(false, 3, "CSV String nima pravilno postavljenih dvojnih narekovajev (" + Convert.ToChar(34).ToString() + ") ! " + _InStr, "", "");
                return "";
              }
            }
            else
            {
              _ResStr = _ResStr + _InStr[i];
            }
            i = i + 1;
          }
        }
      }
      else
      {
        _ResStr = _InStr;
      }
      _FuncRes = cFunctionResult.Set(true, 0, "Funkcija uspešno izvedena", "", "");
      return _ResStr;
    }
  
    public static String Str_Copy(string s, int _FromIndex, int _Length)
    {
      String _resStr;
      if (_FromIndex > s.Length - 1)
        _resStr = "";
      else if ((_FromIndex + _Length) > s.Length - 1)
        _resStr = s.Substring(_FromIndex, s.Length - 1);
      else
        _resStr = s.Substring(_FromIndex, _Length);
      return _resStr;
    }

    public static String Add_LeadingChars(string _Txt, int _Len, char _ch)
    {
      String _resStr;
      _resStr = _Txt;
      while (_resStr.Length < _Len)
        _resStr = _ch.ToString() + _resStr;
      return _resStr;      
    }

    public static String Add_LeadingSpaces(string _Txt, int _Len)
    {
      return Add_LeadingChars(_Txt, _Len, ' ');
    }

    public static String Add_LeadingZeros(string _Txt, int _Len)
    {
      return Add_LeadingChars(_Txt, _Len, '0');
    }

    public static String Add_TrailingChars(string _Txt, int _Len, char _ch)
    {
      String _resStr;
      _resStr = _Txt;
      while ((_resStr.Length - 1) < _Len)
        _resStr = _resStr + _ch.ToString();
      return _resStr;
    }

    public static String Add_TrailingSpaces(string _Txt, int _Len)
    {
      return Add_TrailingChars(_Txt, _Len, ' ');
    }

    public static String Add_TrailingZeros(string _Txt, int _Len)
    {
      return Add_TrailingChars(_Txt, _Len, '0');
    }

    public static String Remove_LeadingChars(string _Txt, char _ch)
    {
      Boolean Konec = false;
      while ((_Txt != "") && (!Konec))
      {
        if (_Txt[0] == _ch)
          _Txt = _Txt.Remove(0, 1);
        if (_Txt != "")
        {
          Konec  = (_Txt[0] != _ch);
        }
      }
      return _Txt;
    }

    public static String Remove_LeadingSpaces(string _Txt)
    {
      return Remove_LeadingChars(_Txt, ' ');
    }

    public static String Remove_LeadingZeros(string _Txt)
    {
      return Remove_LeadingChars(_Txt, '0');
    }

    public static String Remove_Empty( String _Str)
    {
        return _Str.Replace("&nbsp;", ""); ;
    }

    public static String Limit_String(String _Str, int _startNr, int _endNr, String _BetweenText)
    {
        String startStr;
        String endStr;
    
        // če je Text krajši od omejitev potem vrni cel tekst
        if (_Str.Length < _startNr + _endNr) 
            return _Str;
      
        // določi začetni del stringa
        startStr = _Str.Substring(0, _startNr);
        // definiraj ostanek stringa
        _Str = _Str.Substring(_startNr-1, (int)Math.Max( 0, _Str.Length-_startNr) );
  
        // določi konec stringa
        endStr   = _Str.Substring( Math.Max((_Str.Length-1)-_endNr, 0), _endNr );
        return startStr + _BetweenText + endStr;
    }

    public static Boolean IsNumber(string _Str)
    {
        Boolean _resBool = false;
        if (_Str == null)
        {
            _resBool = false;
            return _resBool;
        }
        if (_Str.Trim() == "")
        {
            _resBool = false;
            return _resBool;
        }
        _resBool = true;
        try
        {
            Convert.ToDouble(_Str);
        }
        catch
        {
            _resBool = false;
        }
        return _resBool;
    }

    public static Boolean IsInteger(string _Str)
    {
        Boolean _resBool = false;
        if (_Str == null)
        {
            _resBool = false;
            return _resBool;
        }
        if (_Str.Trim() == "")
        {
            _resBool = false;
            return _resBool;
        }
        _resBool = true;
        try
        {
            Convert.ToInt64(_Str);
        }
        catch
        {
            _resBool = false;
        }
        return _resBool;
    }

    public static Boolean IsDate(string _Str)
    {
        Boolean _resBool = false;
        if (_Str == null)
        {
            _resBool = false;
            return _resBool;
        }
        if (_Str.Trim() == "")
        {
            _resBool = false;
            return _resBool;
        }
        _resBool = true;
        try
        {
            Convert.ToDateTime(_Str);
        }
        catch
        {
            _resBool = false;
        }
        return _resBool;
    }

    public static Boolean IsTime(string _Str)
    {
        return IsDate(_Str);
    }

    public static Boolean ToDate(string _StrDate, out DateTime _Date)
    {
        _Date = Convert.ToDateTime("01.01.1900");
        if (_StrDate == null)
        {
            return false;
        }
        if (_StrDate.Trim() == "")
        {
            return false;
        }
        _StrDate = _StrDate.Trim();
        if (_StrDate.IndexOf(' ') > 0)
        {
            _StrDate = _StrDate.Substring(0, _StrDate.IndexOf(' '));
        }
        
        String DateFormat = "dd.MM.yyyy";
        // yyyy-mm-dd
        if ((_StrDate[4] == '-') && (_StrDate[7] == '-'))
        {
            DateFormat = "yyyy-MM-dd";
        }
        // yyyymmdd
        else if ((_StrDate.Length == 8) && (_StrDate.Substring(0, 2) == "20"))
        {
            DateFormat = "yyyyMMdd";
        }
        if (DateFormat == "dd.MM.yyyy")
        {
            if (_StrDate[2] != '.')
            {
                _StrDate = _StrDate.Insert(0, "0");
            }
            if (_StrDate[5] != '.')
            {
                _StrDate = _StrDate.Insert(3, "0");
            }
        }
        if (DateTime.TryParseExact(_StrDate, DateFormat,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out _Date))
        {

        }
        else
        {
            return false;
        }
        return true;
    }

    public static Boolean ToDateNull(string _StrDate, out DateTime? _Date)
    {
        DateTime TestDate;
        _Date = null;
        if (_StrDate == null)
        {
            return false;
        }
        if (_StrDate.Trim() == "")
        {
            return false;
        }
        _StrDate = _StrDate.Trim();
        if (_StrDate.IndexOf(' ') > 0)
        {
            _StrDate = _StrDate.Substring(0, _StrDate.IndexOf(' '));
        }

        String DateFormat = "dd.MM.yyyy";
        // yyyy-mm-dd
        if ((_StrDate[4] == '-') && (_StrDate[7] == '-'))
        {
            DateFormat = "yyyy-MM-dd";
        }
        // yyyymmdd
        else if ((_StrDate.Length == 8) && (_StrDate.Substring(0, 2) == "20"))
        {
            DateFormat = "yyyyMMdd";
        }
        if (DateFormat == "dd.MM.yyyy")
        {
            if (_StrDate[2] != '.')
            {
                _StrDate = _StrDate.Insert(0, "0");
            }
            if (_StrDate[5] != '.')
            {
                _StrDate = _StrDate.Insert(3, "0");
            }
        }
        if (DateTime.TryParseExact(_StrDate, DateFormat,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out TestDate))
        {
            _Date = TestDate;
        }
        else
        {
            return false;
        }
        return true;
    }

    public static Int32 Get_MonthInDate(DateTime _InDate)
    {
      Int32 _resInt = 0;
      try
      {
        _resInt = Convert.ToInt32(_InDate.ToString("MM"));
      }
      catch
      {
        _resInt = 0;
      }
      return _resInt;
    }

    public static Int32 Get_DayInDate(DateTime _InDate)
    {
      Int32 _resInt = 0;
      try
      {
        _resInt = Convert.ToInt32(_InDate.ToString("dd"));
      }
      catch
      {
        _resInt = 0;
      }
      return _resInt;
    }

    public static Int32 Get_YearInDate(DateTime _InDate)
    {
      Int32 _resInt = 0;
      try
      {
        _resInt = Convert.ToInt32(_InDate.ToString("yyyy"));
      }
      catch
      {
        _resInt = 0;
      }
      return _resInt;
    }

    public static DateTime Get_FirstDayInMonth(DateTime _InDate)
    {
      int _Month, _Year; 
      try
      {
        _Month = Get_MonthInDate(_InDate);
        _Year  = Get_YearInDate(_InDate);
        return Convert.ToDateTime("01." + _Month.ToString() + "." + _Year.ToString()).Date;
      }
      catch
      {
        return DateTime.Now.Date;
      }
    }

    public static DateTime Get_LastDayInMonth(DateTime _InDate)
    {
      int _Day, _Month, _Year; 
      try
      {
        _Month = Get_MonthInDate(_InDate);
        _Year  = Get_YearInDate(_InDate);
        _Day   = DateTime.DaysInMonth(_Year, _Month);
        return Convert.ToDateTime(_Day.ToString() + "." + _Month.ToString() + "." + _Year.ToString()).Date;
      }
      catch
      {
        return DateTime.Now.Date;
      }
    }

    public static Int32 Get_WeekOfYear(DateTime _InDate)
    {
        System.Globalization.CultureInfo myCI = new System.Globalization.CultureInfo("no");//("sl-SI");
        System.Globalization.Calendar          myCal = myCI.Calendar; 
        System.Globalization.CalendarWeekRule  myCWR = myCI.DateTimeFormat.CalendarWeekRule;
        System.DayOfWeek                       myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
        return myCal.GetWeekOfYear(_InDate, myCWR, myFirstDOW);
    }

    public static Int32 Get_DayOfWeekInt(System.DayOfWeek _InDayOfWeek)
    {
      Int32 _resInt = 0;
      if (_InDayOfWeek == System.DayOfWeek.Monday) _resInt = 1;
      else if (_InDayOfWeek == System.DayOfWeek.Tuesday) _resInt = 2;
      else if (_InDayOfWeek == System.DayOfWeek.Wednesday) _resInt = 3;
      else if (_InDayOfWeek == System.DayOfWeek.Thursday) _resInt = 4;
      else if (_InDayOfWeek == System.DayOfWeek.Friday) _resInt = 5;
      else if (_InDayOfWeek == System.DayOfWeek.Saturday) _resInt = 6;
      else if (_InDayOfWeek == System.DayOfWeek.Sunday) _resInt = 7;
      return _resInt;
    }

    public static Int32 Get_DayOfWeekInt(DateTime _InDate)
    {
        Int32 _resInt = 0;
        if (_InDate.DayOfWeek == System.DayOfWeek.Monday) _resInt = 1;
        else if (_InDate.DayOfWeek == System.DayOfWeek.Tuesday) _resInt = 2;
        else if (_InDate.DayOfWeek == System.DayOfWeek.Wednesday) _resInt = 3;
        else if (_InDate.DayOfWeek == System.DayOfWeek.Thursday) _resInt = 4;
        else if (_InDate.DayOfWeek == System.DayOfWeek.Friday) _resInt = 5;
        else if (_InDate.DayOfWeek == System.DayOfWeek.Saturday) _resInt = 6;
        else if (_InDate.DayOfWeek == System.DayOfWeek.Sunday) _resInt = 7;
        return _resInt;
    }

    public static System.DayOfWeek Get_DayOfWeek(Int32 _IntDay)
    {
      DayOfWeek _DayOrWeek;
      if (_IntDay==1) _DayOrWeek = System.DayOfWeek.Monday;
      else if (_IntDay==2) _DayOrWeek = System.DayOfWeek.Tuesday; 
      else if (_IntDay==3) _DayOrWeek = System.DayOfWeek.Wednesday; 
      else if (_IntDay==4) _DayOrWeek = System.DayOfWeek.Thursday; 
      else if (_IntDay==5) _DayOrWeek = System.DayOfWeek.Friday; 
      else if (_IntDay==6) _DayOrWeek = System.DayOfWeek.Saturday; 
      else if (_IntDay==7) _DayOrWeek = System.DayOfWeek.Sunday;
      else _DayOrWeek = System.DayOfWeek.Monday;
      return _DayOrWeek;
    }

    public static DateTime Get_DayDateFromDate(DateTime _InDate, System.DayOfWeek _DayOfWeek)
    {
        Int32 SubtractDays;
        if (_InDate.DayOfWeek == _DayOfWeek)
        {
            return _InDate;
        }
        else
        {

            SubtractDays = cCommon.Get_DayOfWeekInt(_InDate.DayOfWeek) - cCommon.Get_DayOfWeekInt(_DayOfWeek);
            if (SubtractDays < 0)
            {
                SubtractDays = 7 + SubtractDays;
            }
            return _InDate.AddDays(-SubtractDays);
        }
    }

    public static DateTime Get_MondayDateFromDate(DateTime _InDate)
    {
        return Get_DayDateFromDate(_InDate, System.DayOfWeek.Monday);
    }

    public static String Convert_EnterToNL(String _InStr)
    {
      String _ResStr;
      _ResStr = _InStr.Replace(Convert.ToChar(13).ToString(), "\n");
      _ResStr = _ResStr.Replace(Convert.ToChar(10).ToString(), "\n");
      return _ResStr;
    }

    public static void SetCSVList( ref String _list, String _newItem, Boolean _Insert)
    {
      String _tmpList;
      String _Token;
      _tmpList = _list;
      _list = "";
      while (_tmpList != "")
      {
        _Token = GetCSVToken(ref _tmpList);
        if (_newItem != _Token)
        {
          if (_list != "") 
          {
            _list = _list + ";";
          }
          _list = _list + EnQuoteCSV( _Token );
        }
      }
      if (_Insert)
      {
        if (_list != "") 
        {
          _list = _list + ";";
        }
        _list = _list + EnQuoteCSV(_newItem);
      }
    }
      
    public static string Color_ToHexStr(Color _col )
    {
      return "#" + _col.R.ToString("X2") + _col.G.ToString("X2") + _col.B.ToString("X2") ;
    }

    public static String Get_RandomID()
    {
        Random randID = new Random();
        int randInt = randID.Next(2147483640);

        return randInt.ToString();
    }

    public static string GridView_ItemStyle(String _BackColor, String _Cursor, Boolean _DoPainting)
    {
        if (_BackColor == "")
            _BackColor = "'White'";
        if (_Cursor == "")
            _Cursor = "'pointer'";
        if (_DoPainting)
            return "this.style.backgroundColor='" + _BackColor + "';this.style.cursor='" + _Cursor + "';";
        else
            return "this.style.cursor='" + _Cursor + "';";
    }

    public static string GridView_ItemStyle(int _BackColor, String _Cursor, Boolean _DoPainting)
    {
        String strBackColor = "'White'";
        if (_BackColor > 0)
            strBackColor = Color_ToHexStr(Color.FromArgb(_BackColor));
        return GridView_ItemStyle(strBackColor, _Cursor, _DoPainting);
    }

    public static string GridView_MouseOutItemStyle(String _BackColor)
    {
      if (_BackColor == "")
        _BackColor = "White";
      return "this.style.backgroundColor='" + _BackColor + "';";
    }

    public static string GridView_MouseOutItemStyle(int _BackColor)
    {
      String strBackColor = "'White'";
      if (_BackColor > 0)
        strBackColor = Color_ToHexStr(Color.FromArgb(_BackColor));
      return GridView_MouseOutItemStyle(strBackColor);
    }

    public static string GridView_MouseOutItemStyleCSS(String _cssClassName)
    {
      return "this.className ='" + _cssClassName + "';";
    }

    public static string GridView_MouseOverItemStyle(String _BackColor, String _Cursor, Boolean _DoPainting)
    {
      if (_BackColor == "")
        _BackColor = "'AliceBlue'";
      if (_Cursor == "")
        _Cursor = "'hand'";
      if (_DoPainting)
        return "this.style.backgroundColor='" + _BackColor + "';this.style.cursor='" + _Cursor + "';";
      else
        return "this.style.cursor='" + _Cursor + "';";
    }

    public static string GridView_MouseOverItemStyle(int _BackColor, String _Cursor, Boolean _DoPainting)
    {
      String strBackColor = "'AliceBlue'";
      if (_BackColor > 0)
        strBackColor = Color_ToHexStr(Color.FromArgb(_BackColor));
      return GridView_MouseOverItemStyle(strBackColor, _Cursor, _DoPainting);
    }

    public static string GridView_MouseOverItemStyleCSS(String _cssClassName)
    {
      return "this.className ='" + _cssClassName + "';";
    }

        public static String Get_WorkDaysDatesBetweenDates(DateTime _DateFrom, DateTime _DateTo)
    {
        String Dates = "";

        DateTime Date = _DateFrom;
        while (Date <= _DateTo)
        {
            if (Date.DayOfWeek != DayOfWeek.Saturday && Date.DayOfWeek != DayOfWeek.Sunday) //prvi obhod zanke while -> _DateFrom je lahko vikend
            {
                if (Dates.Equals(""))
                    Dates = "'" + Date.Year + "-" + Date.Month + "-" + Date.Day + "'";
                else
                    Dates = Dates + ", '" + Date.Year + "-" + Date.Month + "-" + Date.Day + "'";
            }

            //Get next work/week day
            Date = Date.AddDays(1);
            if (Date.DayOfWeek == DayOfWeek.Saturday)
                Date = Date.AddDays(2);
            else if (Date.DayOfWeek == DayOfWeek.Sunday)
                Date = Date.AddDays(1);
        }

        return Dates;
    }

    public static String Get_WeekendDaysDatesBetweenDates(DateTime _DateFrom, DateTime _DateTo)
    {
        String Dates = "";

        DateTime Date = _DateFrom;
        while (Date <= _DateTo)
        {
            if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday) //prvi obhod zanke while -> _DateFrom je lahko vikend
            {
                if (Dates.Equals(""))
                    Dates = "'" + Date.Year + "-" + Date.Month + "-" + Date.Day + "'";
                else
                    Dates = Dates + ", '" + Date.Year + "-" + Date.Month + "-" + Date.Day + "'";
            }

            //Get next work/week day
            Date = Date.AddDays(1);
            if (Date.DayOfWeek == DayOfWeek.Monday)
                Date = Date.AddDays(5);
            else if (Date.DayOfWeek == DayOfWeek.Tuesday)
                Date = Date.AddDays(4);
            else if (Date.DayOfWeek == DayOfWeek.Wednesday)
                Date = Date.AddDays(3);
            else if (Date.DayOfWeek == DayOfWeek.Thursday)
                Date = Date.AddDays(2);
            else if (Date.DayOfWeek == DayOfWeek.Friday)
                Date = Date.AddDays(1);
        }

        return Dates;
    }

    public static String Get_SundayDaysDatesBetweenDates(DateTime _DateFrom, DateTime _DateTo)
    {
        String Dates = "";

        DateTime Date = _DateFrom;
        while (Date <= _DateTo)
        {
            if (Date.DayOfWeek == DayOfWeek.Sunday) //prvi obhod zanke while -> _DateFrom je lahko vikend
            {
                if (Dates.Equals(""))
                    Dates = "'" + Date.Year + "-" + Date.Month + "-" + Date.Day + "'";
                else
                    Dates = Dates + ", '" + Date.Year + "-" + Date.Month + "-" + Date.Day + "'";
            }

            //Get next work/week day
            Date = Date.AddDays(1);
            if (Date.DayOfWeek == DayOfWeek.Monday)
                Date = Date.AddDays(6);
            else if (Date.DayOfWeek == DayOfWeek.Tuesday)
                Date = Date.AddDays(5);
            else if (Date.DayOfWeek == DayOfWeek.Wednesday)
                Date = Date.AddDays(4);
            else if (Date.DayOfWeek == DayOfWeek.Thursday)
                Date = Date.AddDays(3);
            else if (Date.DayOfWeek == DayOfWeek.Friday)
                Date = Date.AddDays(2);
            else if (Date.DayOfWeek == DayOfWeek.Saturday)
                Date = Date.AddDays(1);
        }

        return Dates;
    }
}
