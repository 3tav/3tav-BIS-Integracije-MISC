using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Net;


namespace InfoteamClientLib
{
    public class InfoteamService
    {
        private string _baseUrl;
        private string _username;
        private string _password;
        private string _connString;
        public string ConnString { get { return _connString; } }

        private string _storedProcXML;
        private string _storedProcWarehouseXML;
        private string _storedProcRemoteXML;
        private string _exportId;
        private string _exportArgs;
        private string _xmlPath;
        private string _fileDumpPath;
        private int _uploadSize;
        private int _clientTimeout = 0;
        private string _useConnString;
        private int _userId = -99; // default za job / brez uporabnika

        public void Init(string server, string database, string user, string pass, string exportId, string exportArgs, string xmlPath)
        {
            _baseUrl = ConfigurationManager.AppSettings["InfoteamUrl"].ToString();
            _username = ConfigurationManager.AppSettings["InfoteamUsername"].ToString();
            _password = ConfigurationManager.AppSettings["InfoteamPassword"].ToString();
            _storedProcXML = ConfigurationManager.AppSettings["StoredProcXML"].ToString();
            _storedProcWarehouseXML = ConfigurationManager.AppSettings["StoredProcWarehouseXML"].ToString();
            _storedProcRemoteXML = ConfigurationManager.AppSettings["StoredProcRemoteXML"].ToString();

            
            _fileDumpPath = ConfigurationManager.AppSettings["FileDumpPath"].ToString();
            _uploadSize = Convert.ToInt32(ConfigurationManager.AppSettings["UploadSize"]);
            _useConnString = ConfigurationManager.AppSettings["UseConnString"].ToString();

            try
            {
                int clientTimeout = 0;
                if (int.TryParse(ConfigurationManager.AppSettings["ClientTimeout"].ToString(), out clientTimeout))
                {
                    if (clientTimeout > 0)
                        _clientTimeout = clientTimeout;
                }
            }
            catch (Exception ex)
            { 
            
            }            

            if (_useConnString == "Yes")
            {
                _connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            }
            else
            {
                _connString = GetConnectionString(server, database, user, pass);
            }
            
            _exportId = exportId;
            _exportArgs = exportArgs;
            _xmlPath = xmlPath;
        }

        public void DispatchMethod(string method, string args)
        {
            try
            {
                switch (method)
                {
                    case Methods.PostMeterReadData:
                        PostMeterReadData();
                        break;
                    case Methods.PostMeterChangeData:
                        PostMeterChangeData(args);
                        break;
                    case Methods.GetMeterReadData:
                        GetMeterReadData(args);
                        break;
                    case Methods.PostWarehouseData:                        
                        PostWarehouseData();
                        break;
                    case Methods.PostMeterReadDataRemote:
                        PostMeterReadDataRemote();
                        break;
                    case Methods.GetMeterReadDataRemote:
                        GetMeterReadDataRemote(args);
                        break;
                    default:
                        throw new Exception("Method not implemented!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} Error: {1}", method, ex.Message));
            }
        }

        private string GetConnectionString(string server, string database, string user, string pass)
        {
            string trustedConnection = "No";
            if (string.IsNullOrEmpty(user))
            {
                trustedConnection = "Yes";
            }

            if (trustedConnection == "Yes")
            {
                return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout=600;", server, database);
            }
            else
            {
                return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};Trusted_Connection={4};Connection Timeout=600;", server, database, user, pass, trustedConnection);                
            }                        
        }

        public void PostMeterReadData()
        {
            int obdobjeOd = 0, obdobjeDo = 0;
            int userId = _userId;
          
            try
            {
                var parameters = _exportArgs.Split(';');
                Int32.TryParse(parameters[0], out obdobjeOd);
                Int32.TryParse(parameters[1], out obdobjeDo);
                Int32.TryParse(parameters[2], out userId);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri branju parametrov: {0}", ex.Message));
            }

            PostMeterReadData(obdobjeOd, obdobjeDo, userId);
                    
        }

        

