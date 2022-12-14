using System;
using System.Data;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using System.Collections.Generic;

public enum cSQLDateTimeType { tfDate, tfTime, tfTimeStamp, tfDateNoTime, tfDateTime };
public enum cSQLDatabaseType { dbIBMDB2, dbUnknown, dbMSAccess, dbcSQLServer, dbOracle };

public class cSQL
{
    public List<SqlParameter> ParametersList;
    public String Text;
    internal SqlConnection dbSqlConn;

    public cSQL(String _SqlConnString)
    {
        this.Text = "";
        this.dbSqlConn = new SqlConnection(_SqlConnString);
        this.ParametersList = new List<SqlParameter>(0);
    }

    public void Conn_Open()
    {
        Boolean _TryReopen = false;
        try
        {
            if (this.dbSqlConn.State != ConnectionState.Open) { this.dbSqlConn.Open(); }
        }
        catch (Exception ex)
        {
            if ((ex.Message.IndexOf("A communication error has been detected") > 0) ||
                (ex.Message.IndexOf("Communication link failure") > 0) ||
                (ex.Message.IndexOf("not connected") > 0) ||
                (ex.Message.IndexOf("Neznana napaka") > 0))
            {
                _TryReopen = true;
            }
            else
            { 
                // rethrow
                throw new Exception(ex.Message);
            }
        }
        if (_TryReopen)
        {
            //this.dbSqlConn.ResetState();
            this.dbSqlConn.Close();
            this.dbSqlConn.Open();
        }
    }
    public void Conn_Close()
    {
        if (this.dbSqlConn.State == ConnectionState.Open) { this.dbSqlConn.Close(); }
    }

    public void ClearParametersList()
    {
        ParametersList = new List<SqlParameter>(0);
    }

    public void AddSQLParameter(String _ParameterName, SqlDbType _SqlDbType, ParameterDirection _ParameterDirection, Object _ParameterValue)
    {
        AddSQLParameter(_ParameterName, _SqlDbType, _ParameterDirection, _ParameterValue, null);
    }

    public void AddSQLParameter(String _ParameterName, SqlDbType _SqlDbType, ParameterDirection _ParameterDirection, Object _ParameterValue, Int32? _ParameterLength)
    {
        SqlParameter _NewParameter = new SqlParameter();

        _NewParameter.ParameterName = _ParameterName;
        _NewParameter.SqlDbType = _SqlDbType;
        _NewParameter.Direction = _ParameterDirection;
        if (_ParameterLength != null)
        {
            _NewParameter.Size = (Int32)_ParameterLength;
        }
        _NewParameter.Value = _ParameterValue;
        ParametersList.Add(_NewParameter);
    }

    public sFunctionResult Query_DataSet(ref DataSet _ds, String _functionName)
    {
        SqlDataAdapter da = new SqlDataAdapter(this.Text, this.dbSqlConn);
        sFunctionResult _Fr = cFunctionResult.Init();
        this.Conn_Open();
        try
        {
            da.Fill(_ds);
            _Fr = cFunctionResult.Set(true, 0, "", "", this.Text);
        }
        catch (Exception Ex)
        {
            _Fr = cFunctionResult.Set(false, 1, "Napaka v funkciji " + _functionName + " !!! ", Ex.Message, this.Text);
            da.Dispose();
            _ds.Dispose();
            this.Conn_Close();
            return _Fr;
        }
        return _Fr;
    }

    public SqlDataReader Query_Read(String _functionName, ref sFunctionResult _Fr)
    {
        SqlCommand cmd = new SqlCommand();
        _Fr = cFunctionResult.Init();

        if ((ParametersList == null) || (ParametersList.Count == 0))
        {
            _Fr = cFunctionResult.Set(false, 5, "Kontrola parametrov.", "Seznam parametrov je prazen", this.Text);
            return null;
        }
        try
        {
            this.Conn_Open();
            cmd.Connection = this.dbSqlConn;
            cmd.CommandText = this.Text;

            for (Int32 i = 0; i < ParametersList.Count; i++)
            {
                cmd.Parameters.Add(ParametersList[i]);
            }
            SqlDataReader dr = cmd.ExecuteReader();
            _Fr = cFunctionResult.Set(true, 0, "Funkcija " + _functionName + " uspešno izvedena.", "", this.Text);
            cmd.Dispose();
            return dr;
        }
        catch (Exception ex)
        {
            _Fr = cFunctionResult.Set(false, 1, "Napaka v funkciji " + _functionName + " !!! ", ex.Message, this.Text);
            return null;
        }
    }

