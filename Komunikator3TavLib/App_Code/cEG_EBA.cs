using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Security;
using System.Text;

namespace Komunikator3TavLib
{
    public enum eZCODocumentTypeForEBA
    {
        PogodbaODobavi = 100,
        ZahtevaZaEvidencoMM = 300
    }

    public struct sGeneralDocumentMetaData
    {
        public String Key;
        public String Value;
    }


    [DataContract]
    public struct dcDS_Dokument
    {
        [DataMember]
        public String DokumentID { get; set; }
        [DataMember]
        public String EAD { get; set; }
        [DataMember]
        public String Predmet { get; set; }
        [DataMember]
        public String TipDokumenta { get; set; }
        [DataMember]
        public String Smer { get; set; }
        [DataMember]
        public DateTime? DatumSprejema { get; set; }
        [DataMember]
        public DateTime? DatumOddaje { get; set; }
        [DataMember]
        public String PartnerNaziv { get; set; }
        [DataMember]
        public String SkrbnikDokumenta { get; set; }
    }

    [DataContract]
    public class dcDS_DokumentList
    {
        [DataMember]
        public int SteviloDokumentov { get; set; }
        [DataMember]
        public List<dcDS_Dokument> Data { get; set; }
    }

    public class cDS_Dokumenti
    {
        /// <summary>
        /// podatki o podatkovni bazi
        /// </summary>
        private String SqlConnStr;



        public cDS_Dokumenti(String _SqlConnStr)
        {
            this.SqlConnStr = _SqlConnStr;
        }


        /// <summary>
        /// Dobimo podatke o nameščenih napravah
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_VrstaNaprave">Vrsta naprave (opcijsko)</param>
        /// <param name="_NamesceneNaprave">out List<sNamescenaNaprava></param>
        /// <returns></returns>
        public sFunctionResult Get_DocumentsForPartner(List<Int32> _ListOfPartnerID, String _UserEVI, out List<dcDS_Dokument> _Dokumenti)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            dcDS_Dokument DS_Dokument;

            _Dokumenti = new List<dcDS_Dokument>(0);

            if (_ListOfPartnerID.Count == 0)
            {
                return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Preverjanje partnerjev", "Seznam partnerjev je prazen.", "");
            }

            sql.Text = sql.Text + "select * " + cCommon.CR();
            sql.Text = sql.Text + "from ( " + cCommon.CR();
            sql.Text = sql.Text + "select distinct eba_doc.id, eba_doc.ident EAD, " + cCommon.CR();
            sql.Text = sql.Text + "      eba_doc.subject As Predmet, eba_doc.doctype Tip_dokumenta," + cCommon.CR();

