﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ZZIEStoritveLib.ZZIEStoritveProxy;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Compression;
using System.Data;
using System.Net.Http;
using OpenPop.Mime;
using System.Xml.Linq;

namespace ZZIEStoritveLib
{

    public class ZZIEStoritveService
    {                
        EStoritveService _svc;
        private string _endpointUrl;
        private string _connString;
        private string _filePath;
        private string _username;
        private string _password;
        private string _guid;
        private bool _useDbParm;
        private bool _deleteAfterTransfer;
        private string _tempFolderPath;

        private const string EnvelopeFileName = "envelope.xml";
        private const string ZipFileName = "package.zip";

        private const int StatusNapaka = -1;
        private const int StatusOk = 2;

        public void Init()
        {
            _svc = new ZZIEStoritveProxy.EStoritveService();
            _connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            _useDbParm = (ConfigurationManager.AppSettings["UseDbParm"] == "true" ? true : false);
            _deleteAfterTransfer = (ConfigurationManager.AppSettings["DeleteAfterTransfer"] == "true" ? true : false);
            _tempFolderPath = ConfigurationManager.AppSettings["TempFolderToWrite"];
            if (_useDbParm)
            {
                using (var conn = new SqlConnection(_connString))
                { 
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "p_zzi_get_parameters";
                        using (var rdr = cmd.ExecuteReader())
                        { 
                            if (rdr.Read())
                            {                             
                                _username = rdr.GetString(0);
                                _password = rdr.GetString(1);
                                _endpointUrl = rdr.GetString(2);
                                _filePath = rdr.GetString(3);                                
                            }                            
                        }
                    }
                }                
            }
            else
            {
                _endpointUrl = ConfigurationManager.AppSettings["ZZIEndpoint"];
                _username = ConfigurationManager.AppSettings["ZZIUsername"];
                _password = ConfigurationManager.AppSettings["ZZIPassword"];
                _filePath = ConfigurationManager.AppSettings["DocumentPath"];                             
            }
            _svc.Url = _endpointUrl;            
        }

