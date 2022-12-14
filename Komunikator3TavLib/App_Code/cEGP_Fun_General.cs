﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Komunikator3TavLib
{
    public struct sEbaMainInfo
    {
        public String EBA_wsLogIn_UserName;
        public String EBA_wsLogIn_Password;
    }

    public class cSettings
    {
        private static String CompanyID = "";
        public static Boolean UseStoredProcedures = false;

        public static int DistribucijaId = -1;
        
        private static Boolean Local_3Tav_StaticConnectionString = true;
        private static cSQLDatabaseType Local_3Tav_DatabaseType = cSQLDatabaseType.dbcSQLServer;
        private static String Local_3Tav_UserName = "";
        private static String Local_3Tav_Password = "";
        private static String Local_3Tav_AliasName = "";
        private static String Local_3Tav_ServerName = "";
        private static Boolean Local_3Tav_IntegratedSecurity = false;

        private static Boolean Local_EG_StaticConnectionString = true;
        private static cSQLDatabaseType Local_EG_DatabaseType = cSQLDatabaseType.dbcSQLServer;
        private static String Local_EG_UserName = "";
        private static String Local_EG_Password = "";
        private static String Local_EG_AliasName = "";
        private static String Local_EG_ServerName = "";
        private static Boolean Local_EG_IntegratedSecurity = false;

        private static Boolean Local_EBA_StaticConnectionString = true;
        private static cSQLDatabaseType Local_EBA_DatabaseType = cSQLDatabaseType.dbcSQLServer;
        private static String Local_EBA_UserName = "";
        private static String Local_EBA_Password = "";
        private static String Local_EBA_AliasName = "";
        private static String Local_EBA_ServerName = "";
        private static Boolean Local_EBA_IntegratedSecurity = false;

        private static Boolean KlicniCenter_StaticConnectionString = true;
        private static cSQLDatabaseType KlicniCenter_DatabaseType = cSQLDatabaseType.dbcSQLServer;
        private static String KlicniCenter_UserName = "";
        private static String KlicniCenter_Password = "";
        private static String KlicniCenter_AliasName = "";
        private static String KlicniCenter_ServerName = "";
        private static Boolean KlicniCenter_IntegratedSecurity = false;

        private static sEbaMainInfo EbaMainInfo = new sEbaMainInfo();

        private static String MailServer = "";
        public static Boolean UseFixed_EMail = false;
        public static String Fixed_EMail = "";

        public static Boolean Fill_FromSettingsFile()
        {
            String _FileName = cCommon.Get_ApplicationRootFolder() + "Setings_Komunikator3Tav.ini";
            Ini.IniFile _IniFile = new Ini.IniFile(_FileName);

            // šifra domače distribucije
            DistribucijaId = Perun3WsLib.Settings.GetDistribucijaId();

            // 3tav
            Local_3Tav_StaticConnectionString = _IniFile.ReadBool("Conn_3tav", "StaticConnectionString", false);
            switch (_IniFile.ReadInteger("Conn_3tav", "DatabaseType", 1))
            {
                case 0: Local_3Tav_DatabaseType = cSQLDatabaseType.dbIBMDB2; break;
                case 2: Local_3Tav_DatabaseType = cSQLDatabaseType.dbMSAccess; break;
                case 3: Local_3Tav_DatabaseType = cSQLDatabaseType.dbcSQLServer; break;
                case 4: Local_3Tav_DatabaseType = cSQLDatabaseType.dbOracle; break;
            }
            Local_3Tav_UserName = _IniFile.ReadString("Conn_3tav", "UserName", "");
            Local_3Tav_Password = cCommon.Decode_Text(_IniFile.ReadString("Conn_3tav", "Password", ""));
            Local_3Tav_AliasName = _IniFile.ReadString("Conn_3tav", "AliasName", "");
            Local_3Tav_ServerName = _IniFile.ReadString("Conn_3tav", "ServerName", "");
            Local_3Tav_IntegratedSecurity = _IniFile.ReadBool("Conn_3tav", "IntegratedSecurity", false);

            

            // EG
            Local_EG_StaticConnectionString = _IniFile.ReadBool("Conn_EG", "StaticConnectionString", false);
            switch (_IniFile.ReadInteger("Conn_EG", "DatabaseType", 1))
            {
                case 0: Local_EG_DatabaseType = cSQLDatabaseType.dbIBMDB2; break;
                case 2: Local_EG_DatabaseType = cSQLDatabaseType.dbMSAccess; break;
                case 3: Local_EG_DatabaseType = cSQLDatabaseType.dbcSQLServer; break;
                case 4: Local_EG_DatabaseType = cSQLDatabaseType.dbOracle; break;
            }
            Local_EG_UserName = _IniFile.ReadString("Conn_EG", "UserName", "");
            Local_EG_Password = cCommon.Decode_Text(_IniFile.ReadString("Conn_EG", "Password", ""));
            Local_EG_AliasName = _IniFile.ReadString("Conn_EG", "AliasName", "");
            Local_EG_ServerName = _IniFile.ReadString("Conn_EG", "ServerName", "");
            Local_EG_IntegratedSecurity = _IniFile.ReadBool("Conn_EG", "IntegratedSecurity", false);

            // EBA
            /*
            Local_EBA_StaticConnectionString = _IniFile.ReadBool("Conn_EBA", "StaticConnectionString", false);
            switch (_IniFile.ReadInteger("Conn_EBA", "DatabaseType", 1))
            {
                case 0: Local_EBA_DatabaseType = cSQLDatabaseType.dbIBMDB2; break;
                case 2: Local_EBA_DatabaseType = cSQLDatabaseType.dbMSAccess; break;
                case 3: Local_EBA_DatabaseType = cSQLDatabaseType.dbcSQLServer; break;
                case 4: Local_EBA_DatabaseType = cSQLDatabaseType.dbOracle; break;
            }
            Local_EBA_UserName = _IniFile.ReadString("Conn_EBA", "UserName", "");
            Local_EBA_Password = cCommon.Decode_Text(_IniFile.ReadString("Conn_EBA", "Password", ""));
            Local_EBA_AliasName = _IniFile.ReadString("Conn_EBA", "AliasName", "");
            Local_EBA_ServerName = _IniFile.ReadString("Conn_EBA", "ServerName", "");
            Local_EBA_IntegratedSecurity = _IniFile.ReadBool("Conn_EBA", "IntegratedSecurity", false);

            // Klicni Center
            KlicniCenter_StaticConnectionString = _IniFile.ReadBool("Conn_KlicniCenter", "StaticConnectionString", false);
            switch (_IniFile.ReadInteger("Conn_KlicniCenter", "DatabaseType", 1))
            {
                case 0: KlicniCenter_DatabaseType = cSQLDatabaseType.dbIBMDB2; break;
                case 2: KlicniCenter_DatabaseType = cSQLDatabaseType.dbMSAccess; break;
                case 3: KlicniCenter_DatabaseType = cSQLDatabaseType.dbcSQLServer; break;
                case 4: KlicniCenter_DatabaseType = cSQLDatabaseType.dbOracle; break;
            }
            KlicniCenter_UserName = _IniFile.ReadString("Conn_KlicniCenter", "UserName", "");
            KlicniCenter_Password = cCommon.Decode_Text(_IniFile.ReadString("Conn_KlicniCenter", "Password", ""));
            KlicniCenter_AliasName = _IniFile.ReadString("Conn_KlicniCenter", "AliasName", "");
            KlicniCenter_ServerName = _IniFile.ReadString("Conn_KlicniCenter", "ServerName", "");
            KlicniCenter_IntegratedSecurity = _IniFile.ReadBool("Conn_KlicniCenter", "IntegratedSecurity", false);

            // EbaMainInfo
            EbaMainInfo.EBA_wsLogIn_UserName = _IniFile.ReadString("EbaMainInfo", "EBA_wsLogIn_UserName", "");
            EbaMainInfo.EBA_wsLogIn_Password = _IniFile.ReadString("EbaMainInfo", "EBA_wsLogIn_Password", "");
            */
            return true;
        }

        public static String CreateSqlConnectionString(cSQLDatabaseType _dbType, String _DB_UserName, String _Password, String _AliasName, String _ServerName, Boolean _IntegratedSecurity)
        {
            String _SqlConnStr = "";
            _SqlConnStr = "user id=" + _DB_UserName + ";password=" + _Password + ";server=" + _ServerName + ";Trusted_Connection=no;database= " + _AliasName + " ;connection timeout=3";
            return _SqlConnStr;
        }

        public static String Get_3Tav_ConnString()
        {
            //return "data source=3tav-sql\\sql2016;initial catalog=3tav_db_is_rwe;persist security info=False;Trusted_Connection=True;packet size=4096";
            return CreateSqlConnectionString(Local_3Tav_DatabaseType, Local_3Tav_UserName, Local_3Tav_Password, Local_3Tav_AliasName, Local_3Tav_ServerName, Local_3Tav_IntegratedSecurity);
        }

        public static String Get_3Tav_ConnStringDOC()
        {
            
            var local_3Tav_AliasName = Local_3Tav_AliasName.Replace("DB_IS_RWE", "DB_DOC_RWE");
            local_3Tav_AliasName = local_3Tav_AliasName.Replace("DB_IS_ECE", "DB_DOC_ECE");
            local_3Tav_AliasName = local_3Tav_AliasName.Replace("_TEST", string.Empty);
            //return "data source=3tav-sql\\sql2016;initial catalog=3tav_db_is_rwe;persist security info=False;Trusted_Connection=True;packet size=4096";
            return CreateSqlConnectionString(Local_3Tav_DatabaseType, Local_3Tav_UserName, Local_3Tav_Password, local_3Tav_AliasName, Local_3Tav_ServerName, Local_3Tav_IntegratedSecurity);
        }


        public static String Get_EG_ConnString()
        {
            return CreateSqlConnectionString(Local_EG_DatabaseType, Local_EG_UserName, Local_EG_Password, Local_EG_AliasName, Local_EG_ServerName, Local_EG_IntegratedSecurity);
        }


        public static String Get_EBA_ConnString()
        {
            return CreateSqlConnectionString(Local_EBA_DatabaseType, Local_EBA_UserName, Local_EBA_Password, Local_EBA_AliasName, Local_EBA_ServerName, Local_EBA_IntegratedSecurity);
        }

        public static String Get_KlicniCenter_ConnString()
        {
            return CreateSqlConnectionString(KlicniCenter_DatabaseType, KlicniCenter_UserName, KlicniCenter_Password, KlicniCenter_AliasName, KlicniCenter_ServerName, KlicniCenter_IntegratedSecurity);
        }


        public static String Get_MailServer()
        {
            return MailServer;
        }

        public static Boolean Get_UseFixed_EMail()
        {
            return UseFixed_EMail;
        }

        public static String Get_Fixed_EMail()
        {
            return Fixed_EMail;
        }

        public static sEbaMainInfo Get_EbaMainInfo()
        {
            return EbaMainInfo;
        }

        public static String Get_CompanyID()
        {
            return CompanyID;
        }
    }

    public class LocalTables
    {
        private static string t_3Tav_bis_odjemna_mesta = "bis_odjemna_mesta";
        private static string t_3Tav_bie_odjemna_mesta = "bie_odjemna_mesta";
        private static string t_3Tav_bie_omnamescenistevci = "bie_omnamescenistevci";
        private static string t_3Tav_bis_merilne_naprave = "bis_merilne_naprave";
        private static string t_3Tav_bie_merilne_naprave = "bie_merilne_naprave";
        private static string t_3Tav_bie_tipi_merilnih_naprav = "bie_tipi_merilnih_naprav";
        private static string t_3Tav_bis_odcitki_v = "bis_odcitki_v";
        private static string t_3Tav_bis_odjemna_mesta_povprecja = "bis_odjemna_mesta_povprecja";
        private static string t_3Tav_bis_pogodbe_gl = "bis_pogodbe_gl";
        private static string t_3Tav_bie_rajoni_popisa = "bie_rajoni_popisa";
        private static string t_3Tav_poslovni_partnerji = "poslovni_partnerji";
        private static string t_3Tav_posta = "posta";
        private static string t_3Tav_vrste_placil = "vrste_placil";
        private static string t_3Tav_bie_odjemna_skupina = "bie_odjemna_skupina";
        private static string t_3Tav_bie_omr_SifTarifneSkupine = "bie_omr_SifTarifneSkupine";
        private static string t_3Tav_bie_DobaviteljElektrike = "bie_DobaviteljElektrike";
        private static string t_3Tav_bis_odjemna_mesta_letne_kolicine = "bis_odjemna_mesta_letne_kolicine";
        private static string t_3Tav_Bie_XML_paketi = "Bie_XML_paketi";
        private static string t_3Tav_bis_Fakture_gl = "bis_Fakture_gl";
        private static string t_3Tav_bis_fakture_gl_PrilogaA = "bis_fakture_gl_PrilogaA";
        private static string t_3Tav_bie_omr_cenik_N1 = "bie_omr_cenik_N1";
        private static string t_3Tav_bie_omr_cenik_N2 = "bie_omr_cenik_N2";
        private static string t_3Tav_bie_omr_cenik_sistem = "bie_omr_cenik_sistem";
        private static string t_3Tav_Bie_ObracunskaMocVarovalke = "Bie_ObracunskaMocVarovalke";
        private static string t_3Tav_Bie_SistemskiOperater = "Bie_SistemskiOperater";
        private static string t_3Tav_zck_MenjaveDobaviteljev = "zck_MenjaveDobaviteljev";
        private static string t_3Tav_zck_OdklopiOM = "zck_OdklopiOM";
        private static string t_3Tav_WmCustomAttachments = "WmCustomAttachments";
        private static string t_3Tav_dwh_RealizacijaKolicine = "dwh_RealizacijaKolicine";
        private static string t_3Tav_pdo_Szp_Pregled_v = "pdo_Szp_Pregled_v";
        private static string t_3Tav_pdo_PoD_pregled_v = "pdo_PoD_pregled_v";

        private static string t_Mowe_EVENTS = "ERPO_EVENTS";
        private static string t_Mowe_CHANGE_MCD = "ERPO_CHANGE_MCD";
        private static string t_Mowe_READ_VAL = "ERPO_READ_VAL";

        private static string t_EG_KEG_USERS_LIST = "KEGP_USERS_LIST";
        private static string t_EG_KEG_USER_AUTHORIZATIONS = "KEGP_USER_AUTHORIZATIONS";
        private static string t_EG_KEG_REQ_MAIN = "KEG_REQ_MAIN";
        private static string t_EG_KEG_REQ_DETAIL = "KEG_REQ_DETAIL";
        private static string t_EG_KEG_RES_MAIN = "KEG_RES_MAIN";
        private static string t_EG_KEG_RES_DETAIL = "KEG_RES_DETAIL";

        public static Boolean Read_TableNames()
        {
            String _FileName = cCommon.Get_ApplicationRootFolder() + "Setings_Komunikator3Tav.ini";

            Ini.IniFile _IniFile = new Ini.IniFile(_FileName);

            t_3Tav_bis_odjemna_mesta = _IniFile.ReadString("3tav_Tables", "bis_odjemna_mesta", "");
            t_3Tav_bie_odjemna_mesta = _IniFile.ReadString("3tav_Tables", "bie_odjemna_mesta", "");
            t_3Tav_bie_omnamescenistevci = _IniFile.ReadString("3tav_Tables", "bie_omnamescenistevci", "");
            t_3Tav_bis_merilne_naprave = _IniFile.ReadString("3tav_Tables", "bis_merilne_naprave", "");
            t_3Tav_bie_merilne_naprave = _IniFile.ReadString("3tav_Tables", "bie_merilne_naprave", "");
            t_3Tav_bie_tipi_merilnih_naprav = _IniFile.ReadString("3tav_Tables", "bie_tipi_merilnih_naprav", "");
            t_3Tav_bis_odcitki_v = _IniFile.ReadString("3tav_Tables", "bis_odcitki_v", "");
            t_3Tav_bis_odjemna_mesta_povprecja = _IniFile.ReadString("3tav_Tables", "bis_odjemna_mesta_povprecja", "");
            t_3Tav_bis_pogodbe_gl = _IniFile.ReadString("3tav_Tables", "bis_pogodbe_gl", "");
            t_3Tav_bie_rajoni_popisa = _IniFile.ReadString("3tav_Tables", "bie_rajoni_popisa", "");
            t_3Tav_poslovni_partnerji = _IniFile.ReadString("3tav_Tables", "poslovni_partnerji", "");
            t_3Tav_posta = _IniFile.ReadString("3tav_Tables", "posta", "");
            t_3Tav_vrste_placil = _IniFile.ReadString("3tav_Tables", "vrste_placil", "");
            t_3Tav_bie_odjemna_skupina = _IniFile.ReadString("3tav_Tables", "bie_odjemna_skupina", "");
            t_3Tav_bie_omr_SifTarifneSkupine = _IniFile.ReadString("3tav_Tables", "bie_omr_SifTarifneSkupine", "");
            t_3Tav_bie_DobaviteljElektrike = _IniFile.ReadString("3tav_Tables", "bie_DobaviteljElektrike", "");
            t_3Tav_bis_odjemna_mesta_letne_kolicine = _IniFile.ReadString("3tav_Tables", "bis_odjemna_mesta_letne_kolicine", "");
            t_3Tav_Bie_XML_paketi = _IniFile.ReadString("3tav_Tables", "Bie_XML_paketi", "");
            t_3Tav_bis_Fakture_gl = _IniFile.ReadString("3tav_Tables", "bis_Fakture_gl", "");
            t_3Tav_bis_fakture_gl_PrilogaA = _IniFile.ReadString("3tav_Tables", "bis_fakture_gl_PrilogaA", "");
            t_3Tav_bie_omr_cenik_N1 = _IniFile.ReadString("3tav_Tables", "bie_omr_cenik_N1", "");
            t_3Tav_bie_omr_cenik_N2 = _IniFile.ReadString("3tav_Tables", "bie_omr_cenik_N2", "");
            t_3Tav_bie_omr_cenik_sistem = _IniFile.ReadString("3tav_Tables", "bie_omr_cenik_sistem", "");
            t_3Tav_Bie_ObracunskaMocVarovalke = _IniFile.ReadString("3tav_Tables", "Bie_ObracunskaMocVarovalke", "");
            t_3Tav_Bie_SistemskiOperater = _IniFile.ReadString("3tav_Tables", "Bie_SistemskiOperater", "");
            t_3Tav_zck_MenjaveDobaviteljev = _IniFile.ReadString("3tav_Tables", "zck_MenjaveDobaviteljev", "");
            t_3Tav_zck_OdklopiOM = _IniFile.ReadString("3tav_Tables", "zck_OdklopiOM", "");
            t_3Tav_WmCustomAttachments = _IniFile.ReadString("3tav_Tables", "WmCustomAttachments", "");
            t_3Tav_dwh_RealizacijaKolicine = _IniFile.ReadString("3tav_Tables", "dwh_RealizacijaKolicine", "");
            t_3Tav_pdo_Szp_Pregled_v = _IniFile.ReadString("3tav_Tables", "pdo_Szp_Pregled_v", "");
            t_3Tav_pdo_PoD_pregled_v = _IniFile.ReadString("3tav_Tables", "pdo_PoD_pregled_v", "");
            t_Mowe_EVENTS = _IniFile.ReadString("Mowe_Tables", "EVENTS", "");
            t_Mowe_CHANGE_MCD = _IniFile.ReadString("Mowe_Tables", "CHANGE_MCD", "");
            t_Mowe_READ_VAL = _IniFile.ReadString("Mowe_Tables", "READ_VAL", "");

            t_EG_KEG_USERS_LIST = _IniFile.ReadString("EG_Tables", "KEG_USERS_LIST", "");
            t_EG_KEG_USER_AUTHORIZATIONS = _IniFile.ReadString("EG_Tables", "KEG_USER_AUTHORIZATIONS", "");
            t_EG_KEG_REQ_MAIN = _IniFile.ReadString("EG_Tables", "KEG_REQ_MAIN", "");
            t_EG_KEG_REQ_DETAIL = _IniFile.ReadString("EG_Tables", "KEG_REQ_DETAIL", "");
            t_EG_KEG_RES_MAIN = _IniFile.ReadString("EG_Tables", "KEG_RES_MAIN", "");
            t_EG_KEG_RES_DETAIL = _IniFile.ReadString("EG_Tables", "KEG_RES_DETAIL", "");
            return true;
        }

        public static string Get_3Tav_bis_odjemna_mesta()
        {
            return t_3Tav_bis_odjemna_mesta;
        }

        public static string Get_3Tav_bie_odjemna_mesta()
        {
            return t_3Tav_bie_odjemna_mesta;
        }

        public static string Get_3Tav_bis_fakture_gl_PrilogaA()
        {
            return t_3Tav_bis_fakture_gl_PrilogaA;
        }

        public static string Get_3Tav_bie_omnamescenistevci()
        {
            return t_3Tav_bie_omnamescenistevci;
        }

        public static string Get_3Tav_bis_merilne_naprave()
        {
            return t_3Tav_bis_merilne_naprave;
        }

        public static string Get_3Tav_bie_merilne_naprave()
        {
            return t_3Tav_bie_merilne_naprave;
        }

        public static string Get_3Tav_bie_tipi_merilnih_naprav()
        {
            return t_3Tav_bie_tipi_merilnih_naprav;
        }

        public static string Get_3Tav_bis_odcitki_v()
        {
            return t_3Tav_bis_odcitki_v;
        }

        public static string Get_3Tav_bis_odjemna_mesta_povprecja()
        {
            return t_3Tav_bis_odjemna_mesta_povprecja;
        }

        public static string Get_3Tav_bis_pogodbe_gl()
        {
            return t_3Tav_bis_pogodbe_gl;
        }

        public static string Get_3Tav_bie_rajoni_popisa()
        {
            return t_3Tav_bie_rajoni_popisa;
        }

        public static string Get_3Tav_Poslovni_partnerji()
        {
            return t_3Tav_poslovni_partnerji;
        }

        public static string Get_3Tav_Posta()
        {
            return t_3Tav_posta;
        }

        public static string Get_3Tav_Vrste_Placil()
        {
            return t_3Tav_vrste_placil;
        }

        public static string Get_3Tav_bie_odjemna_skupina()
        {
            return t_3Tav_bie_odjemna_skupina;
        }

        public static string Get_3Tav_bie_omr_SifTarifneSkupine()
        {
            return t_3Tav_bie_omr_SifTarifneSkupine;
        }

        public static string Get_3Tav_bie_DobaviteljElektrike()
        {
            return t_3Tav_bie_DobaviteljElektrike;
        }

        public static string Get_3Tav_bis_odjemna_mesta_letne_kolicine()
        {
            return t_3Tav_bis_odjemna_mesta_letne_kolicine;
        }

        public static string Get_3Tav_Bie_XML_paketi()
        {
            return t_3Tav_Bie_XML_paketi;
        }

        public static string Get_3Tav_bis_Fakture_gl()
        {
            return t_3Tav_bis_Fakture_gl;
        }

        public static string Get_3Tav_bie_omr_cenik_N1()
        {
            return t_3Tav_bie_omr_cenik_N1;
        }

        public static string Get_3Tav_bie_omr_cenik_N2()
        {
            return t_3Tav_bie_omr_cenik_N2;
        }

        public static string Get_3Tav_Bie_ObracunskaMocVarovalke()
        {
            return t_3Tav_Bie_ObracunskaMocVarovalke;
        }

        public static string Get_3Tav_bie_omr_cenik_sistem()
        {
            return t_3Tav_bie_omr_cenik_sistem;
        }

        public static string Get_3Tav_Bie_SistemskiOperater()
        {
            return t_3Tav_Bie_SistemskiOperater;
        }

        public static string Get_3Tav_zck_MenjaveDobaviteljev()
        {
            return t_3Tav_zck_MenjaveDobaviteljev;
        }

        public static string Get_3Tav_zck_OdklopiOM()
        {
            return t_3Tav_zck_OdklopiOM;
        }

        public static string Get_3Tav_WmCustomAttachments()
        {
            return t_3Tav_WmCustomAttachments;
        }

        public static string Get_3Tav_dwh_RealizacijaKolicine()
        {
            return t_3Tav_dwh_RealizacijaKolicine;
        }

        public static string Get_3Tav_pdo_Szp_Pregled_v()
        {
            return t_3Tav_pdo_Szp_Pregled_v;
        }

        public static string Get_3Tav_pdo_PoD_pregled_v()
        {
            return t_3Tav_pdo_PoD_pregled_v;
        }


        public static string Get_Mowe_EVENTS()
        {
            return t_Mowe_EVENTS;
        }

        public static string Get_Mowe_CHANGE_MCD()
        {
            return t_Mowe_CHANGE_MCD;
        }

        public static string Get_Mowe_READ_VAL()
        {
            return t_Mowe_READ_VAL;
        }

        public static string Get_EG_KEG_USERS_LIST()
        {
            return t_EG_KEG_USERS_LIST;
        }

        public static string Get_EG_KEG_USER_AUTHORIZATIONS()
        {
            return t_EG_KEG_USER_AUTHORIZATIONS;
        }

        public static string Get_EG_KEG_REQ_MAIN()
        {
            return t_EG_KEG_REQ_MAIN;
        }

        public static string Get_EG_KEG_REQ_DETAIL()
        {
            return t_EG_KEG_REQ_DETAIL;
        }

        public static string Get_EG_KEG_RES_MAIN()
        {
            return t_EG_KEG_RES_MAIN;
        }

        public static string Get_EG_KEG_RES_DETAIL()
        {
            return t_EG_KEG_RES_DETAIL;
        }
    }

    public class cDobaviteljElektrike
    {
        private static String SqlConnStr;
        private static Boolean Valid = false;
        private static List<Int32> DobaviteljElektrike;

        public static sFunctionResult Get_ListOfDobaviteljElektrike(String _SqlConnStr)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(_SqlConnStr);
            SqlDataReader dbReader;

            Valid = false;

            DobaviteljElektrike = new List<Int32>(0);
            sql.Text = sql.Text + "select IdDobaviteljEl" + cCommon.CR();
            sql.Text = sql.Text + "from Bie_DobaviteljElektrike" + cCommon.CR();
            sql.Text = sql.Text + "order by IdDobaviteljEl" + cCommon.CR();


            dbReader = sql.Query_Read("cDobaviteljElektrike.Get_ListOfDobaviteljElektrike", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        DobaviteljElektrike.Add(sql.GetField_Int(ref dbReader, "IdDobaviteljEl"));
                    }
                }
                else
                {
                    return cFunctionResult.Set(false, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            Valid = true;
            return fr;
        }

        public static Boolean IsDobaviteljElektrikeValid(Int32 _DobaviteljElektrike)
        {
            if (!Valid)
            {
                if (!Get_ListOfDobaviteljElektrike(SqlConnStr).resBool)
                {
                    return false;
                }
            }
            if (DobaviteljElektrike == null)
            {
                return false;
            }
            return DobaviteljElektrike.Contains(_DobaviteljElektrike);
        }
    }
}