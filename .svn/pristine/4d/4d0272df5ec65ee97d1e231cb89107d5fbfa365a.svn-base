using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Ini
{
  /// <summary>
  /// Create a New INI file to store or load data
  /// </summary>
  public class IniFile
  {
    public string path;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
             string key, string def, StringBuilder retVal,
        int size, string filePath);

    /// <summary>
    /// INIFile Constructor.
    /// </summary>
    /// <PARAM name="INIPath"></PARAM>
    public IniFile(string INIPath)
    {
      path = INIPath;
    }
    /// <summary>
    /// Write Data to the INI File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// Section name
    /// <PARAM name="Key"></PARAM>
    /// Key Name
    /// <PARAM name="Value"></PARAM>
    /// Value Name
    public void IniWriteValue(string Section, string Key, string Value)
    {
      WritePrivateProfileString(Section, Key, Value, this.path);
    }

    /// <summary>
    /// Read Data Value From the Ini File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// <PARAM name="Key"></PARAM>
    /// <PARAM name="Path"></PARAM>
    /// <returns></returns>
    public string IniReadValue(string Section, string Key)
    {
      StringBuilder temp = new StringBuilder(255);
      int i = GetPrivateProfileString(Section, Key, "", temp,
                                      255, this.path);
      return temp.ToString();

    }

    public void WriteInteger(string Section, string Key, int Value)
    {
      WritePrivateProfileString(Section, Key, Convert.ToString(Value), this.path);
    }


    public Int32 ReadInteger(string Section, string Key, int defValue)
    {
      StringBuilder temp = new StringBuilder(255);
      int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
      string x = (temp.ToString());
      if (cCommon.IsInteger(x) == true)
      {
        return (Convert.ToInt32(x));
      }
      else
      {
        return defValue;
      }
    }

    public void WriteBool(string Section, string Key, bool Value)
    {
      WritePrivateProfileString(Section, Key, Convert.ToString(Value), this.path);
    }

    public bool ReadBool(string Section, string Key, bool defValue)
    {
      StringBuilder temp = new StringBuilder(255);
      int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
      string x = (temp.ToString());
      if (x.Trim() == "1")
        return true;
      else if (x.Trim() == "0")
        return false;
      else
        return defValue;
    }

    public void WriteDouble(string Section, string Key, double Value)
    {
      string podatek = Value.ToString();
      podatek = podatek.Replace(',', '.');
      WritePrivateProfileString(Section, Key, podatek, this.path);
    }

    public Double ReadDouble(string Section, string Key, double defValue)
    {
      StringBuilder temp = new StringBuilder(255);
      int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
      string x = (temp.ToString());
      if (cCommon.IsNumber(x) == true)
      {
        return Convert.ToDouble(x);
      }
      else
      {
        return defValue;
      }
    }

    public void WriteString(string Section, string Key, string Value)
    {
      WritePrivateProfileString(Section, Key, Value, this.path);
    }

    public string ReadString(string Section, string Key, string defValue)
    {
      StringBuilder temp = new StringBuilder(255);
      int i = GetPrivateProfileString(Section, Key, defValue, temp, 255, this.path);
      return temp.ToString();
    }

    public bool SectionExist(string section)
    {
      using (StreamReader sr = File.OpenText(path))
      {
        string s = "";
        while ((s = sr.ReadLine()) != null)
        {
          if (s.StartsWith(Convert.ToString('[' + section + ']')) == true)
          {
            return true;
          }
        }
        return false;
      }
    }


    public bool KeyExist(string section, string key)
    {
      using (StreamReader sr = File.OpenText(path))
      {
        string c = "";
        while ((c = sr.ReadLine()) != null)
        {
          if (c.StartsWith(Convert.ToString('[' + section + ']')) == true)
          {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
              if (s.StartsWith(Convert.ToString(key)) == true)
              {
                return true;
              }
            }
          }
          return false;
        }
        return false;
      }
    }
  }
}