        public void PostMeterReadData(int obdobjeOd, int obdobjeDo, int userId)
        {
            try
            {
                // priprava
                var count = GetNaborCount(userId);// PripraviNabor(obdobjeOd, obdobjeDo);

                var iterations = (count / _uploadSize) + 1;
                for (var i = 0; i < iterations; i++)
                {
                    SetTransferStatus(Korak.Init, userId);
                    var xml = GetXml(obdobjeOd, obdobjeDo, (int)Korak.Xml, userId);
                    FileDump("PostMeterReadData", xml);
                    PostMeterReadData(xml);
                    SetTransferStatus(Korak.Close, userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri klicu servisa: {0}", ex.Message));
            }
        }

        public void PostMeterChangeData(string args)
        {
            int obdobjeOd = 0, obdobjeDo = 0;
            int userId = _userId;

            try
            {
                var parameters = args.Split(';');
                Int32.TryParse(parameters[0], out obdobjeOd);
                Int32.TryParse(parameters[1], out obdobjeDo);
                Int32.TryParse(parameters[2], out userId);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri branju parametrov: {0}", ex.Message));
            }


            PostMeterChangeData(obdobjeOd, obdobjeDo, userId);
        }

        public void PostMeterChangeData(int obdobjeOd, int obdobjeDo, int userId)
        {
            try
            {
                // priprava
                var count = GetNaborCount("CHANGE", userId);// PripraviNabor(obdobjeOd, obdobjeDo);

                var iterations = (count / _uploadSize) + 1;
                for (var i = 0; i < iterations; i++)
                {
                    SetTransferStatus(Korak.Init, userId);
                    var xml = GetXml(obdobjeOd, obdobjeDo, (int)Korak.XmlMenjave, userId);
                    FileDump("PostMeterChangeData", xml);
                    PostMeterReadData(xml);
                    SetTransferStatus(Korak.Close, userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri klicu servisa: {0}", ex.Message));
            }
        }

        public void PostWarehouseData()
        {
            try
            {                                
                    var xml = GetWarehouseXml();
                    FileDump("PostWarehouseData", xml);
                    PostWarehouseData(xml);                                    
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri klicu servisa: {0}", ex.Message));
            }
        }

        private RestClient GetClient()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(_baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(_username, _password);
            if (_clientTimeout > 0)
            {
                client.Timeout = _clientTimeout;
            }

            //client.Timeout = 0;
            return client;
        }

        private void PostMeterReadData(string readData)
        {
            var client = GetClient();
            //var request = new RestRequest("/MeterReadData/api/InfotimXML", Method.POST);
            var request = new RestRequest("/api/InfotimXML", Method.POST);

            request.RequestFormat = RestSharp.DataFormat.Xml;
            //request.AddHeader("Authorization", string.Format("Basic Auth {0}:{1}", _username, _password));

            var authorization = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", _username, _password)));
            request.AddHeader("Authorization", authorization);

            request.AddHeader("Accept", "application/xml");
            request.AddParameter("text/xml", readData, ParameterType.RequestBody);
            //request.AddObject(readData);


            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            //client.Timeout = 0;
            var response = client.Execute(request);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
                throw new Exception(string.Format("Protocol error: {0}", response.ErrorMessage));

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(string.Format("Service error {0}: {1}", response.StatusCode.ToString(), response.StatusDescription));
            
        }


        private void PostMeterReadDataRemote(string readData)
        {
            var client = GetClient();
            //var request = new RestRequest("/MeterReadData/api/InfotimXML", Method.POST);
            var request = new RestRequest("/api/InfotimXMLRemote", Method.POST);

            request.RequestFormat = RestSharp.DataFormat.Xml;
            //request.AddHeader("Authorization", string.Format("Basic Auth {0}:{1}", _username, _password));

            var authorization = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", _username, _password)));
            request.AddHeader("Authorization", authorization);

            request.AddHeader("Accept", "application/xml");
            request.AddParameter("text/xml", readData, ParameterType.RequestBody);
            //request.AddObject(readData);


            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var response = client.Execute(request);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
                throw new Exception(string.Format("Protocol error: {0}", response.ErrorMessage));

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(string.Format("Service error {0}: {1}", response.StatusCode.ToString(), response.StatusDescription));

        }

        public void PostMeterReadDataRemote()
        {
            int obdobjeOd = 0, obdobjeDo = 0;
            int userId = _userId;
            try
            {
                var parameters = _exportArgs.Split(';');
                Int32.TryParse(parameters[0], out obdobjeOd);
                Int32.TryParse(parameters[1], out obdobjeDo);
                Int32.TryParse(parameters[2], out userId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri branju parametrov: {0}", ex.Message));
            }

            PostMeterReadDataRemote(obdobjeOd, obdobjeDo, userId);

        }
        public void PostMeterReadDataRemote(int obdobjeOd, int obdobjeDo, int userId)
        {
            try
            {
                // priprava
                var count = CreateNaborRemote(userId);// PripraviNabor(obdobjeOd, obdobjeDo);
                
                var iterations = (count / _uploadSize) + 1;
                for (var i = 0; i < iterations; i++)
                {
                    SetTransferStatus(Korak.Init, _storedProcRemoteXML, userId);
                    var xml = GetRemoteXml(obdobjeOd, obdobjeDo, userId);
                    FileDump("PostMeterReadDataRemote", xml);
                    PostMeterReadDataRemote(xml);
                    SetTransferStatus(Korak.Close, _storedProcRemoteXML, userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri klicu servisa: {0}", ex.Message));
            }
        }

        public int GetMeterReadData(string args)
        {
            var a = args.Split(';');
            var userId = _userId;

            if (a.Length == 4)
            {
                return GetMeterReadData(a[0], a[1], a[2], a[3], _userId);
            }

            if (a.Length == 5)
            {
                int.TryParse(a[4], out userId);

                return GetMeterReadData(a[0], a[1], a[2], a[3], userId);
            }

            return -1;
        }


        public int GetMeterReadDataRemote(string args)
        {
            var a = args.Split(';');

            if (a.Length == 3) {
                return GetMeterReadDataRemote(a[0], a[1], a[2]);
            }

            return -1;
            //if (a.Length == 4) {
            //    return GetMeterReadDataRemote(a[0], a[1], a[2], a[3]);
            //}
            
        }

        public int GetMeterReadData(string period, string all, string dateFrom, string dateTo, int userId)
        {
            
            var client = GetClient();
            //var method = "/MeterReadData/api/InfotimXML";
            var method = "/api/InfotimXML";
            var request = new RestRequest(method, Method.GET);

            //var authorization = string.Format("Basic Auth {0}:{1}", _username, _password);

            var authorization = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", _username, _password)));

            request.RequestFormat = RestSharp.DataFormat.Xml;
            request.AddHeader("Authorization", authorization);
            request.AddHeader("Accept", "text/xml");
            request.AddQueryParameter("Period", period);

            if (period == "CHANGE")
                request.AddQueryParameter("Exported", "0");

            //request.AddQueryParameter("All", all);

            //request.AddQueryParameter("DateFrom", dateFrom);
            //request.AddQueryParameter("DateTo", dateTo);

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


            var uri = client.BuildUri(request);

            var response = client.Execute(request);

          

            var requestLog = new StringBuilder();
            requestLog.AppendLine(string.Format("Method: {0}", _baseUrl + method));
            requestLog.AppendLine(string.Format("Uri: {0}", uri));
            requestLog.AppendLine(string.Format("Authorization: {0}", authorization));            
            requestLog.AppendLine(string.Format("Accept: {0}", "text/xml"));

            requestLog.AppendLine(string.Format("Period: {0}", period));
            //requestLog.AppendLine(string.Format("All: {0}", all));
            
            if (period == "CHANGE")
                requestLog.AppendLine(string.Format("Exported: {0}", 0));
            //requestLog.AppendLine(string.Format("DateFrom: {0}", dateFrom));
            //requestLog.AppendLine(string.Format("DateTo: {0}", dateTo));

            FileDump("Request", requestLog.ToString());


            if (!string.IsNullOrEmpty(response.ErrorMessage))
                throw new Exception(string.Format("Protocol error: {0}", response.ErrorMessage));

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(string.Format("{0}: {1}", response.StatusCode,  response.StatusDescription));


            var xml = response.Content;

            var filename = "GetMeterReadData";
            if (period == "CHANGE")
                filename = "GetMeterChangeReadData";

            FileDump(filename, xml);
       
            if (period == "CHANGE")
            {
                ImportMeterChangeData(xml, userId);
            }
            else
            {
                ImportMeterData(xml, userId);
            }
            


            return 1;          
        }

       

        public int GetMeterReadDataRemote(string all, string dateFrom, string dateTo)
        {

            var client = GetClient();
            //var method = "/MeterReadData/api/InfotimXML";
            var method = "/api/InfotimXMLRemote";
            var request = new RestRequest(method, Method.GET);

            //var authorization = string.Format("Basic Auth {0}:{1}", _username, _password);

            var authorization = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", _username, _password)));

            request.RequestFormat = RestSharp.DataFormat.Xml;
            request.AddHeader("Authorization", authorization);
            request.AddHeader("Accept", "text/xml");
            request.AddQueryParameter("All", all);
            request.AddQueryParameter("DateFrom", dateFrom);
            request.AddQueryParameter("DateTo", dateTo);

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;



            var response = client.Execute(request);


            var requestLog = new StringBuilder();
            requestLog.AppendLine(string.Format("Method: {0}", _baseUrl + method));
            requestLog.AppendLine(string.Format("Authorization: {0}", authorization));
            requestLog.AppendLine(string.Format("Accept: {0}", "text/xml"));

            //requestLog.AppendLine(string.Format("Period: {0}", period));
            requestLog.AppendLine(string.Format("All: {0}", all));
            requestLog.AppendLine(string.Format("DateFrom: {0}", dateFrom));
            requestLog.AppendLine(string.Format("DateTo: {0}", dateTo));

            FileDump("Request", requestLog.ToString());


            if (!string.IsNullOrEmpty(response.ErrorMessage))
                throw new Exception(string.Format("Protocol error: {0}", response.ErrorMessage));

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.StatusDescription));


            var xml = response.Content;

            var filename = "GetMeterReadDataRemote";
             
            FileDump(filename, xml);

            
            ImportMeterDataRemote(xml);
            
            return 1;
        }

        private void PostWarehouseData(string xml)
        {
            var client = GetClient();

            var request = new RestRequest("/api/Warehouse", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Xml;
            
            var authorization = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", _username, _password)));
            request.AddHeader("Authorization", authorization);
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("text/xml", xml, ParameterType.RequestBody);
            
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            var response = client.Execute(request);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
                throw new Exception(string.Format("Protocol error: {0}", response.ErrorMessage));

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(string.Format("Service error {0}: {1}", response.StatusCode.ToString(), response.StatusDescription));

        }

        public void ImportMeterData(string xml, int userId)
        {
            if (xml == null)
                return;

            var importId = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                //var trans = conn.BeginTransaction();

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = "select isnull(max(import_id), 0) + 1 from infotim_meterData";
                        importId = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                   // trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 10));
                }
 
                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = @"insert into infotim_meterData (import_id, vsebina_xml, vpis_uporabnik) values (@importId, @vsebinaXml, @userId)";
                        cmd.Parameters.AddWithValue("@importId", importId);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.Add(new SqlParameter("@vsebinaXml", SqlDbType.Xml)
                        {
                            Value = new SqlXml(XmlReader.Create(new StringReader(xml)))
                        });
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 20));
                }

                //trans.Commit();

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = "p_infotim_tIntegrationChecked_insert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@importId", importId);                        
                        cmd.Parameters.AddWithValue("@userId", userId);
                        

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw new Exception(string.Format("{0} {1}",ex.Message, 30));
                }                

                //trans.Commit();
            }
        }

        public void ImportMeterDataRemote(string xml)
        {
            if (xml == null)
                return;

            var importId = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                //var trans = conn.BeginTransaction();

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = "select isnull(max(import_id), 0) + 1 from infotim_meterData";
                        importId = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    // trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 10));
                }

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = @"insert into infotim_meterData (import_id, vsebina_xml) values (@importId, @vsebinaXml)";
                        cmd.Parameters.AddWithValue("@importId", importId);
                        cmd.Parameters.Add(new SqlParameter("@vsebinaXml", SqlDbType.Xml)
                        {
                            Value = new SqlXml(XmlReader.Create(new StringReader(xml)))
                        });
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 20));
                }

                //trans.Commit();

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = "p_infotim_tIntegrationChecked_insert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@importId", importId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 30));
                }

                //trans.Commit();
            }
        }

        public void ImportMeterChangeData(string xml, int userId)
        {
            if (xml == null)
                return;

            var importId = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                //var trans = conn.BeginTransaction();

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = "select isnull(max(import_id), 0) + 1 from infotim_meterData";
                        importId = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    // trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 10));
                }

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = @"insert into infotim_meterData (import_id, vsebina_xml, period, vpis_uporabnik) 
                                            values (@importId, @vsebinaXml, @period, @userId)";
                        cmd.Parameters.AddWithValue("@importId", importId);
                        cmd.Parameters.AddWithValue("@period", "CHANGE");
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.Add(new SqlParameter("@vsebinaXml", SqlDbType.Xml)
                        {
                            Value = new SqlXml(XmlReader.Create(new StringReader(xml)))
                        });

                       

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 20));
                }

                //trans.Commit();

                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.Transaction = trans;
                        cmd.CommandText = "p_infotim_tMeterChangeJunction_insert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@importId", importId);                        
                        cmd.Parameters.AddWithValue("@userId", userId);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw new Exception(string.Format("{0} {1}", ex.Message, 30));
                }

                //trans.Commit();
            }
        }

        private void ImportMeterData(MeterData meterData, int userId)
        {
            if (meterData.Meter == null)
                return;

            var importId = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {                   
                    cmd.CommandText = "select isnull(max(import_id), 0) + 1 from infotim_meterData";
                    importId = (int)cmd.ExecuteScalar();
                }
            }

            var table = GetDataTable(meterData, importId, userId);

            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();

                var trans = conn.BeginTransaction();

                try
                {
                    var bc = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, trans);
                    bc.DestinationTableName = "infotim_meterData";
                    bc.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(string.Format("Bulk Copy Error: {0}", ex.Message));                    
                }

                
                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;
                        cmd.CommandText = "p_infotim_tIntegrationChecked_insert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@import_id", importId);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(string.Format("Procedure Error: {0}", ex.Message));
                }
                
                trans.Commit();

            }
        }

        private int PripraviNabor(int obdobjeOd, int obdobjeDo, int userId)
        {           
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = "delete from infotim_nabor where vpis_uporabnik = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = _storedProcXML;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@obdobjeOd", obdobjeOd);
                    cmd.Parameters.AddWithValue("@obdobjeDo", obdobjeDo);
                    cmd.Parameters.AddWithValue("@korak", (int)Korak.Priprava);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    return cmd.ExecuteNonQuery();                    
                }
            }
        }

        private int GetNaborCount(int userId)
        { 
            var count = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = "select isnull(count(*), 0) from infotim_nabor where status = 0 and vpis_uporabnik = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    count = (Int32)cmd.ExecuteScalar();
                }              
            }

            return count;
        }

        private int CreateNaborRemote(int userId)
        {
            var count = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();


                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = _storedProcRemoteXML;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@obdobjeOd", 0);
                    cmd.Parameters.AddWithValue("@obdobjeDo", 0);
                    cmd.Parameters.AddWithValue("@korak", 0);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    return cmd.ExecuteNonQuery();
                }

                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = "select isnull(count(*), 0) from infotim_nabor where status = 0 and userId = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    count = (Int32)cmd.ExecuteScalar();
                }        
            }

            return count;
        }

        private int GetNaborCount(string period, int userId)
        {
            var count = 0;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = "select isnull(count(*), 0) from infotim_nabor where status = 0 and period = @period and userId = @userId";
                    cmd.Parameters.AddWithValue("@period", period);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    count = (Int32)cmd.ExecuteScalar();
                }
            }

            return count;
        }

        private int SetTransferStatus(Korak k, int userId)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
               

                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = _storedProcXML;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@obdobjeOd", 0);
                    cmd.Parameters.AddWithValue("@obdobjeDo", 0);
                    cmd.Parameters.AddWithValue("@korak", (int)k);
                    cmd.Parameters.AddWithValue("@userId", (int)k);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        private int SetTransferStatus(Korak k, string storedProcedure, int userId)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();


                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@obdobjeOd", 0);
                    cmd.Parameters.AddWithValue("@obdobjeDo", 0);
                    cmd.Parameters.AddWithValue("@korak", (int)k);                    
                    cmd.Parameters.AddWithValue("@userId", userId);
                    
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetXml(int obdobjeOd, int obdobjeDo, int korak, int userId)
        {

            string xml = string.Empty;
            /*            
            xml = File.ReadAllText("C:\\temp\\Infotim\\PostMeterReadData.xml");
            return xml;
            */
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = _storedProcXML;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@obdobjeOd", obdobjeOd);
                    cmd.Parameters.AddWithValue("@obdobjeDo", obdobjeDo);
                    cmd.Parameters.AddWithValue("@korak", korak);                    
                    cmd.Parameters.AddWithValue("@userId", userId);
                    
                    using (var rdr = cmd.ExecuteXmlReader())
                    {
                        if (rdr.Read())
                        {
                            xml = rdr.ReadOuterXml();
                        }
                    }
                }

                if (_xmlPath.Length > 0)
                {                    
                    WriteToFile(_xmlPath , xml);
                }

                return xml;
            }
        }

        private string GetRemoteXml(int obdobjeOd, int obdobjeDo, int userId)
        {

            string xml = string.Empty;
            
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // generiranje xml
                    cmd.CommandText = _storedProcRemoteXML;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@obdobjeOd", obdobjeOd);
                    cmd.Parameters.AddWithValue("@obdobjeDo", obdobjeDo);
                    cmd.Parameters.AddWithValue("@korak", (int)Korak.Xml);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var rdr = cmd.ExecuteXmlReader())
                    {
                        if (rdr.Read())
                        {
                            xml = rdr.ReadOuterXml();
                        }
                    }
                }
              
                return xml;
            }
        }



        private string GetWarehouseXml()
        {

            string xml = string.Empty;            
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {                    
                    cmd.CommandText = _storedProcWarehouseXML;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@korak", (int)Korak.Xml);
                    cmd.CommandTimeout = 0;
                                        
                    using (var rdr = cmd.ExecuteXmlReader())
                    {
                        if (rdr.Read())
                        {
                            xml = rdr.ReadOuterXml();
                        }
                    }
                }

                if (_xmlPath.Length > 0)
                {                    
                    WriteToFile(_xmlPath , xml);
                }

                return xml;
            }
        }

        

        protected void WriteToFile(string filePath, string xml)
        {
            try
            {
                string fileName = string.Format("infotim_{0}.xml", Environment.TickCount);
                filePath += fileName;
                if (File.Exists(filePath))
                    File.Delete(filePath);

                XmlDocument doc;
                doc = new XmlDocument();

                doc.LoadXml(xml);

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                settings.IndentChars = "  ";
                settings.NewLineChars = "\r\n";
                settings.NewLineHandling = NewLineHandling.Replace;
                using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                {
                    doc.Save(writer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Napaka pri pisanju v datoteko: {0}", ex.Message));
            }

        }

        private DataTable GetDataTable(MeterData meterData, int importId, int userId)
        {
            var table = GetDataTable();

            foreach (var m in meterData.Meter)
            {
                table.Rows.Add(CreateDataRow(table, m, importId, userId));
            }

            return table;
        }

        private DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            DataColumn column;

            column = table.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = table.Columns.Add();
            column.ColumnName = "import_id";
            column.DataType = typeof(int);

            column = table.Columns.Add();
            column.ColumnName = "AMRAlarms";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Active";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Address";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Barcode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "BillingID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Combined";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CoronisAddress";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CoronisInput";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CoronisInput1";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CoronisType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CoronisUnit";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CoronisUnit1";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CustomerEmail";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CustomerNumber";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Dimension";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "DistrictID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "GPSLat";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "GPSLatNew";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "GPSLon";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "GPSLonNew";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Images";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "IsChecked";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "LastMeterChangeDate";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Location";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCAdditionalData";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCBackflowProtectionID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCCoronisAddress";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCCoronisInput1";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCCoronisInput2";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCCoronisType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCCoronisUnit1";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCCoronisUnit2";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCDate";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCDiameter";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCManufacturerID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCMeterChanged";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCMeterStamp";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCMeterTypeID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCNewMeterReg1";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCNewMeterReg2";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCNote";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCOldMeterReg1";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCOldMeterReg2";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCRFID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCRFIDType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCRFUnitChanged";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCRemarkDescription";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCRemarkID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCSerialNo";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCSignature";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCSignatureDate";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MCUserID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MP";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MeterType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "NewBarcode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Note";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Owner";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Period";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RFID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RFIDKey";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RFIDType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "ReadingType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Reg1Average";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Reg1LastValue";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Reg1Value";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Reg2Average";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Reg2LastValue";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Reg2Value";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RegDate";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RegLastDate";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RemarkDescription";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "RemarkID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "SerialNo";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "SortOrder";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "TerminalID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "UserID";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "WorkOrderType";
            column.DataType = typeof(string);

            return table;
        }

        private DataRow CreateDataRow(DataTable table, MeterDataMeter m, int importId, int userId)
        {
            DataRow row = table.NewRow();
            row["import_id"] = importId;
            row["AMRAlarms"] = m.AMRAlarms;
            row["Active"] = m.Active;
            row["Address"] = m.Address;
            row["Barcode"] = m.Barcode;
            row["BillingID"] = m.BillingID;
            row["Combined"] = m.Combined;
            row["CoronisAddress"] = m.CoronisAddress;
            row["CoronisInput"] = m.CoronisInput;
            row["CoronisInput1"] = m.CoronisInput1;
            row["CoronisType"] = m.CoronisType;
            row["CoronisUnit"] = m.CoronisUnit;
            row["CoronisUnit1"] = m.CoronisUnit1;
            row["CustomerEmail"] = m.CustomerEmail;
            row["CustomerNumber"] = m.CustomerNumber;
            row["Dimension"] = m.Dimension;
            row["DistrictID"] = m.DistrictID;
            row["GPSLat"] = m.GPSLat;
            row["GPSLatNew"] = m.GPSLatNew;
            row["GPSLon"] = m.GPSLon;
            row["GPSLonNew"] = m.GPSLonNew;
            row["Images"] = m.Images;
            row["IsChecked"] = m.IsChecked;
            row["LastMeterChangeDate"] = m.LastMeterChangeDate;
            row["Location"] = m.Location;
            row["MCAdditionalData"] = m.MCAdditionalData;
            row["MCBackflowProtectionID"] = m.MCBackflowProtectionID;
            row["MCCoronisAddress"] = m.MCCoronisAddress;
            row["MCCoronisInput1"] = m.MCCoronisInput1;
            row["MCCoronisInput2"] = m.MCCoronisInput2;
            row["MCCoronisType"] = m.CoronisType;
            row["MCCoronisUnit1"] = m.CoronisUnit1;
            row["MCCoronisUnit2"] = m.MCCoronisUnit2;
            row["MCDate"] = m.MCDate;
            row["MCDiameter"] = m.MCDiameter;
            row["MCManufacturerID"] = m.MCManufacturerID;
            row["MCMeterChanged"] = m.MCMeterChanged;
            row["MCMeterStamp"] = m.MCMeterStamp;
            row["MCMeterTypeID"] = m.MCMeterTypeID;
            row["MCNewMeterReg1"] = m.MCNewMeterReg1;
            row["MCNewMeterReg2"] = m.MCNewMeterReg2;
            row["MCNote"] = m.MCNote;
            row["MCOldMeterReg1"] = m.MCOldMeterReg1;
            row["MCOldMeterReg2"] = m.MCOldMeterReg2;
            row["MCRFID"] = m.MCRFID;
            row["MCRFIDType"] = m.MCRFIDType;
            row["MCRFUnitChanged"] = m.MCRFUnitChanged;
            row["MCRemarkDescription"] = m.MCRemarkDescription;
            row["MCRemarkID"] = m.MCRemarkID;
            row["MCSerialNo"] = m.MCSerialNo;
            row["MCSignature"] = m.MCSignature;
            row["MCSignatureDate"] = m.MCSignatureDate;
            row["MCUserID"] = m.MCUserID;
            row["MP"] = m.MP;
            row["MeterType"] = m.MeterType;
            row["NewBarcode"] = m.NewBarcode;
            row["Note"] = m.Note;
            row["Owner"] = m.Owner;
            row["Period"] = m.Period;
            row["RFID"] = m.RFID;
            row["RFIDKey"] = m.RFIDKey;
            row["RFIDType"] = m.RFIDType;
            row["ReadingType"] = m.ReadingType;
            row["Reg1Average"] = m.Reg1Average;
            row["Reg1LastValue"] = m.Reg1LastValue;
            row["Reg1Value"] = m.Reg1Value;
            row["Reg2Average"] = m.Reg2Value;
            row["Reg2LastValue"] = m.Reg2LastValue;
            row["Reg2Value"] = m.Reg2Value;
            row["RegDate"] = m.RegDate;
            row["RegLastDate"] = m.RegLastDate;
            row["RemarkDescription"] = m.RemarkDescription;
            row["RemarkID"] = m.RemarkID;
            row["SerialNo"] = m.SerialNo;
            row["SortOrder"] = m.SortOrder;
            row["TerminalID"] = m.TerminalID;
            row["UserID"] = m.UserID;
            row["WorkOrderType"] = m.WorkOrderType;
            row["vpis_uporabnik"] = userId;
            return row;
        }

        private void FileDump(string method, string xml)
        {
            try
            {
                if (_fileDumpPath.Length > 0)
                {
                    if (Directory.Exists(_fileDumpPath))
                    {
                        var filename = string.Format("{0}{1}.xml", _fileDumpPath, string.Format("{0}_{1}", method, DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                        File.WriteAllText(filename, xml);
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }            
        }

        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