    public SqlDataReader Query_ReadNoParameters(String _functionName, ref sFunctionResult _Fr)
    {
        SqlCommand cmd = new SqlCommand();
        _Fr = cFunctionResult.Init();
        try
        {
            this.Conn_Open();
            cmd.Connection = this.dbSqlConn;
            cmd.CommandText = this.Text;

            SqlDataReader dr = cmd.ExecuteReader();
            _Fr = cFunctionResult.Set(true, 0, "Funkcija " + _functionName + " uspešno izvedena.", "", this.Text);
            cmd.Dispose();
            return dr;
        }
        catch (Exception ex)
        {
            _Fr = cFunctionResult.Set(false, 1, "Napaka v funkciji " + _functionName + " !!! ", ex.Message, this.Text);
            return null;
        }
    }

    public sFunctionResult Query_ExecuteScalar(String _functionName, out Int64? _Identity)
    {
        SqlCommand cmd = new SqlCommand();
        sFunctionResult _Fr = cFunctionResult.Init();
        Object RTC;
        String _IdentityStr;
        _Identity = null;
        if ((ParametersList == null) || (ParametersList.Count == 0))
        {
            _Fr = cFunctionResult.Set(false, 5, "Kontrola parametrov.", "Seznam parametrov je prazen", this.Text);
            return _Fr;
        }
        try
        {
            this.Conn_Open();
            cmd.Connection = this.dbSqlConn;
            this.Text = this.Text + ";select scope_identity()";
            cmd.CommandText = this.Text;
            for (Int32 i = 0; i < ParametersList.Count; i++)
            {
                cmd.Parameters.Add(ParametersList[i]);
            }
            RTC = cmd.ExecuteScalar();
            _Fr = cFunctionResult.Set(true, 0, "Funkcija " + _functionName + " uspešno izvedena.", "", this.Text);
            _IdentityStr = Convert.ToString(RTC);
            if (cCommon.IsInteger(_IdentityStr))
            {
                _Identity = Convert.ToInt64(_IdentityStr);
            }
            cmd.Dispose();
            return _Fr;
        }
        catch (Exception ex)
        {
            _Fr = cFunctionResult.Set(false, 1, "Napaka v funkciji " + _functionName + " !!! ", ex.Message, this.Text);
            return _Fr;
        }
    }

    public sFunctionResult Query_Execute(String _functionName)
    {
        SqlCommand cmd = new SqlCommand();
        sFunctionResult _Fr = cFunctionResult.Init();
        if ((ParametersList == null) || (ParametersList.Count == 0))
        {
            _Fr = cFunctionResult.Set(false, 5, "Kontrola parametrov.", "Seznam parametrov je prazen", this.Text);
            return _Fr;
        }
        try
        {
            this.Conn_Open();
            cmd.Connection = this.dbSqlConn;
            cmd.CommandText = this.Text;
            for (Int32 i = 0; i < ParametersList.Count; i++)
            {
                cmd.Parameters.Add(ParametersList[i]);
            }
            cmd.ExecuteNonQuery();
            _Fr = cFunctionResult.Set(true, 0, "Funkcija " + _functionName + " uspešno izvedena.", "", this.Text);
            cmd.Dispose();
            return _Fr;
        }
        catch (Exception ex)
        {
            _Fr = cFunctionResult.Set(false, 1, "Napaka v funkciji " + _functionName + " !!! ", ex.Message, this.Text);
            return _Fr;
        }
    }

    public String Sysdate()
    {
        String res;
        res = "CURRENT_TIMESTAMP";
        return res;
    }

    private String CnvString(string _Text, bool _Quote)
    {
        if (String.IsNullOrEmpty(_Text))
        {
            return "null";
        }
        else
        {
            _Text = _Text.Replace("`", Convert.ToChar(39).ToString());
            _Text = _Text.Replace(Convert.ToString('"'), Convert.ToChar(39).ToString() + Convert.ToChar(39).ToString());
            if (_Quote)
            {
                _Text = (Convert.ToChar(39).ToString() + _Text + Convert.ToChar(39).ToString());
                return _Text;
            }
            else
            {
                return _Text;
            }
        }
    }

