using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.Xml;

using System.Configuration;


using KroClientLib.KroFaktureService;

namespace KroClientLib
{
    public class KroService
    {
        ServiceClient _svc = null;

        private string _connectionString;
        private string _storedProcXML;
        private string _storedProcXMLZbirnik;
        private string _baseUrl;
        private int _session;
        private int _minContentLength;

        public KroService()
        {
            
        }

        public void Init()
        {

            _svc = new ServiceClient();
            _baseUrl = ConfigurationManager.AppSettings["KroUrl"].ToString();
            _connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            _storedProcXML = ConfigurationManager.AppSettings["StoredProcedureXML"].ToString();
            _storedProcXMLZbirnik = ConfigurationManager.AppSettings["StoredProcedureXML_zbirnik"].ToString();
            _minContentLength = Convert.ToInt32(ConfigurationManager.AppSettings["MinContentLength"]);

            Random r = new Random();
            _session = r.Next(1, 10000);
        }

        public byte[] GetFakturaPdf(int tip, string xmlName, string xmlContent, string posiljatelj)
        {
           Vhod v = new Vhod();
            v.IdTip = tip;
            v.Ime_XML = xmlName;
            v.Vsebina_XML = xmlContent;
            v.Posiljatelj = posiljatelj;

            var ret = _svc.Vhodni_xml(v);

            if (ret.Stanje.StanjeMember < 0)
                throw new Exception(ret.Stanje.Opis);            

            var po = new Prevzeto();            
            po.Ime_PDF = xmlName;
            po.Posiljatelj = v.Posiljatelj;
            po.IdTip = v.IdTip;
            po.Odgovor = 1;
            _svc.Datoteka_Prevzeta(po);
            
            return ret.Datoteka_Byte;                        
        }

        public byte[] GetFakturaPdf(int tip, string xmlName, int idFakture, string posiljatelj)
        {

            var xml = string.Empty;
            var storedProcedure = _storedProcXML;
            switch (tip)
            { 
                case 10:
                    storedProcedure = _storedProcXMLZbirnik;
                    break;
            
            }

            //File.AppendAllText(_logFile, string.Format("[{0} : {1}] {2}{3}", DateTime.Now, _session, message, Environment.NewLine));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                conn.Open();            
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;


                    switch (tip)
                    {
                        case 10:
                            cmd.Parameters.AddWithValue("@ARG_ID_ZBIRNIK", idFakture);
                            cmd.Parameters.AddWithValue("@ARG_PAKET", DBNull.Value);
                            break;
                        default:
                            cmd.Parameters.AddWithValue("@STEVILKARACUNA", idFakture);                                                            
                            cmd.Parameters.AddWithValue("@PAKET", DBNull.Value);
                            break;
                    }
                                                            
                    using (XmlReader rdr = cmd.ExecuteXmlReader())
                    {
                        if (rdr.Read())
                        {
                            xml = rdr.ReadOuterXml();
                        }
                    }
                }                
            }



            if (string.IsNullOrEmpty(xml))
                throw new Exception("Generiranje XML ni uspelo!");

            var pdf = GetFakturaPdf(tip, xmlName, xml, posiljatelj);

            /*
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO epps_ws_log(url,method,
							                        request,response,filename,
							                        result, message, session)
                                            VALUES (@url, @method,
                                                   @request,@response,@filename, 
                                                   @result, @message, @session)";

                    cmd.Parameters.AddWithValue("@url", _baseUrl);
                    cmd.Parameters.AddWithValue("@method", "CreatePDF");
                    cmd.Parameters.AddWithValue("@request", idFakture);
                    cmd.Parameters.AddWithValue("@response", string.Empty);
                    cmd.Parameters.AddWithValue("@filename", xmlName);
                    cmd.Parameters.AddWithValue("@result", 1);
                    cmd.Parameters.AddWithValue("@message", "PDF OK");
                    cmd.Parameters.AddWithValue("@session", _session);
                    cmd.ExecuteNonQuery();
                }
            }
            */

            return pdf;            
        }
    }
}