        public void PosljiDokument(string guid, int id)
        {        
            var files = new List<string>();

            // Preberi paket iz baze
           
            var xmlPaket = GetXmlPaket(id);
            if (string.IsNullOrEmpty(xmlPaket.XmlContent))
                throw new Exception(string.Format("XML paket {0} ne vsebuje podatkov!", id));

            // Zapiši fajl + ovojnico na disk, ZIP
            var subdirPath = string.Format("{0}{1}", _filePath, id);
            if (Directory.Exists(subdirPath))
                Directory.Delete(subdirPath, true);

            Directory.CreateDirectory(subdirPath);

            var xmlPath = string.Format("{0}\\{1}", subdirPath, xmlPaket.ImeDatoteke);
            //var xmlPath = string.Format("{0}\\{1}.xml", subdirPath, xmlPaket.ImeDatoteke);
            File.WriteAllText(xmlPath, xmlPaket.XmlContent);

            // create envelope
            var envelopePath = string.Format("{0}\\{1}", subdirPath, EnvelopeFileName);
            string docType;
            if (string.IsNullOrEmpty(xmlPaket.DocType))
            {
                docType = "GIZDZP.OBRPODZP";
            }
            else
            {
                docType = xmlPaket.DocType;
            }
            File.WriteAllText(envelopePath, Serialize<envelope>(CreateEnvelope(xmlPaket.From, xmlPaket.To, Path.GetFileName(xmlPath), xmlPaket.Description, docType)));

            // zip package
            var zipFile = string.Format("{0}\\{1}", subdirPath, ZipFileName);
            files.Add(xmlPath);
            files.Add(envelopePath);
            CreateZip(zipFile, files);
            var fileBytes = File.ReadAllBytes(zipFile);

            // web service
            try
            {  
                // upload na ZZI
                var response = UploadFileEx(guid, fileBytes);

                // get ID 
                var portalId = GetPortalId(response);

                // status paketa OK
                UpdatePaketEnd(id, portalId, null, StatusOk);

                // clean up
                if (_deleteAfterTransfer)
                    Directory.Delete(subdirPath, true);
            }
            catch (Exception ex)
            {
                UpdatePaketEnd(id, null, ex.Message, StatusNapaka);
                throw new Exception(string.Format("Napaka pri klicu WS: {0}", ex.Message));
            }

           
        }
        public void EchoTest()
        {
            try
            {
                _guid = Login(_username, _password);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("WS Login: {0}", ex.Message));
            }
            string test = Echo(_guid);
            test.CompareTo("");
        }
        public void PosljiDokument(int id)
        {            
            try
            {
                _guid = Login(_username, _password);                
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("WS Login: {0} Adress: {1}", ex.Message,(string.IsNullOrEmpty(_endpointUrl) ? "null" :_endpointUrl)));
            }
            
            PosljiDokument(_guid, id);
        }
        public DobiDokumente_response DobiDokumente(string davcna_stevilka, DateTime datum_od, DateTime datum_do )
        {
            string xml = @"<FILTERS>
	                        <FILTER>
		                        <NAME>ACCEPTED</NAME>
		                        <OPERATOR>=</OPERATOR>
		                        <VALUE>FALSE</VALUE>
	                        </FILTER>
                            <FILTER>
		                        <NAME>INSERT_DATE</NAME>
		                        <OPERATOR>BETWEEN</OPERATOR>
		                        <VALUE>"+datum_od.ToString("yyyy-MM-dd") + @"</VALUE>
                                <VALUE2>" + datum_do.ToString("yyyy-MM-dd") + @"</VALUE2>
	                        </FILTER>

                        </FILTERS>	";
            try
            {
                _guid = Login(_username, _password);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("WS Login: {0}", ex.Message));
            }

            if (!davcna_stevilka.Contains("SI"))
                davcna_stevilka = "SI" + davcna_stevilka;



            string xml_str= GetDocumentList(_guid, davcna_stevilka, xml);
            XmlDocument xml_response = new XmlDocument();
            xml_response.LoadXml(xml_str);
            XmlNodeList xnList = xml_response.GetElementsByTagName("DOCUMENT");
            List<Dobi_dokumente_dokument_response> list = new List<Dobi_dokumente_dokument_response>();
            foreach (XmlNode element in xnList)
            {
                int id = -1;
                if (element["ID"] != null)
                    int.TryParse(element["ID"].InnerText, out id);
                string external_id = "";
                if (element["EXTERNAL_ID"] != null)
                    external_id = element["EXTERNAL_ID"].InnerText;

                string TITLE = "";
                if (element["TITLE"] != null)
                    TITLE = element["TITLE"].InnerText;
                string CREATION_TIME = "";
                if (element["CREATION_TIME"] != null)
                    CREATION_TIME = element["CREATION_TIME"].InnerText;
                string CREATION_LOCATION = "";
                if (element["CREATION_LOCATION"] != null)
                    CREATION_LOCATION = element["CREATION_LOCATION"].InnerText;
                string FILENAME = "";
                if (element["FILENAME"] != null)
                    FILENAME = element["FILENAME"].InnerText;
                string MIMETYPE = "";
                if (element["MIMETYPE"] != null)
                    MIMETYPE = element["MIMETYPE"].InnerText;
                string ORGANIZATION = "";
                if (element["ORGANIZATION"] != null)
                    ORGANIZATION = element["ORGANIZATION"].InnerText;
                string INSERT_DATE = "";
                if (element["INSERT_DATE"] != null)
                    INSERT_DATE = element["INSERT_DATE"].InnerText;
                string CLASSIFICATION_NAME = "";
                if (element["CLASSIFICATION_NAME"] != null)
                    CLASSIFICATION_NAME = element["CLASSIFICATION_NAME"].InnerText;
                string ACCOUNT_NAME = "";
                if (element["ACCOUNT_NAME"] != null)
                    ACCOUNT_NAME = element["ACCOUNT_NAME"].InnerText;
                string USR_ID = "";
                if (element["USR_ID"] != null)
                    USR_ID = element["USR_ID"].InnerText;
                string STATUS = "";
                if (element["STATUS"] != null)
                    STATUS = element["STATUS"].InnerText;
                string USR_ID_UPORABNIK = "";
                    if (element["USR_ID_UPORABNIK"] != null)
                    USR_ID_UPORABNIK = element["USR_ID_UPORABNIK"].InnerText;
                int SIZE = -1;
                if (element["SIZE"] != null)
                    int.TryParse(element["SIZE"].InnerText, out SIZE);
                string TYPE = "";
                if (element["TYPE"] != null)
                    TYPE = element["TYPE"].InnerText;
                int ACC_ID = -1;
                if (element["ACC_ID"] != null)
                    int.TryParse(element["ACC_ID"].InnerText, out ACC_ID);
                string VER_DESCRIPTION = "";
                if (element["VER_DESCRIPTION"] != null)
                    VER_DESCRIPTION = element["VER_DESCRIPTION"].InnerText;
                int DOC_ID = -1;
                if (element["DOC_ID"] != null)
                    int.TryParse(element["DOC_ID"].InnerText, out DOC_ID);
                string DATA_REFERENCE = "";
                if (element["DATA_REFERENCE"] != null)
                    DATA_REFERENCE = element["DATA_REFERENCE"].InnerText;
                list.Add(new Dobi_dokumente_dokument_response(id, external_id, TITLE, CREATION_TIME, CREATION_LOCATION, FILENAME, MIMETYPE,
                    ORGANIZATION, INSERT_DATE, CLASSIFICATION_NAME, ACCOUNT_NAME, USR_ID, STATUS, USR_ID_UPORABNIK, SIZE, TYPE, ACC_ID, 
                    VER_DESCRIPTION, DOC_ID, DATA_REFERENCE));

            }
            DobiDokumente_response ret;
            if (list.Count == 0)
                ret = new DobiDokumente_response(false, "Ni podatkov!", list);
            else
                ret = new DobiDokumente_response(true, "", list);

            return ret;
        }
        public DobiDokument_response DobiDokument(int docid)
        {
            DobiDokument_response response;
            try
            {
                _guid = Login(_username, _password);
            }
            catch (Exception ex)
            {
                response = new DobiDokument_response(false,  string.Format("WS Login: {0}", ex.Message));
            }
            try
            {
                ReceiveDocument(_guid, docid);
                response = new DobiDokument_response(true, "");
            }
            catch (Exception ex)
            {
                response = new DobiDokument_response(false,  ex.Message);
            }
            return response;
        }
        private string GetPortalId(string response)
        {
            var startIdent = "<sendDocumentReturn>";
            var endIdent = "</sendDocumentReturn>";
            var startIndex = response.IndexOf(startIdent) + startIdent.Length;
            var length = response.IndexOf(endIdent) - startIndex;
            var idPortal = response.Substring(startIndex, length);
            return idPortal;
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public string Login(string username, string password)        
        {
            return _svc.login(username, password);
        }

        public string Echo(string s)
        {
            return _svc.echo(s);
        }               

        public void sendDocument(string guid, bool isArchived, string path)
        {    
            var attachment = new Microsoft.Web.Services2.Attachments.Attachment("text/xml", path);
            //_svc.RequestSoapContext.Attachments.Add(attachment);
            //return _svc.sendDocument(guid, isArchived);
        }

        public string GetDocumentList(string guid, string taxNumber, string filterXml)
        {
            return _svc.getDocumentList(guid, taxNumber, filterXml);
        }

        public void ReceiveDocument(string guid, int docId)
        {
            List<string> retVal = new List<string>();
            XmlDocument xml = new XmlDocument();
            try
            {
                var webrequest = CreateWebRequestReceive(_endpointUrl, "recieveDocument");
                var recieve = CreateSoapEnvelopeReceive(guid, docId);
                InsertSoapEnvelopeIntoWebRequest(recieve, webrequest);
                var responce = webrequest.GetResponse();
                //Stream s = responce.GetResponseStream();
                //StreamReader sr = new StreamReader(s);
                //string r = sr.ReadToEnd();
                //responce.Close();

                retVal = ReadMultipart(responce, docId).Result;
                xml.LoadXml(retVal[0]); // suppose that myXmlString contains "<Names>...</Names>"
                XmlNodeList xnList = xml.GetElementsByTagName("receiveDocumentReturn");
                //XmlNodeList xnList = xml.SelectNodes("/soapenv:Envelope/soapenv:Body/p338:receiveDocumentResponse");
                if (xnList.Count != 1)
                {
                    InsertDataintoDatabase(docId, 1, "Napaka pri branju o uspešnosti poizvedbe!", retVal[0], null);
                }
                else
                {
                    if (retVal.Count == 2)
                    {
                        XmlDocument xml_data = new XmlDocument();
                        xml_data.LoadXml(retVal[1]);
                        if (InsertDataintoDatabase(docId, 0, "", retVal[0], retVal[1]) == 1)
                            _svc.receiveDocumentCommit(guid, docId, true);
                    }
                    else
                    {
                        InsertDataintoDatabase(docId, -1, "Neustrezne priloge!", retVal[0], null);
                    }
                }
            }
            catch (Exception ex)
            {
                if (retVal.Count == 1)
                   InsertDataintoDatabase(docId, -1,ex.Message, retVal[0], null);
                if (retVal.Count == 2)
                    InsertDataintoDatabase(docId, -1, ex.Message, retVal[0], retVal[1]);
                if (retVal.Count == 0)
                    InsertDataintoDatabase(docId, -1, "retval null", null, null);
            }
        }
        private int InsertDataintoDatabase(int docid, int error , string error_str, string xml_response, string xml_data)
        {
           
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "bie_zziDokumenti";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    AddSQLParameter(cmd, "@id_dokumenta", SqlDbType.Int, ParameterDirection.Input, docid);
                    AddSQLParameter(cmd, "@error", SqlDbType.Int, ParameterDirection.Input, error);
                    AddSQLParameter(cmd, "@error_str", SqlDbType.NVarChar, ParameterDirection.Input, error_str);
                    AddSQLParameter(cmd, "@xmlresponse", SqlDbType.Xml, ParameterDirection.Input, xml_response);
                    AddSQLParameter(cmd, "@xmlData", SqlDbType.Xml, ParameterDirection.Input, xml_data);

                    cmd.ExecuteNonQuery();

                    return 1;

                }
            }
            

        }
        public void AddSQLParameter(SqlCommand _cmd, String _ParameterName, SqlDbType _SqlDbType, ParameterDirection _ParameterDirection, Object _ParameterValue)
        {
            AddSQLParameter(_cmd, _ParameterName, _SqlDbType, _ParameterDirection, _ParameterValue, null);
        }

        public void AddSQLParameter(SqlCommand _cmd, String _ParameterName, SqlDbType _SqlDbType, ParameterDirection _ParameterDirection, Object _ParameterValue, Int32? _ParameterLength)
        {
            SqlParameter _NewParameter = new SqlParameter();

            _NewParameter.ParameterName = _ParameterName;
            _NewParameter.SqlDbType = _SqlDbType;
            _NewParameter.Direction = _ParameterDirection;

            if (_ParameterLength != null)
            {
                _NewParameter.Size = (Int32)_ParameterLength;
            }

            if (_ParameterValue == null)
                _NewParameter.Value = DBNull.Value;
            else
                _NewParameter.Value = _ParameterValue;

            _cmd.Parameters.Add(_NewParameter);
        }

        private async Task<List<string>> ReadMultipart(WebResponse httpResponse, int docId)
        {
            var content = new StreamContent(httpResponse.GetResponseStream());
            content.Headers.Add("Content-Type", httpResponse.ContentType);

            MultipartMemoryStreamProvider multipart = new MultipartMemoryStreamProvider();
            Task.Factory.StartNew(() => multipart = content.ReadAsMultipartAsync().Result,
                CancellationToken.None,
                TaskCreationOptions.LongRunning, // guarantees separate thread
                TaskScheduler.Default)
                .Wait();
            String xml = await multipart.Contents[0].ReadAsStringAsync();
            List<string> data = new List<string>();
            data.Add(xml);
            string path = _tempFolderPath + "/" + docId.ToString() + ".zip";
            byte[] fileData = multipart.Contents[1].ReadAsByteArrayAsync().Result;
            System.IO.File.WriteAllBytes(path, fileData);
            using (ZipArchive archive = new ZipArchive(File.OpenRead(path)))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if ((entry.FullName.EndsWith(".xml") || entry.FullName.EndsWith(".XML")) & !entry.FullName.Contains("envelope"))
                    {
                        var stream = entry.Open();
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            data.Add(RemoveEncodingFromXmlString(reader.ReadToEnd()));

                        }
                    }
                }
            }
            return data;
        }

        public static string RemoveEncodingFromXmlString(string xmlIn)
        {
            XDocument xdoc = XDocument.Parse(xmlIn);
            xdoc.Declaration = null;



            return xdoc.ToString();
        }


        public void ReceiveDocumentCommit(string guid, int? docId, bool isArchived)
        {
         //   return _svc.receiveDocumentCommit(guid, docId, isArchived);
        }

        public void IsCompanyEDIEnabled(string guid, string taxId)
        {
           // return _svc.isCompanyEDIEnabled(guid, taxId);
        }

        public void GetELocation(string guid, string elocationId)
        {
            // return _svc.getELocationByELocationId(guid, elocationId);
        }

        public void GetCompanyList(string guid, string filter)
        {
             // return _svc.getCompanyList(guid, filter);
        }

        public void GetStatus(string guid, int? msgId)
        {
            // return _svc.getStatusByMsgId(guid, msgId);
        }

        private static HttpWebRequest CreateWebRequestSend(string url, string action, string boundary)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            webRequest.ContentType = "multipart/related; boundary=\"" + boundary + "\";start=\"<sendDoc>\"; type=\"text/xml\"";
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.Headers.Add("MIME-Version", "1.0");
            //    webRequest.ContentType = "text/xml;charset=\"utf-8\"";

            //    webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        /*
            POST http://was7test.zzi.si:80/zziestoritve/services/EStoritve HTTP/1.1
            Accept-Encoding: gzip,deflate
            Content-Type: text/xml;charset=UTF-8
            SOAPAction: "receiveDocument"
            Content-Length: 346
            Host: was7test.zzi.si:80
            Connection: Keep-Alive
            User-Agent: Apache-HttpClient/4.1.1 (java 1.5)
        */

        private static HttpWebRequest CreateWebRequestReceive(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            webRequest.ContentType = "text/xml;charset=UTF-8";
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.Headers.Add("MIME-Version", "1.0");
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";

                webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelopeReceive(String guid, int dokId)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml("<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ws=\"http://ws.estoritve.zzi.si\">" +
                     "<SOAP-ENV:Body>" +
                "<ws:receiveDocument>" +
                "<guid>" + guid + "</guid>" +
                "<docid>" + dokId + "</docid>" +
                "</ws:receiveDocument>" +
                     "</SOAP-ENV:Body>" +
                    "</SOAP-ENV:Envelope>");
            return soapEnvelop;
        }

        private static XmlDocument CreateSoapEnvelopeSend(String guid)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml("<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ws=\"http://ws.estoritve.zzi.si\">" +
                     "<SOAP-ENV:Body>" +
                "<ws:sendDocument>" +
                "<guid>" + guid + "</guid>" +
                "<isArchived>false</isArchived>" +
                "</ws:sendDocument>" +
                     "</SOAP-ENV:Body>" +
                    "</SOAP-ENV:Envelope>");
            return soapEnvelop;
        }

        public String downloadSoapAttchemnt(String guid, int docId)
        {            
            String a = _svc.receiveDocument(guid, docId);
            return a;
        }

        public MemoryStream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public void Write(Stream from, Stream to)
        {
            for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
                to.WriteByte((byte)a);
        }
        public void SaveStreamToFile(Stream stream, string filename)
        {
            using (Stream destination = File.Create(filename))
                Write(stream, destination);
        }

        public void CallWebService(String guid1, int docId, string filePath)
        {
            
            XmlDocument soapEnvelopeXml = CreateSoapEnvelopeReceive(guid1, docId);
            HttpWebRequest webRequest = CreateWebRequestReceive(_endpointUrl, "\"sendDocument\"");
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);


            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {

                String[] sesGlava = webResponse.Headers["Content-Type"].Split(';');
                StringBuilder a = new StringBuilder();
                a.Append(sesGlava[3] + ";");
                StringBuilder test = new StringBuilder();

                test.Append("MIME-Version: 1.0");
                test.Append("\r\n");
                test.Append("Content-Type: multipart/mixed;" + sesGlava[3]);
                test.Append("\r\n");
                test.Append("\r\n");

                MemoryStream streamTest = GenerateStreamFromString(test.ToString());
                MemoryStream strApp = new MemoryStream();

                Write(streamTest, strApp);
                Write(webResponse.GetResponseStream(), strApp);

                strApp.Position = 0;
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {

                    test.Append(rd.ReadToEnd());

                    OpenPop.Mime.Message mess = OpenPop.Mime.Message.Load(strApp);


                    List<MessagePart> l = mess.FindAllAttachments();
                    foreach (var attachment in mess.FindAllAttachments())
                    {
                        //string filePath = saveName;
                        attachment.Save(new FileInfo(filePath));
                    }

                }

            }
           
        }
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        public String UploadFileEx(String guid, byte[] fileBytes)
        {
            try
            {
                String boundary = "----=my_boundary";
                String fileName = "myAttachment.zip";
                HttpWebRequest webrequest = CreateWebRequestSend(_endpointUrl, "\"sendDocument\"", boundary);
                //HttpWebRequest webrequest = CreateWebRequestSend(@"http://demo.zzi.si/zziestoritve/services/EStoritve", "\"sendDocument\"", boundary);
                StringBuilder sb = new StringBuilder();

                sb.Append("--");
                sb.Append(boundary);
                sb.Append("\r\n");
                sb.Append("Content-Type: ");
                sb.Append("text/xml; charset=UTF-8");
                sb.Append("\r\n");
                sb.Append("Content-Transfer-Encoding: ");
                sb.Append("8bit");
                sb.Append("\r\n");
                sb.Append("Content-ID: ");
                sb.Append("<sendDoc>");
                sb.Append("\r\n");
                sb.Append("\r\n");
                sb.Append(CreateSoapEnvelopeSend(guid).OuterXml);
                sb.Append("\r\n");
                sb.Append("--");
                sb.Append(boundary);
                sb.Append("\r\n");

                sb.Append("Content-type: ");
                sb.Append("application/zip; name=");
                sb.Append(fileName);
                sb.Append("\r\n");
                sb.Append("Content-Transfer-Encoding: ");
                sb.Append("binary");
                sb.Append("\r\n");
                sb.Append("Content-ID: ");
                sb.Append(fileName);
                sb.Append("\r\n");


                sb.Append("Content-Disposition: attachment; name=\"");
                sb.Append(fileName);//envelope.zip
                sb.Append("\"; filename=\"");
                sb.Append(fileName);//envelope.zip
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("\r\n");
                string postHeader = sb.ToString();
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                long length = postHeaderBytes.Length + fileBytes.Length + boundaryBytes.Length + 2;
                webrequest.ContentLength = length;
                Stream requestStream = webrequest.GetRequestStream();
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                requestStream.Write(fileBytes, 0, fileBytes.Length);
                boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                WebResponse responce = webrequest.GetResponse();
                Stream s = responce.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                string r = sr.ReadToEnd();
                responce.Close();
                //lblFile.ForeColor = Color.Green;
                //lblFile.Text = lblFile.Text + " \u2714";
                //label4.Text = label4.Text + " (ID: " + getSendDocId(r) + ")";
                return r;
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                          .ReadToEnd();
                //MessageBox.Show(pageContent);
                //lblFile.ForeColor = Color.Red;
                //lblFile.Text = lblFile.Text + "... NAPAKA PRI POŠILJANJU!";
                //label4.Text = "Datoteka: ";
                //return "prislo je do napake!";
                //throw new Exception(wex.Message + wex.InnerException );
                throw new Exception(string.Format("{0}: {1}", wex.Message, wex.InnerException));
            }
        }


        public envelope CreateEnvelope(string from, string to, string fileName, string description, string docType)
        {
            var fileType = System.IO.Path.GetExtension(fileName).Replace(".", string.Empty);

            var e = new envelope();
            e.header = new header();
            e.header.from = new location();
            //e.header.from.e_address = "SI25670344";
            e.header.from.e_location = from;
            //e.header.from.e_address1 = "";            
            /*
            e.header.from.physicaladdress = new locationPhysicaladdress();
            e.header.from.physicaladdress.address = "";
            e.header.from.physicaladdress.country = "";
            e.header.from.physicaladdress.name = "";
            e.header.from.physicaladdress.po = "";
            e.header.from.physicaladdress.po_code = "";
            */
            e.header.to = new location();
            //e.header.to.e_address = "SI25670000";
            e.header.to.e_location = to;
            /*
            e.header.to.e_address1 = "";            
            e.header.to.physicaladdress = new locationPhysicaladdress();
            e.header.to.physicaladdress.address = "";
            e.header.to.physicaladdress.country = "";
            e.header.to.physicaladdress.name = "";
            e.header.to.physicaladdress.po = "";
            e.header.to.physicaladdress.po_code = "";
                        
            e.header.status = "";            
            e.header.@params = new param[1];
            e.header.@params[0] = new param();
            e.header.@params[0].Name = "";
            e.header.@params[0].Value = "";
            
            */
            
            e.document = new document();
            e.document.description = description;
            e.document.external_id = "";
            e.document.file_name = fileName;
            e.document.format = fileType;
            e.document.location = "";
            //e.document.raw_data = "";
            e.document.type = docType;

            //e.attachments = new document[1];
            //e.attachments[0].
                        
            return e;
            
        }

        private document DeserializeDocument(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(document));
            StringReader rdr = new StringReader(inputString);
            return (document)serializer.Deserialize(rdr);            
        }

        public XmlPaket GetXmlPaket(int id)
        {            
            XmlPaket paket = null;
            var sql = @"select PosiljateljPortal, PrejemnikPortal, 
                                status_paketa, cf_imeDatoteke, docType
                        from bie_FaktureXmlPaketi where xml_paket = @id";
            
            using (var conn = new SqlConnection((_connString)))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {                            
                            paket = new XmlPaket { From = rdr.GetString(0), To = rdr.GetString(1),
                                                    Status = rdr.GetInt16(2), ImeDatoteke = rdr.GetString(3) , DocType = rdr.GetString(4)};
                        }                      
                    }
                }

                // posebaj branje XML zaradi omejitev datareaderja
                sql = @"select xml_vsebina
                        from bie_FaktureXmlPaketi where xml_paket = @id";
                using (var cmd = conn.CreateCommand())
                {  
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rdr = cmd.ExecuteXmlReader())
                    {
                        if (rdr.Read())
                        {
                            paket.XmlContent = rdr.ReadOuterXml();

                            //throw new Exception(string.Format("XML READ: {0}", paket.XmlContent.Length));
                        }                        
                    }
                }
                           
            }
            return paket;
        }
       

        public T Deserialize<T>(string serializedResults)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(serializedResults))
                return (T)serializer.Deserialize(stringReader);
        }
        /*
        public GeneratedClassFromXSD GetObjectFromXML()
        {
            var settings = new XmlReaderSettings();
            var obj = new GeneratedClassFromXSD();
            var reader = XmlReader.Create(urlToService, settings);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GeneratedClassFromXSD));
            obj = (GeneratedClassFromXSD)serializer.Deserialize(reader);

            reader.Close();
            return obj;
        }
        */
        public void UpdatePaketEnd(int id, string portalId, string napaka, int status )
        {
            var sql = @"update bie_FaktureXmlPaketi 
                            set IdPortal = @portalId,
                                DatumPotrditvePortal = getdate(),
                                NapakaPortal = @napakaPortal,
                                status_paketa = @status
                        where xml_paket = @id";
            using (var conn = new SqlConnection((_connString)))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    if (string.IsNullOrEmpty(portalId))
                    {
                        cmd.Parameters.AddWithValue("@portalId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@portalId", portalId);
                    }                    

                    if (string.IsNullOrEmpty(napaka))
                    {
                        cmd.Parameters.AddWithValue("@napakaPortal", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@napakaPortal", napaka);
                    }                    
                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.ExecuteNonQuery();
                }
            }            
        }

        public void CreateZip(string zipFile, List<string> files)
        {
            using (ZipFile z = ZipFile.Create(zipFile))
            {                
                z.BeginUpdate();
                foreach (var f in files)
                {
                    z.Add(f, System.IO.Path.GetFileName(f));
                }                
                z.CommitUpdate();
            }
        }

                
        // Compresses the files in the nominated folder, and creates a zip file on disk named as outPathname.
        //
        public void CreateSample(string outPathname, string password, string folderName) {

            FileStream fsOut = File.Create(outPathname);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            //zipStream.Password = password;  // optional. Null is the same as not setting. Required if using AES.

            // This setting will strip the leading part of the folder path in the entries, to
            // make the entries relative to the starting folder.
            // To include the full path for each entry up to the drive root, assign folderOffset = 0.
            int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

            CompressFolder(folderName, zipStream, folderOffset);

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();
        }

        private void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {

            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }

        public string Serialize<T>(T value)
        {
            if (value == null)
                return string.Empty;

            string serializedXml = string.Empty;

            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
                StringWriter stringWriter = new Utf8StringWriter();                
                XmlWriter writer = XmlWriter.Create(stringWriter);
                xmlserializer.Serialize(writer, value);
                serializedXml = stringWriter.ToString();
                writer.Close();
            }
            catch (Exception ex)
            {
                serializedXml = string.Format(@"<serializationError>{0}</serializationError>", ex.Message);
            }

            return serializedXml;
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }



        }

    }
}