    public String FromXML(string _Text)
    {
        return "cast(" + CnvString(_Text, true) + " as XML)";
    }

    public Boolean GetField_IsNull(ref SqlDataReader _DR, String _FieldName)
    {
        if (_DR.IsDBNull(_DR.GetOrdinal(_FieldName)))
            return true;
        else
            return false;
    }

    public Int32? GetField_IntNull(ref SqlDataReader _DR, String _FieldName)
    {
        String DataTypeName;
        Int32 _Index;
        Int32? _ResInt = null;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResInt;
        DataTypeName = _DR.GetDataTypeName(_Index);
        if (cCommon.IsInteger(_DR.GetValue(_Index).ToString()))
        {
            _ResInt = Convert.ToInt32(_DR.GetValue(_Index).ToString());
        }
        return _ResInt;
    }

    public Int32 GetField_Int(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        Int32 _ResInt = -1;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResInt;
        if (cCommon.IsInteger(_DR.GetValue(_Index).ToString()))
        {
            _ResInt = Convert.ToInt32(_DR.GetValue(_Index).ToString());
        }
        return _ResInt;
    }

    public bool? GetField_BoolNull(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        bool? _ResBool = null;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResBool;
        if (_DR.GetValue(_Index).ToString() == "1")
            return true;
        else
            return false;
    }

    public bool GetField_Bool(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        bool _ResBool = false;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResBool;
        if (_DR.GetValue(_Index).ToString().ToUpper() == "TRUE")
            return true;
        else if (_DR.GetValue(_Index).ToString().ToUpper() == "FALSE")
            return false;
        else if (_DR.GetValue(_Index).ToString() == "1")
            return true;
        else
            return false;
    }

    public Double? GetField_DoubleNull(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        Double? _ResDouble = null;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResDouble;
        if (cCommon.IsNumber(_DR.GetValue(_Index).ToString()))
        {
            _ResDouble = Convert.ToDouble(_DR.GetValue(_Index).ToString());
        }
        return _ResDouble;
    }

    public Double GetField_Double(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        Double _ResDouble = -1;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResDouble;
        if (cCommon.IsNumber(_DR.GetValue(_Index).ToString()))
        {
            _ResDouble = Convert.ToDouble(_DR.GetValue(_Index).ToString());
        }
        return _ResDouble;
    }

    public String GetField_String(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        String _ResStr = "";
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResStr;
        return _DR.GetValue(_Index).ToString();
    }

    public DateTime? GetField_DateTimeNull(ref SqlDataReader _DR, String _FieldName)
    {
        Int32 _Index;
        DateTime? _ResDT = null;
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResDT;
        try
        {
            _ResDT = _DR.GetDateTime(_Index);
        }
        catch { }

        return _ResDT;
    }

    public DateTime GetField_DateTime(ref SqlDataReader _DR, String _FieldName)
    {
        String DataTypeName;
        Int32 _Index;
        DateTime _ResDT = Convert.ToDateTime(DateTime.Now);
        _Index = _DR.GetOrdinal(_FieldName);
        if (_DR.IsDBNull(_Index))
            return _ResDT;
        DataTypeName = _DR.GetDataTypeName(_Index);
        try
        {
            _ResDT = _DR.GetDateTime(_Index);
        }
        catch { }
        return _ResDT;
    }

    public String Get_TimeToString(String cField)
    {
        return " rtrim(char(coalesce(Hour(" + cField + "),0))) || ':' || (CASE WHEN coalesce(Minute(" + cField + "),0)>=10 THEN rtrim(char(coalesce(Minute(" + cField + "),0))) ELSE '0' || rtrim(char(coalesce(Minute(" + cField + "),0))) END)";
    }

    public String Get_CreateTimeString(String cField_H, String cField_M)
    {
        return " rtrim(char(coalesce(" + cField_H + ",0))) || ':' || (CASE WHEN coalesce(" + cField_M + ",0)>=10 THEN rtrim(char(coalesce(" + cField_M + ",0))) ELSE '0' || rtrim(char(coalesce(" + cField_M + ",0))) END)";
    }

}