            //sql.Text = sql.Text + "       CASE WHEN eba_doc_permissions.extern_user_id = '" + _UserEVI + "' THEN eba_doc.subject ELSE '***' END As Predmet, " + cCommon.CR();
            //sql.Text = sql.Text + "       CASE WHEN eba_doc_permissions.extern_user_id = '" + _UserEVI + "' THEN eba_doc.doctype ELSE '***' END As Tip_dokumenta," + cCommon.CR();
            sql.Text = sql.Text + "       eba_doc.Smer, eba_doc.received_date, eba_doc.sent_date, eba_doc.name As PP_naziv , eba_doc.username as Skrbnik" + cCommon.CR();
            sql.Text = sql.Text + "from " + cCommon.CR();
            sql.Text = sql.Text + "(Select" + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.subject," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.username," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.doctype," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.received_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.sent_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.name," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.extern_id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_.ident," + cCommon.CR();
            sql.Text = sql.Text + "  'Vhodni' As Smer" + cCommon.CR();
            sql.Text = sql.Text + "From" + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents On eba_documents.sendercompanyid = eba_addressbook.ebaid Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_ On eba_documents.id = eba_ctx_4__global_supp_.id" + cCommon.CR();
            sql.Text = sql.Text + "Union" + cCommon.CR();
            sql.Text = sql.Text + "Select" + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.subject," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.username," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.doctype," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.received_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents.sent_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.name," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.extern_id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_.ident," + cCommon.CR();
            sql.Text = sql.Text + "  'Izhodni'" + cCommon.CR();
            sql.Text = sql.Text + "From" + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_documents On eba_documents.receivercompanyid = eba_addressbook.ebaid Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_ On eba_documents.id = eba_ctx_4__global_supp_.id" + cCommon.CR();
            sql.Text = sql.Text + "Union" + cCommon.CR();
            sql.Text = sql.Text + "Select" + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.subject," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.username," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.doctype," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.received_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.sent_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.name," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.extern_id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_.ident," + cCommon.CR();
            sql.Text = sql.Text + "  'Vhodni'" + cCommon.CR();
            sql.Text = sql.Text + "From" + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive On eba_archive.sendercompanyid = eba_addressbook.ebaid Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_ On eba_archive.id = eba_ctx_4__global_supp_.id" + cCommon.CR();
            sql.Text = sql.Text + "Union" + cCommon.CR();
            sql.Text = sql.Text + "Select" + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.subject," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.username," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.doctype," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.received_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive.sent_date," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.name," + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook.extern_id," + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_.ident," + cCommon.CR();
            sql.Text = sql.Text + "  'Izhodni'" + cCommon.CR();
            sql.Text = sql.Text + "From" + cCommon.CR();
            sql.Text = sql.Text + "  eba_addressbook Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_archive On eba_archive.receivercompanyid = eba_addressbook.ebaid Inner Join" + cCommon.CR();
            sql.Text = sql.Text + "  eba_ctx_4__global_supp_ On eba_archive.id = eba_ctx_4__global_supp_.id" + cCommon.CR();
            sql.Text = sql.Text + ") as eba_doc " + cCommon.CR();
            sql.Text = sql.Text + "left join" + cCommon.CR();
            sql.Text = sql.Text + "(" + cCommon.CR();
            // UPORABNIKI, KI IMAJO PRAVICE PREKO GRUPE
            sql.Text = sql.Text + "(select docid, src.caption, eba_users_ex.group_id, eba_users_ex.user_id, eba_users_ex.extern_user_id" + cCommon.CR();
            sql.Text = sql.Text + "from " + cCommon.CR();
            sql.Text = sql.Text + "((select " + cCommon.CR();
            sql.Text = sql.Text + "       foo.docid," + cCommon.CR();
            sql.Text = sql.Text + "       foo.id, " + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.caption " + cCommon.CR();
            sql.Text = sql.Text + "   from " + cCommon.CR();
            sql.Text = sql.Text + "       ( " + cCommon.CR();
            sql.Text = sql.Text + "       select " + cCommon.CR();
            sql.Text = sql.Text + "            eba_org_bit_groups.id as docid," + cCommon.CR();
            sql.Text = sql.Text + "            eba_organizationschema.id as id, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1 and cast( group_id as int ) < 32 then eba_org_bit_groups.col_00 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c0, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 32 and cast( group_id as int ) < 64 then eba_org_bit_groups.col_01 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c1, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 64 and cast( group_id as int ) < 96 then eba_org_bit_groups.col_02 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c2, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 96 and cast( group_id as int ) < 128 then eba_org_bit_groups.col_03 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c3, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 128 and cast( group_id as int ) < 160 then eba_org_bit_groups.col_04 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c4, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 160 and cast( group_id as int ) < 192 then eba_org_bit_groups.col_05 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c5, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 192 and cast( group_id as int ) < 224 then eba_org_bit_groups.col_06 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c6, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 224 and cast( group_id as int ) < 256 then eba_org_bit_groups.col_07 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c7, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 256 and cast( group_id as int ) < 288 then eba_org_bit_groups.col_08 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c8, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 288 and cast( group_id as int ) < 320 then eba_org_bit_groups.col_09 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c9, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 320 and cast( group_id as int ) < 352 then eba_org_bit_groups.col_10 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c10, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 352 and cast( group_id as int ) < 384 then eba_org_bit_groups.col_11 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c11," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 384 and cast( group_id as int ) < 416 then eba_org_bit_groups.col_12 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c12," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 416 and cast( group_id as int ) < 448 then eba_org_bit_groups.col_13 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c13," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 448 and cast( group_id as int ) < 480 then eba_org_bit_groups.col_14 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c14," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 480 and cast( group_id as int ) < 512 then eba_org_bit_groups.col_15 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c15," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 512 and cast( group_id as int ) < 544 then eba_org_bit_groups.col_16 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c16," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 544 and cast( group_id as int ) < 576 then eba_org_bit_groups.col_17 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c17," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 576 and cast( group_id as int ) < 608 then eba_org_bit_groups.col_18 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c18," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 608 and cast( group_id as int ) < 640 then eba_org_bit_groups.col_19 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c19," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 672 then eba_org_bit_groups.col_20 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c20," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 672 and cast( group_id as int ) < 704 then eba_org_bit_groups.col_21 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c21," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 704 and cast( group_id as int ) < 736 then eba_org_bit_groups.col_22 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c22," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 736 and cast( group_id as int ) < 768 then eba_org_bit_groups.col_23 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c23," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 768 and cast( group_id as int ) < 800 then eba_org_bit_groups.col_24 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c24," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 800 and cast( group_id as int ) < 832 then eba_org_bit_groups.col_25 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c25," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 832 and cast( group_id as int ) < 864 then eba_org_bit_groups.col_26 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c26," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 864 and cast( group_id as int ) < 896 then eba_org_bit_groups.col_27 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c27," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 896 and cast( group_id as int ) < 352 then eba_org_bit_groups.col_28 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c28," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 928 then eba_org_bit_groups.col_29 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c29," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 928 and cast( group_id as int ) < 960 then eba_org_bit_groups.col_30 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c30," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 960 and cast( group_id as int ) < 992 then eba_org_bit_groups.col_31 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c31," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 992 and cast( group_id as int ) < 1024 then eba_org_bit_groups.col_32 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c32," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1024 and cast( group_id as int ) < 1056 then eba_org_bit_groups.col_33 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c33," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1056 and cast( group_id as int ) < 1088 then eba_org_bit_groups.col_34 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c34," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1088 and cast( group_id as int ) < 1120 then eba_org_bit_groups.col_35 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c35," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1120 and cast( group_id as int ) < 1152 then eba_org_bit_groups.col_36 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c36," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1152 and cast( group_id as int ) < 1184 then eba_org_bit_groups.col_37 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c37," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1184 and cast( group_id as int ) < 1216 then eba_org_bit_groups.col_38 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c38," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1216 and cast( group_id as int ) < 1248 then eba_org_bit_groups.col_39 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c39," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1248 and cast( group_id as int ) < 1280 then eba_org_bit_groups.col_40 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c40" + cCommon.CR();
            sql.Text = sql.Text + "       from eba_org_bit_groups, eba_organizationschema" + cCommon.CR();
            sql.Text = sql.Text + "       where " + cCommon.CR();
            sql.Text = sql.Text + "           eba_organizationschema.unit_type <> 4" + cCommon.CR();
            sql.Text = sql.Text + "       ) as foo " + cCommon.CR();
            sql.Text = sql.Text + "       Inner Join eba_organizationschema On foo.id = eba_organizationschema.id " + cCommon.CR();
            sql.Text = sql.Text + "   where(c0 <> 0 Or c1 <> 0 Or c2 <> 0 Or c3 <> 0 Or c4 <> 0 Or c5 <> 0 Or c6 <> 0 Or c7 <> 0 Or c8 <> 0 Or c9 <> 0 Or c10 <> 0 Or c11 <> 0 Or c12 <> 0 Or c13 <> 0 Or c14 <> 0 Or c15 <> 0 Or c16 <> 0 Or c17 <> 0 Or c18 <> 0 Or c19 <> 0 Or c20 <> 0" + cCommon.CR();
            sql.Text = sql.Text + "         Or c21 <> 0 Or c22 <> 0 Or c23 <> 0 Or c24 <> 0 Or c25 <> 0 Or c26 <> 0 Or c27 <> 0 Or c28 <> 0 Or c29 <> 0 Or c30 <> 0 Or c31 <> 0 Or c32 <> 0 Or c33 <> 0 Or c34 <> 0 Or c35 <> 0 Or c36 <> 0 Or c37 <> 0 Or c38 <> 0 Or c39 <> 0 Or c40 <> 0)" + cCommon.CR();
            sql.Text = sql.Text + ")" + cCommon.CR();
            sql.Text = sql.Text + "   UNION" + cCommon.CR();
            sql.Text = sql.Text + "(select " + cCommon.CR();
            sql.Text = sql.Text + "       foo.docid," + cCommon.CR();
            sql.Text = sql.Text + "       foo.id, " + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.caption " + cCommon.CR();
            sql.Text = sql.Text + "   from " + cCommon.CR();
            sql.Text = sql.Text + "       ( " + cCommon.CR();
            sql.Text = sql.Text + "       select " + cCommon.CR();
            sql.Text = sql.Text + "            eba_org_archive_bit_groups.id as docid," + cCommon.CR();
            sql.Text = sql.Text + "            eba_organizationschema.id as id, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1 and cast( group_id as int ) < 32 then eba_org_archive_bit_groups.col_00 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c0, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 32 and cast( group_id as int ) < 64 then eba_org_archive_bit_groups.col_01 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c1, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 64 and cast( group_id as int ) < 96 then eba_org_archive_bit_groups.col_02 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c2, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 96 and cast( group_id as int ) < 128 then eba_org_archive_bit_groups.col_03 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c3, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 128 and cast( group_id as int ) < 160 then eba_org_archive_bit_groups.col_04 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c4, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 160 and cast( group_id as int ) < 192 then eba_org_archive_bit_groups.col_05 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c5, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 192 and cast( group_id as int ) < 224 then eba_org_archive_bit_groups.col_06 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c6, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 224 and cast( group_id as int ) < 256 then eba_org_archive_bit_groups.col_07 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c7, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 256 and cast( group_id as int ) < 288 then eba_org_archive_bit_groups.col_08 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c8, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 288 and cast( group_id as int ) < 320 then eba_org_archive_bit_groups.col_09 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c9, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 320 and cast( group_id as int ) < 352 then eba_org_archive_bit_groups.col_10 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c10," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 352 and cast( group_id as int ) < 384 then eba_org_archive_bit_groups.col_11 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c11," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 384 and cast( group_id as int ) < 416 then eba_org_archive_bit_groups.col_12 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c12," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 416 and cast( group_id as int ) < 448 then eba_org_archive_bit_groups.col_13 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c13," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 448 and cast( group_id as int ) < 480 then eba_org_archive_bit_groups.col_14 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c14," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 480 and cast( group_id as int ) < 512 then eba_org_archive_bit_groups.col_15 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c15," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 512 and cast( group_id as int ) < 544 then eba_org_archive_bit_groups.col_16 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c16," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 544 and cast( group_id as int ) < 576 then eba_org_archive_bit_groups.col_17 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c17," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 576 and cast( group_id as int ) < 608 then eba_org_archive_bit_groups.col_18 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c18," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 608 and cast( group_id as int ) < 640 then eba_org_archive_bit_groups.col_19 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c19," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 672 then eba_org_archive_bit_groups.col_20 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c20," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 672 and cast( group_id as int ) < 704 then eba_org_archive_bit_groups.col_21 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c21," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 704 and cast( group_id as int ) < 736 then eba_org_archive_bit_groups.col_22 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c22," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 736 and cast( group_id as int ) < 768 then eba_org_archive_bit_groups.col_23 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c23," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 768 and cast( group_id as int ) < 800 then eba_org_archive_bit_groups.col_24 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c24," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 800 and cast( group_id as int ) < 832 then eba_org_archive_bit_groups.col_25 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c25," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 832 and cast( group_id as int ) < 864 then eba_org_archive_bit_groups.col_26 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c26," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 864 and cast( group_id as int ) < 896 then eba_org_archive_bit_groups.col_27 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c27," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 896 and cast( group_id as int ) < 352 then eba_org_archive_bit_groups.col_28 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c28," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 928 then eba_org_archive_bit_groups.col_29 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c29," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 928 and cast( group_id as int ) < 960 then eba_org_archive_bit_groups.col_30 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c30," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 960 and cast( group_id as int ) < 992 then eba_org_archive_bit_groups.col_31 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c31," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 992 and cast( group_id as int ) < 1024 then eba_org_archive_bit_groups.col_32 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c32," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1024 and cast( group_id as int ) < 1056 then eba_org_archive_bit_groups.col_33 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c33," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1056 and cast( group_id as int ) < 1088 then eba_org_archive_bit_groups.col_34 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c34," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1088 and cast( group_id as int ) < 1120 then eba_org_archive_bit_groups.col_35 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c35," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1120 and cast( group_id as int ) < 1152 then eba_org_archive_bit_groups.col_36 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c36," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1152 and cast( group_id as int ) < 1184 then eba_org_archive_bit_groups.col_37 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c37," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1184 and cast( group_id as int ) < 1216 then eba_org_archive_bit_groups.col_38 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c38," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1216 and cast( group_id as int ) < 1248 then eba_org_archive_bit_groups.col_39 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c39," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1248 and cast( group_id as int ) < 1280 then eba_org_archive_bit_groups.col_40 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c40 " + cCommon.CR();
            sql.Text = sql.Text + "       from eba_org_archive_bit_groups, eba_organizationschema " + cCommon.CR();
            sql.Text = sql.Text + "       ) as foo " + cCommon.CR();
            sql.Text = sql.Text + "       Inner Join eba_organizationschema On foo.id = eba_organizationschema.id " + cCommon.CR();
            sql.Text = sql.Text + "   where(c0 <> 0 Or c1 <> 0 Or c2 <> 0 Or c3 <> 0 Or c4 <> 0 Or c5 <> 0 Or c6 <> 0 Or c7 <> 0 Or c8 <> 0 Or c9 <> 0 Or c10 <> 0 Or c11 <> 0 Or c12 <> 0 Or c13 <> 0 Or c14 <> 0 Or c15 <> 0 Or c16 <> 0 Or c17 <> 0 Or c18 <> 0 Or c19 <> 0 Or c20 <> 0" + cCommon.CR();
            sql.Text = sql.Text + "         Or c21 <> 0 Or c22 <> 0 Or c23 <> 0 Or c24 <> 0 Or c25 <> 0 Or c26 <> 0 Or c27 <> 0 Or c28 <> 0 Or c29 <> 0 Or c30 <> 0 Or c31 <> 0 Or c32 <> 0 Or c33 <> 0 Or c34 <> 0 Or c35 <> 0 Or c36 <> 0 Or c37 <> 0 Or c38 <> 0 Or c39 <> 0 Or c40 <> 0)" + cCommon.CR();
            sql.Text = sql.Text + ")) src" + cCommon.CR();
            sql.Text = sql.Text + "left join eba_organizationschema on src.id = eba_organizationschema.parentid" + cCommon.CR();
            sql.Text = sql.Text + "left join eba_users_ex on eba_organizationschema.group_id=eba_users_ex.group_id)" + cCommon.CR();
            sql.Text = sql.Text + "UNION ALL" + cCommon.CR();
            sql.Text = sql.Text + "(select docid,'', src.group_id, eba_users_ex.user_id, eba_users_ex.extern_user_id" + cCommon.CR();
            sql.Text = sql.Text + "from " + cCommon.CR();
            //-- UPORABNIKI, KI IMAJO NEPOSREDNO PRAVICO
            sql.Text = sql.Text + "((select " + cCommon.CR();
            sql.Text = sql.Text + "       foo.docid," + cCommon.CR();
            sql.Text = sql.Text + "       foo.id, " + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.caption," + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.group_id " + cCommon.CR();
            sql.Text = sql.Text + "   from " + cCommon.CR();
            sql.Text = sql.Text + "       ( " + cCommon.CR();
            sql.Text = sql.Text + "       select " + cCommon.CR();
            sql.Text = sql.Text + "            eba_org_bit_groups.id as docid," + cCommon.CR();
            sql.Text = sql.Text + "            eba_organizationschema.id as id, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1 and cast( group_id as int ) < 32 then eba_org_bit_groups.col_00 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c0, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 32 and cast( group_id as int ) < 64 then eba_org_bit_groups.col_01 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c1, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 64 and cast( group_id as int ) < 96 then eba_org_bit_groups.col_02 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c2, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 96 and cast( group_id as int ) < 128 then eba_org_bit_groups.col_03 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c3, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 128 and cast( group_id as int ) < 160 then eba_org_bit_groups.col_04 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c4, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 160 and cast( group_id as int ) < 192 then eba_org_bit_groups.col_05 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c5, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 192 and cast( group_id as int ) < 224 then eba_org_bit_groups.col_06 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c6, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 224 and cast( group_id as int ) < 256 then eba_org_bit_groups.col_07 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c7, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 256 and cast( group_id as int ) < 288 then eba_org_bit_groups.col_08 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c8, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 288 and cast( group_id as int ) < 320 then eba_org_bit_groups.col_09 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c9, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 320 and cast( group_id as int ) < 352 then eba_org_bit_groups.col_10 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c10," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 352 and cast( group_id as int ) < 384 then eba_org_bit_groups.col_11 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c11," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 384 and cast( group_id as int ) < 416 then eba_org_bit_groups.col_12 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c12," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 416 and cast( group_id as int ) < 448 then eba_org_bit_groups.col_13 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c13," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 448 and cast( group_id as int ) < 480 then eba_org_bit_groups.col_14 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c14," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 480 and cast( group_id as int ) < 512 then eba_org_bit_groups.col_15 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c15," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 512 and cast( group_id as int ) < 544 then eba_org_bit_groups.col_16 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c16," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 544 and cast( group_id as int ) < 576 then eba_org_bit_groups.col_17 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c17," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 576 and cast( group_id as int ) < 608 then eba_org_bit_groups.col_18 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c18," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 608 and cast( group_id as int ) < 640 then eba_org_bit_groups.col_19 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c19," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 672 then eba_org_bit_groups.col_20 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c20," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 672 and cast( group_id as int ) < 704 then eba_org_bit_groups.col_21 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c21," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 704 and cast( group_id as int ) < 736 then eba_org_bit_groups.col_22 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c22," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 736 and cast( group_id as int ) < 768 then eba_org_bit_groups.col_23 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c23," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 768 and cast( group_id as int ) < 800 then eba_org_bit_groups.col_24 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c24," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 800 and cast( group_id as int ) < 832 then eba_org_bit_groups.col_25 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c25," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 832 and cast( group_id as int ) < 864 then eba_org_bit_groups.col_26 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c26," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 864 and cast( group_id as int ) < 896 then eba_org_bit_groups.col_27 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c27," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 896 and cast( group_id as int ) < 352 then eba_org_bit_groups.col_28 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c28," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 928 then eba_org_bit_groups.col_29 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c29," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 928 and cast( group_id as int ) < 960 then eba_org_bit_groups.col_30 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c30," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 960 and cast( group_id as int ) < 992 then eba_org_bit_groups.col_31 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c31," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 992 and cast( group_id as int ) < 1024 then eba_org_bit_groups.col_32 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c32," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1024 and cast( group_id as int ) < 1056 then eba_org_bit_groups.col_33 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c33," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1056 and cast( group_id as int ) < 1088 then eba_org_bit_groups.col_34 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c34," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1088 and cast( group_id as int ) < 1120 then eba_org_bit_groups.col_35 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c35," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1120 and cast( group_id as int ) < 1152 then eba_org_bit_groups.col_36 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c36," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1152 and cast( group_id as int ) < 1184 then eba_org_bit_groups.col_37 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c37," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1184 and cast( group_id as int ) < 1216 then eba_org_bit_groups.col_38 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c38," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1216 and cast( group_id as int ) < 1248 then eba_org_bit_groups.col_39 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c39," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1248 and cast( group_id as int ) < 1280 then eba_org_bit_groups.col_40 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c40" + cCommon.CR();
            sql.Text = sql.Text + "       from eba_org_bit_groups, eba_organizationschema" + cCommon.CR();
            sql.Text = sql.Text + "       where " + cCommon.CR();
            sql.Text = sql.Text + "           eba_organizationschema.unit_type = 4" + cCommon.CR();
            sql.Text = sql.Text + "       ) as foo " + cCommon.CR();
            sql.Text = sql.Text + "       Inner Join eba_organizationschema On foo.id = eba_organizationschema.id " + cCommon.CR();
            sql.Text = sql.Text + "   where(c0 <> 0 Or c1 <> 0 Or c2 <> 0 Or c3 <> 0 Or c4 <> 0 Or c5 <> 0 Or c6 <> 0 Or c7 <> 0 Or c8 <> 0 Or c9 <> 0 Or c10 <> 0 Or c11 <> 0 Or c12 <> 0 Or c13 <> 0 Or c14 <> 0 Or c15 <> 0 Or c16 <> 0 Or c17 <> 0 Or c18 <> 0 Or c19 <> 0 Or c20 <> 0" + cCommon.CR();
            sql.Text = sql.Text + "         Or c21 <> 0 Or c22 <> 0 Or c23 <> 0 Or c24 <> 0 Or c25 <> 0 Or c26 <> 0 Or c27 <> 0 Or c28 <> 0 Or c29 <> 0 Or c30 <> 0 Or c31 <> 0 Or c32 <> 0 Or c33 <> 0 Or c34 <> 0 Or c35 <> 0 Or c36 <> 0 Or c37 <> 0 Or c38 <> 0 Or c39 <> 0 Or c40 <> 0)" + cCommon.CR();
            sql.Text = sql.Text + ")" + cCommon.CR();
            sql.Text = sql.Text + "   UNION" + cCommon.CR();
            sql.Text = sql.Text + "(select " + cCommon.CR();
            sql.Text = sql.Text + "       foo.docid," + cCommon.CR();
            sql.Text = sql.Text + "       foo.id, " + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.caption, " + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.group_id " + cCommon.CR();
            sql.Text = sql.Text + "   from " + cCommon.CR();
            sql.Text = sql.Text + "       ( " + cCommon.CR();
            sql.Text = sql.Text + "       select " + cCommon.CR();
            sql.Text = sql.Text + "            eba_org_archive_bit_groups.id as docid," + cCommon.CR();
            sql.Text = sql.Text + "            eba_organizationschema.id as id, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1 and cast( group_id as int ) < 32 then eba_org_archive_bit_groups.col_00 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c0,   " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 32 and cast( group_id as int ) < 64 then eba_org_archive_bit_groups.col_01 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c1,  " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 64 and cast( group_id as int ) < 96 then eba_org_archive_bit_groups.col_02 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c2,  " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 96 and cast( group_id as int ) < 128 then eba_org_archive_bit_groups.col_03 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c3, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 128 and cast( group_id as int ) < 160 then eba_org_archive_bit_groups.col_04 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c4," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 160 and cast( group_id as int ) < 192 then eba_org_archive_bit_groups.col_05 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c5," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 192 and cast( group_id as int ) < 224 then eba_org_archive_bit_groups.col_06 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c6," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 224 and cast( group_id as int ) < 256 then eba_org_archive_bit_groups.col_07 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c7," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 256 and cast( group_id as int ) < 288 then eba_org_archive_bit_groups.col_08 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c8, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 288 and cast( group_id as int ) < 320 then eba_org_archive_bit_groups.col_09 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c9, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 320 and cast( group_id as int ) < 352 then eba_org_archive_bit_groups.col_10 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c10," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 352 and cast( group_id as int ) < 384 then eba_org_archive_bit_groups.col_11 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c11," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 384 and cast( group_id as int ) < 416 then eba_org_archive_bit_groups.col_12 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c12," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 416 and cast( group_id as int ) < 448 then eba_org_archive_bit_groups.col_13 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c13," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 448 and cast( group_id as int ) < 480 then eba_org_archive_bit_groups.col_14 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c14," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 480 and cast( group_id as int ) < 512 then eba_org_archive_bit_groups.col_15 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c15," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 512 and cast( group_id as int ) < 544 then eba_org_archive_bit_groups.col_16 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c16," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 544 and cast( group_id as int ) < 576 then eba_org_archive_bit_groups.col_17 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c17," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 576 and cast( group_id as int ) < 608 then eba_org_archive_bit_groups.col_18 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c18," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 608 and cast( group_id as int ) < 640 then eba_org_archive_bit_groups.col_19 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c19," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 672 then eba_org_archive_bit_groups.col_20 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c20," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 672 and cast( group_id as int ) < 704 then eba_org_archive_bit_groups.col_21 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c21," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 704 and cast( group_id as int ) < 736 then eba_org_archive_bit_groups.col_22 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c22," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 736 and cast( group_id as int ) < 768 then eba_org_archive_bit_groups.col_23 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c23," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 768 and cast( group_id as int ) < 800 then eba_org_archive_bit_groups.col_24 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c24," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 800 and cast( group_id as int ) < 832 then eba_org_archive_bit_groups.col_25 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c25," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 832 and cast( group_id as int ) < 864 then eba_org_archive_bit_groups.col_26 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c26," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 864 and cast( group_id as int ) < 896 then eba_org_archive_bit_groups.col_27 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c27," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 896 and cast( group_id as int ) < 352 then eba_org_archive_bit_groups.col_28 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c28," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 928 then eba_org_archive_bit_groups.col_29 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c29," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 928 and cast( group_id as int ) < 960 then eba_org_archive_bit_groups.col_30 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c30," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 960 and cast( group_id as int ) < 992 then eba_org_archive_bit_groups.col_31 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c31," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 992 and cast( group_id as int ) < 1024 then eba_org_archive_bit_groups.col_32 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c32, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1024 and cast( group_id as int ) < 1056 then eba_org_archive_bit_groups.col_33 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c33," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1056 and cast( group_id as int ) < 1088 then eba_org_archive_bit_groups.col_34 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c34," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1088 and cast( group_id as int ) < 1120 then eba_org_archive_bit_groups.col_35 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c35," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1120 and cast( group_id as int ) < 1152 then eba_org_archive_bit_groups.col_36 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c36," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1152 and cast( group_id as int ) < 1184 then eba_org_archive_bit_groups.col_37 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c37," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1184 and cast( group_id as int ) < 1216 then eba_org_archive_bit_groups.col_38 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c38," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1216 and cast( group_id as int ) < 1248 then eba_org_archive_bit_groups.col_39 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c39," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1248 and cast( group_id as int ) < 1280 then eba_org_archive_bit_groups.col_40 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c40" + cCommon.CR();
            sql.Text = sql.Text + "       from eba_org_archive_bit_groups, eba_organizationschema" + cCommon.CR();
            sql.Text = sql.Text + "       where " + cCommon.CR();
            sql.Text = sql.Text + "           eba_organizationschema.unit_type = 4" + cCommon.CR();
            sql.Text = sql.Text + "       ) as foo " + cCommon.CR();
            sql.Text = sql.Text + "       Inner Join eba_organizationschema On foo.id = eba_organizationschema.id " + cCommon.CR();
            sql.Text = sql.Text + "   where(c0 <> 0 Or c1 <> 0 Or c2 <> 0 Or c3 <> 0 Or c4 <> 0 Or c5 <> 0 Or c6 <> 0 Or c7 <> 0 Or c8 <> 0 Or c9 <> 0 Or c10 <> 0 Or c11 <> 0 Or c12 <> 0 Or c13 <> 0 Or c14 <> 0 Or c15 <> 0 Or c16 <> 0 Or c17 <> 0 Or c18 <> 0 Or c19 <> 0 Or c20 <> 0" + cCommon.CR();
            sql.Text = sql.Text + "         Or c21 <> 0 Or c22 <> 0 Or c23 <> 0 Or c24 <> 0 Or c25 <> 0 Or c26 <> 0 Or c27 <> 0 Or c28 <> 0 Or c29 <> 0 Or c30 <> 0 Or c31 <> 0 Or c32 <> 0 Or c33 <> 0 Or c34 <> 0 Or c35 <> 0 Or c36 <> 0 Or c37 <> 0 Or c38 <> 0 Or c39 <> 0 Or c40 <> 0)" + cCommon.CR();
            sql.Text = sql.Text + ")) src" + cCommon.CR();
            sql.Text = sql.Text + "left join eba_users_ex on src.group_id = eba_users_ex.group_id)" + cCommon.CR();
            sql.Text = sql.Text + "UNION ALL" + cCommon.CR();
            sql.Text = sql.Text + "(select docid,'', src.group_id, eba_users_ex.user_id, eba_users_ex.extern_user_id" + cCommon.CR();
            sql.Text = sql.Text + "from " + cCommon.CR();
            //-- UPORABNIKI, KI IMAJO DOKUMENT V PISARNI (lahko da ga še niso odprli -> to pomeni da še nimajo neposredne pravice)
            sql.Text = sql.Text + "(select " + cCommon.CR();
            sql.Text = sql.Text + "       foo.docid," + cCommon.CR();
            sql.Text = sql.Text + "       foo.id, " + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.caption," + cCommon.CR();
            sql.Text = sql.Text + "       eba_organizationschema.group_id " + cCommon.CR();
            sql.Text = sql.Text + "   from " + cCommon.CR();
            sql.Text = sql.Text + "       ( " + cCommon.CR();
            sql.Text = sql.Text + "       select " + cCommon.CR();
            sql.Text = sql.Text + "            eba_bit_doc_users.id as docid," + cCommon.CR();
            sql.Text = sql.Text + "            eba_organizationschema.id as id, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1 and cast( group_id as int ) < 32 then eba_bit_doc_users.col_00 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c0,   " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 32 and cast( group_id as int ) < 64 then eba_bit_doc_users.col_01 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c1,  " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 64 and cast( group_id as int ) < 96 then eba_bit_doc_users.col_02 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c2,  " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 96 and cast( group_id as int ) < 128 then eba_bit_doc_users.col_03 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c3, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 128 and cast( group_id as int ) < 160 then eba_bit_doc_users.col_04 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c4," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 160 and cast( group_id as int ) < 192 then eba_bit_doc_users.col_05 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c5," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 192 and cast( group_id as int ) < 224 then eba_bit_doc_users.col_06 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c6," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 224 and cast( group_id as int ) < 256 then eba_bit_doc_users.col_07 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c7," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 256 and cast( group_id as int ) < 288 then eba_bit_doc_users.col_08 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c8, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 288 and cast( group_id as int ) < 320 then eba_bit_doc_users.col_09 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c9, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 320 and cast( group_id as int ) < 352 then eba_bit_doc_users.col_10 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c10," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 352 and cast( group_id as int ) < 384 then eba_bit_doc_users.col_11 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c11," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 384 and cast( group_id as int ) < 416 then eba_bit_doc_users.col_12 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c12," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 416 and cast( group_id as int ) < 448 then eba_bit_doc_users.col_13 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c13," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 448 and cast( group_id as int ) < 480 then eba_bit_doc_users.col_14 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c14," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 480 and cast( group_id as int ) < 512 then eba_bit_doc_users.col_15 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c15," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 512 and cast( group_id as int ) < 544 then eba_bit_doc_users.col_16 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c16," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 544 and cast( group_id as int ) < 576 then eba_bit_doc_users.col_17 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c17," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 576 and cast( group_id as int ) < 608 then eba_bit_doc_users.col_18 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c18," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 608 and cast( group_id as int ) < 640 then eba_bit_doc_users.col_19 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c19," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 672 then eba_bit_doc_users.col_20 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c20," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 672 and cast( group_id as int ) < 704 then eba_bit_doc_users.col_21 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c21," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 704 and cast( group_id as int ) < 736 then eba_bit_doc_users.col_22 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c22," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 736 and cast( group_id as int ) < 768 then eba_bit_doc_users.col_23 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c23," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 768 and cast( group_id as int ) < 800 then eba_bit_doc_users.col_24 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c24," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 800 and cast( group_id as int ) < 832 then eba_bit_doc_users.col_25 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c25," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 832 and cast( group_id as int ) < 864 then eba_bit_doc_users.col_26 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c26," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 864 and cast( group_id as int ) < 896 then eba_bit_doc_users.col_27 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c27," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 896 and cast( group_id as int ) < 352 then eba_bit_doc_users.col_28 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c28," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 640 and cast( group_id as int ) < 928 then eba_bit_doc_users.col_29 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c29," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 928 and cast( group_id as int ) < 960 then eba_bit_doc_users.col_30 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c30," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 960 and cast( group_id as int ) < 992 then eba_bit_doc_users.col_31 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c31," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 992 and cast( group_id as int ) < 1024 then eba_bit_doc_users.col_32 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c32, " + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1024 and cast( group_id as int ) < 1056 then eba_bit_doc_users.col_33 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c33," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1056 and cast( group_id as int ) < 1088 then eba_bit_doc_users.col_34 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c34," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1088 and cast( group_id as int ) < 1120 then eba_bit_doc_users.col_35 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c35," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1120 and cast( group_id as int ) < 1152 then eba_bit_doc_users.col_36 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c36," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1152 and cast( group_id as int ) < 1184 then eba_bit_doc_users.col_37 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c37," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1184 and cast( group_id as int ) < 1216 then eba_bit_doc_users.col_38 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c38," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1216 and cast( group_id as int ) < 1248 then eba_bit_doc_users.col_39 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c39," + cCommon.CR();
            sql.Text = sql.Text + "			case when cast( group_id as int ) >= 1248 and cast( group_id as int ) < 1280 then eba_bit_doc_users.col_40 & cast(POWER(2.0, ( cast( group_id as int) % 32 )) as bigint) else 0 end as c40" + cCommon.CR();
            sql.Text = sql.Text + "       from eba_bit_doc_users, eba_organizationschema" + cCommon.CR();
            sql.Text = sql.Text + "       where" + cCommon.CR();
            sql.Text = sql.Text + "            eba_organizationschema.unit_type = 4" + cCommon.CR();
            sql.Text = sql.Text + "       ) as foo " + cCommon.CR();
            sql.Text = sql.Text + "       Inner Join eba_organizationschema On foo.id = eba_organizationschema.id " + cCommon.CR();
            sql.Text = sql.Text + "   where(c0 <> 0 Or c1 <> 0 Or c2 <> 0 Or c3 <> 0 Or c4 <> 0 Or c5 <> 0 Or c6 <> 0 Or c7 <> 0 Or c8 <> 0 Or c9 <> 0 Or c10 <> 0 Or c11 <> 0 Or c12 <> 0 Or c13 <> 0 Or c14 <> 0 Or c15 <> 0 Or c16 <> 0 Or c17 <> 0 Or c18 <> 0 Or c19 <> 0 Or c20 <> 0" + cCommon.CR();
            sql.Text = sql.Text + "         Or c21 <> 0 Or c22 <> 0 Or c23 <> 0 Or c24 <> 0 Or c25 <> 0 Or c26 <> 0 Or c27 <> 0 Or c28 <> 0 Or c29 <> 0 Or c30 <> 0 Or c31 <> 0 Or c32 <> 0 Or c33 <> 0 Or c34 <> 0 Or c35 <> 0 Or c36 <> 0 Or c37 <> 0 Or c38 <> 0 Or c39 <> 0 Or c40 <> 0)" + cCommon.CR();
            sql.Text = sql.Text + ") src" + cCommon.CR();
            sql.Text = sql.Text + "left join eba_users_ex on src.group_id = eba_users_ex.group_id" + cCommon.CR();
            sql.Text = sql.Text + "where extern_user_id = '" + _UserEVI + "')" + cCommon.CR();
            sql.Text = sql.Text + ") as eba_doc_permissions " + cCommon.CR();
            sql.Text = sql.Text + "on eba_doc.id = eba_doc_permissions.docid" + cCommon.CR();
            if (_ListOfPartnerID.Count == 1)
            {
                sql.Text = sql.Text + "where eba_doc.extern_id = '" + _ListOfPartnerID[0] + "'" + cCommon.CR();
            }
            else
            {
                String SeznamPartnerjev = "";
                for (Int32 i = 0; i < _ListOfPartnerID.Count; i++)
                {
                    if (SeznamPartnerjev != "")
                    {
                        SeznamPartnerjev = SeznamPartnerjev + ", ";
                    }
                    SeznamPartnerjev = SeznamPartnerjev + "'" + _ListOfPartnerID[i].ToString() + "'";
                }
                sql.Text = sql.Text + "where eba_doc.extern_id in (" + SeznamPartnerjev + ")" + cCommon.CR();
            }
            sql.Text = sql.Text + ") X" + cCommon.CR();
            sql.Text = sql.Text + "order by coalesce(received_date, sent_date)" + cCommon.CR();

