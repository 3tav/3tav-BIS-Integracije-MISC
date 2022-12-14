using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GemaLogicLib
{
    public class DbService
    {
        private string _connString;

        public DbService(string connString)
        {
            _connString = connString;
        }

        public DbService() : this (ConfigurationManager.ConnectionStrings["connString"].ToString())
        {
            
        }
        
        public void BulkInsertOdcitki(DataTable table)
        {
            // bulk insert v bazo            
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var bc = new SqlBulkCopy(conn);
                bc.DestinationTableName = "bis_LoraPodatki";
                bc.WriteToServer(table);
            }
        }

        public void BulkInsertOdjemnaMesta(DataTable table)
        {
            // bulk insert v bazo            
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var bc = new SqlBulkCopy(conn);
                bc.DestinationTableName = "bis_LoraOM";
                bc.WriteToServer(table);
            }
        }

        public DataRow CreateRowOdjemnaMesta(DataTable table, int idPaketa, string externalIdent, int idLora, string name, int productId)
        {
            DataRow row = table.NewRow();
            row["idPaketa"] = idPaketa;
            row["externalIdent"] = externalIdent;
            row["idLora"] = idLora;
            row["name"] = name;
            row["productId"] = productId;
            row["vpis_datetime"] = DateTime.Now;
            return row;
        }


        public DataRow CreateRowOdcitki(DataTable table, int idPaketa, string oznaka, DateTime datum, int externalId, string externalIdOzn, decimal odcitek, int idEnote)
        {
            DataRow row = table.NewRow();
            row["idPaketa"] = idPaketa;
            row["Oznaka"] = oznaka;
            row["Datum"] = datum;
            row["externalId"] = externalId;
            row["externalIdOzn"] = externalIdOzn;
            
            row["Odcitek"] = odcitek;
            row["IdEnote"] = idEnote;

            row["vpis_datetime"] = DateTime.Now;
            return row;
        }

        public void ExecuteSQLProc(string procedureName)
        {
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = procedureName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);
            }
        }

        public int CreatePaket(DateTime datumOd, DateTime datumDo, string naborOm)
        {
            int id = -1;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"insert into bis_LoraPaketi (datum_od, datum_do, NaborOM, vpis_datetime)
                                        OUTPUT INSERTED.idPaketa
                                        values (@datum_od, @datum_do, @naborOM, @vpis_datetime)";
                    cmd.Parameters.AddWithValue("@datum_od", datumOd);
                    cmd.Parameters.AddWithValue("@datum_do", datumDo);
                    cmd.Parameters.AddWithValue("naborOM", (string.IsNullOrEmpty(naborOm) ? (object)DBNull.Value : naborOm ));
                    cmd.Parameters.AddWithValue("@vpis_datetime", DateTime.Now);
                    id = (int)cmd.ExecuteScalar();
                }
            }
            return id;
        }

        public List<int> GetNaborOm(int idPaketa)
        {
            var naborOm = new List<int>();
            var sql = @"select idLora from bis_LoraOM where idPaketa = @idpaketa";
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@idpaketa", idPaketa);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            naborOm.Add(rdr.GetInt32(0));
                        }
                    }
                    
                }
            }            
            return naborOm;
        }

        public List<int> GetNaborOmZadnji()
        {
            var naborOm = new List<int>();
            var sql = @"select idLora from bis_LoraOM where idPaketa = (select max(idPaketa) from bis_loraOM)";
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;                    
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            naborOm.Add(rdr.GetInt32(0));
                        }
                    }

                }
            }
            return naborOm;
        }        
    }
}
