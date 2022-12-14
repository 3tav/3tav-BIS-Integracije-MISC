using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;

namespace Komunikator3TavLib
{
    public class cAuthentication
    {
        /// <summary>
        /// podatki o podatkovni bazi
        /// </summary>
        private String SqlConnStr;

        public cAuthentication(String _SqlConnStr)
        {
            this.SqlConnStr = _SqlConnStr;
            var userName = "";

            if (OperationContext.Current != null && OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
            {
                userName += OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            }
            if (userName.ToUpper() == ("EG\\eg0624").ToUpper())
            {
                userName = "EGwsAdmin";
            }

        }

        public cAuthentication(String _SqlConnStr, out String _KUL_USERNAME, out Int32? _KUL_UID)
        {
            this.SqlConnStr = _SqlConnStr;
            var userName = "";

            if (OperationContext.Current != null && OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
            {
                userName += OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            }
            if (userName.ToUpper() == ("EG\\eg0624").ToUpper())
            {
                userName = "EGwsAdmin";
            }
            _KUL_USERNAME = userName;

            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            sql.Text = sql.Text + "select KUL_UID" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_EG_KEG_USERS_LIST() + cCommon.CR();
            sql.Text = sql.Text + "where KUL_USERNAME = @KUL_USERNAME" + cCommon.CR();

            sql.AddSQLParameter("@KUL_USERNAME", SqlDbType.VarChar, ParameterDirection.Input, _KUL_USERNAME); dbReader = sql.Query_Read("cAuthentication.Get_KUL_UIDForUser", ref fr);
            _KUL_UID = null;
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _KUL_UID = sql.GetField_IntNull(ref dbReader, "KUL_UID");
                    fr = cFunctionResult.Set(true, (int)efrErrorCodes.OK, "", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
        }

        public String Get_UserName()
        {
            var userName = "";

            if (OperationContext.Current != null && OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
            {
                userName += OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            }
            return userName;
        }

        public sFunctionResult Check_Authentication(ewsPrivilegeForWebServiceID _WebServiceID, ewsPrivilegeForRoutineID _RoutineID)
        {
            sFunctionResult fr = cFunctionResult.Init();
            var userName = "";
            if (OperationContext.Current != null && OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
            {
                userName += OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            }
            if (userName.ToUpper() == ("EG\\eg0624").ToUpper())
            {
                userName = "EGwsAdmin";
            }

            cAuthentication Authentic = new cAuthentication(cSettings.Get_EG_ConnString());

            fr = Get_PrivilegesForUser(userName, _WebServiceID, _RoutineID);

            if ((!fr.resBool) || (fr.resInteger != (int)efrErrorCodes.OK))
            {
                return fr;
            }
            return fr;
        }

        public sFunctionResult Get_IsUserNameAndPasswordOK(String _UserName, String _Password, out Boolean _Valid)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            _Valid = false;
            sql.Text = sql.Text + "select KUL_UID, KUL_USERNAME, KUL_PASSWORD" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_EG_KEG_USERS_LIST() + cCommon.CR();
            sql.Text = sql.Text + "where KUL_USERNAME = @KUL_USERNAME" + cCommon.CR();

            sql.AddSQLParameter("@KUL_USERNAME", SqlDbType.VarChar, ParameterDirection.Input, _UserName);
            dbReader = sql.Query_Read("cAuthentication.Get_IsUserNameAndPasswordOK", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    if (sql.GetField_String(ref dbReader, "KUL_PASSWORD") != _Password)
                    {
                        return cFunctionResult.Set(true, (int)efrErrorCodes.NotAuthorized, "Avtentikacija", "Napačno geslo.", "");
                    }
                    fr = cFunctionResult.Set(true, (int)efrErrorCodes.OK, "Avtentikacija", "OK.", "");
                    _Valid = true;
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NotAuthorized, "Avtentikacija", "Uporabnik ni najden v podatkovni bazi.", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public eNacinPridobitveOdcitka Get_NacinPridobitveOdcitkaFormUserName()
        {
            var userName = "";
            eNacinPridobitveOdcitka NaciPridobitveOdcitka;
            if (OperationContext.Current != null && OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
            {
                userName += OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            }
            NaciPridobitveOdcitka = eNacinPridobitveOdcitka.Dobavitelj;


            if (userName.ToUpper() == "EGProdaja".ToUpper())
            {
                NaciPridobitveOdcitka = eNacinPridobitveOdcitka.Dobavitelj;
            }
            else if (userName.ToUpper() == "Informatika".ToUpper())
            {
                NaciPridobitveOdcitka = eNacinPridobitveOdcitka.Preun;
            }
            return NaciPridobitveOdcitka;
        }

        private sFunctionResult Get_PrivilegesForUser(String _UserName, ewsPrivilegeForWebServiceID _WebServiceID, ewsPrivilegeForRoutineID _RoutineID)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            sql.Text = sql.Text + "select KUL_UID, KUL_USERNAME, KUL_PASSWORD, KUL_COMPANY_ID, KUL_ACTIVE, KUA_WS_UID, KUA_ROUTINE_UID" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_EG_KEG_USERS_LIST() + " left join " + LocalTables.Get_EG_KEG_USER_AUTHORIZATIONS() + " on ((KUL_UID=KUA_KUL_UID) and (KUA_WS_UID=@KUA_WS_UID) and (KUA_ROUTINE_UID=@KUA_ROUTINE_UID))" + cCommon.CR();
            sql.Text = sql.Text + "where KUL_USERNAME = @KUL_USERNAME" + cCommon.CR();

            sql.AddSQLParameter("@KUL_USERNAME", SqlDbType.VarChar, ParameterDirection.Input, _UserName);
            sql.AddSQLParameter("@KUA_WS_UID", SqlDbType.Int, ParameterDirection.Input, (int)_WebServiceID);
            sql.AddSQLParameter("@KUA_ROUTINE_UID", SqlDbType.Int, ParameterDirection.Input, (int)_RoutineID);
            dbReader = sql.Query_Read("cAuthentication.Get_PrivilegesForUser", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    if ((sql.GetField_IsNull(ref dbReader, "KUA_WS_UID")) || (sql.GetField_IsNull(ref dbReader, "KUA_ROUTINE_UID")))
                    {
                        return cFunctionResult.Set(true, (int)efrErrorCodes.NotAuthorized, "Avtentikacija", "Nimate pravic za to storitev.", "");
                    }
                    fr = cFunctionResult.Set(true, (int)efrErrorCodes.OK, "Avtentikacija", "OK.", "");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NotAuthorized, "Avtentikacija", "Uporabnik ni najden v podatkovni bazi.", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
    }
}