            dbReader = sql.Query_Read("cDS_Dokumenti.Get_MountedDevices", ref fr);
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
                        DS_Dokument = Init_dcDS_Dokument();
                        DS_Dokument.DokumentID = sql.GetField_String(ref dbReader, "id").Trim();
                        DS_Dokument.EAD = sql.GetField_String(ref dbReader, "EAD").Trim();
                        DS_Dokument.Predmet = sql.GetField_String(ref dbReader, "Predmet").Trim();
                        DS_Dokument.TipDokumenta = sql.GetField_String(ref dbReader, "Tip_dokumenta").Trim();
                        DS_Dokument.Smer = sql.GetField_String(ref dbReader, "Smer").Trim();
                        DS_Dokument.DatumSprejema = sql.GetField_DateTimeNull(ref dbReader, "received_date");
                        DS_Dokument.DatumOddaje = sql.GetField_DateTimeNull(ref dbReader, "sent_date");
                        DS_Dokument.PartnerNaziv = sql.GetField_String(ref dbReader, "PP_naziv").Trim();
                        DS_Dokument.SkrbnikDokumenta = sql.GetField_String(ref dbReader, "Skrbnik").Trim();
                        _Dokumenti.Add(DS_Dokument);
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
            return fr;
        }


        public dcDS_Dokument Init_dcDS_Dokument()
        {
            dcDS_Dokument DS_Dokument = new dcDS_Dokument();
            DS_Dokument.DokumentID = "";
            DS_Dokument.EAD = "";
            DS_Dokument.Predmet = "";
            DS_Dokument.TipDokumenta = "";
            DS_Dokument.Smer = "";
            DS_Dokument.DatumSprejema = null;
            DS_Dokument.DatumOddaje = null;
            DS_Dokument.PartnerNaziv = "";
            DS_Dokument.SkrbnikDokumenta = "";
            return DS_Dokument;
        }
    }
}