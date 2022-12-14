using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PripravljalecPrognozLib
{
    public class PripravljalecPrognozDAL
    {
        private bool _logExceptions = true;
        private string _connString;
        private object Measurments;

        public PripravljalecPrognozDAL(string connString)
        {
            _connString = connString;
        }
        public string GetConfigDescription()
        {
            return this._connString.Substring(0,80);
        }


        public PripravljalecPrognozDAL() : this(ConfigurationManager.ConnectionStrings["connString"].ToString())
        {
            
        }

        public void Log(string url, string method, object request, object response, int result, string message, string oznaka, int steviloOk, int steviloNapak, int trajanje)
        {
            try
            {
                using (var conn = new SqlConnection(_connString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "p_pripPrognoz_wsLog";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@method", method);
                        cmd.Parameters.AddWithValue("@request", request);
                        cmd.Parameters.AddWithValue("@response", response);
                        cmd.Parameters.AddWithValue("@result", result);
                        cmd.Parameters.AddWithValue("@message", message);
                        cmd.Parameters.AddWithValue("@oznaka", oznaka);
                        cmd.Parameters.AddWithValue("@stevilo_ok", steviloOk);
                        cmd.Parameters.AddWithValue("@stevilo_napak", steviloNapak);
                        cmd.Parameters.AddWithValue("@trajanje", trajanje);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (_logExceptions)
                    throw new Exception(ex.Message);
            }            
        }

        public void Potrdi(string procedureName)
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
                if (_logExceptions)
                    throw new Exception(ex.Message);
            }
        }
        public List<PPOfftakePoint> GetAddOfftakePoints(string method)
        {
            var otps = new List<PPOfftakePoint>();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT OfftakePointCode, CityGateCode, Method,
		                                        MeasurementDeviceMultiplier, LoadType, Status,
		                                        SupplierCode, YearlyOfftake, ValidFrom,
		                                        id_odjemnega_mesta
                                        FROM v_PripPrognoz_OfftakePoints
                                        WHERE method = @method";
                    cmd.Parameters.AddWithValue("@method", method);
                    cmd.CommandTimeout = 0;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            otps.Add(new PPOfftakePoint()
                            {
                                OfftakePointCode = rdr.GetString(0),
                                CityGateCode = rdr.GetString(1),
                                MeasurementDeviceMultiplier = (rdr.IsDBNull(3) ? (decimal?)null : rdr.GetDecimal(3)), 
                                LoadType = rdr.GetString(4),
                                Status = rdr.GetString(5),
                                SupplierCode = rdr.GetString(6),
                                YearlyOfftake = (rdr.IsDBNull(7) ? (int?)null : rdr.GetInt32(7)), 
                                ValidFrom = (rdr.IsDBNull(8) ? (DateTime?)null : rdr.GetDateTime(8)),
                                IdOdjemnegaMesta = rdr.GetInt32(9)
                            });
                        }
                    }                    
                }
            }
            return otps;
        }
        public List<PPOfftakePoint> GetChangeSupplierOfftakePoints(string method)
        {
            var otps = new List<PPOfftakePoint>();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT OfftakePointCode, CityGateCode, 
		                                        SupplierCode, ValidFrom
                                        FROM PripPrognoz_ChangeOfftakePointsSupplier";
                    cmd.CommandTimeout = 0;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            otps.Add(new PPOfftakePoint()
                            {
                                OfftakePointCode = rdr.GetString(0),
                                CityGateCode = rdr.GetString(1),
                                SupplierCode = rdr.GetString(2),
                                ValidFrom = (rdr.IsDBNull(3) ? (DateTime?)null : rdr.GetDateTime(3))
                            });
                        }
                    }
                }
            }
            return otps;
        }

        public List<PPOfftakePointReading> GetAddOfftakePointsReadings(string method)
        {
            var otps = new List<PPOfftakePointReading>();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {   if (method.CompareTo("ANNUL") == 0)  {
                        cmd.CommandText = @"SELECT OfftakePointCode, CityGateCode, ReadingDate,
                                                ReadingValue=0, Nm3ConversionFactor=1, NewReadingValue=null,
                                                NewNm3ConversionFactor 
                                        FROM PripPrognoz_AnnulOfftakePointsReadings=null";
                    }
                    else
                    { cmd.CommandText = @"SELECT OfftakePointCode, CityGateCode, ReadingDate,
                                                ReadingValue, Nm3ConversionFactor, NewReadingValue,
                                                NewNm3ConversionFactor 
                                        FROM PripPrognoz_OfftakePointsReadings";
                        cmd.Parameters.AddWithValue("@method", method);
                    }
                    
                    cmd.CommandTimeout = 0;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            otps.Add(new PPOfftakePointReading()
                            {
                                OfftakePointCode = rdr.GetString(0),
                                CityGateCode = rdr.GetString(1),
                                ReadingDate = rdr.GetDateTime(2),
                                ReadingValue = rdr.GetInt32(3),
                                Nm3ConversionFactor = rdr.GetDecimal(4),
                                NewReadingValue = (rdr.IsDBNull(5) ? (int?)null : rdr.GetInt32(5)),
                                NewNm3ConversionFactor = (rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6))                                
                            });
                        }
                    }                    
                }
            }
            return otps;
        }
        public List<PPOfftakePointReadingAnnul> GetAnnulOfftakePointsReadings(string method)
        {
            var otps = new List<PPOfftakePointReadingAnnul>();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                      cmd.CommandText = @"SELECT OfftakePointCode, CityGateCode, ReadingDate 
                                        FROM PripPrognoz_AnnulOfftakePointsReadings";
                      cmd.CommandTimeout = 0;
                  using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            otps.Add(new PPOfftakePointReadingAnnul()
                            {
                                OfftakePointCode = rdr.GetString(0),
                                CityGateCode = rdr.GetString(1),
                                ReadingDate = rdr.GetDateTime(2),
                            });
                        }
                    }
                }
            }
            return otps;
        }



        public List<PPOfftakePointMeasurments> GetAddOfftakePointsMeasurements(string method)
        {
            var otps = new List<PPOfftakePointMeasurments>();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select distinct v.OfftakePointCode, v.CityGateCode from dbo.PripPrognoz_OfftakePointMeasurments v
	            join PripPrognoz_OdjemnaMestaZaPrenos p on v.id_odjemnega_mesta = p.id_odjemnega_mesta
	            where p.DaljinskiValidiran = 1 
	            order by 1";
                    //cmd.Parameters.AddWithValue("@method", method);
                    cmd.CommandTimeout = 0;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            otps.Add(new PPOfftakePointMeasurments()
                            {
                                OfftakePointCode = rdr.GetString(0),
                                CityGateCode = rdr.GetString(1),
                                Measurments = new List<PPOfftakePointMeasurment>()
                            })
                            //Measurments = Helpers.MapOfftakePointsMeasurements(GetAddOfftakePointsMeasurement(this.OfftakePointCode))
                            ;
                            /*.Add(new PPOfftakePointMeasurment()
                            {
                                ReadingTime = rdr.GetDateTime(2),
                                ReadingValue = rdr.GetInt32(3),
                                Nm3ConversionFactor = rdr.GetDecimal(4),
                                NewReadingValue = (rdr.IsDBNull(5) ? (int?)null : rdr.GetInt32(5)),
                                NewNm3ConversionFactor = (rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6))
                            })*/
                        }

                    }
                    foreach (var o in otps)
                    {
                        cmd.CommandText = @"select v.ReadingDate, v.ReadingValue, v.Nm3ConversionFactor, v.NewReadingValue, v.NewNm3ConversionFactor from dbo.PripPrognoz_OfftakePointMeasurments v
	            join PripPrognoz_OdjemnaMestaZaPrenos p on v.id_odjemnega_mesta = p.id_odjemnega_mesta
	            where p.DaljinskiValidiran = 1 and v.OfftakePointCode = '"
                + o.OfftakePointCode + "' order by 1";
//                        cmd.Parameters.AddWithValue("@OfftakePointCode", o.OfftakePointCode);
                        Console.WriteLine(o.CityGateCode);
                        Console.WriteLine(o.OfftakePointCode);
                        cmd.CommandTimeout = 0;
                        using (var rdr1 = cmd.ExecuteReader())
                        {
                            while (rdr1.Read())
                            {
                                o.Measurments.Add(new PPOfftakePointMeasurment()
                                {
                                    ReadingTime = rdr1.GetDateTime(0),
                                    ReadingValue = rdr1.GetInt32(1),
                                    Nm3ConversionFactor = rdr1.GetDecimal(2),
                                    NewReadingValue = (rdr1.IsDBNull(3) ? (int?)null : rdr1.GetInt32(3)),
                                    NewNm3ConversionFactor = (rdr1.IsDBNull(4) ? (decimal?)null : rdr1.GetDecimal(4))
                                });
                            }
  //                          cmd.Parameters.RemoveAt("@OfftakePointCode");
                        }

                    }
                }
            }

            return otps;
        }

        public void GetOfftakePointsInsert(DataTable table)
        {            
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();         
                var bc = new SqlBulkCopy(conn);
                bc.DestinationTableName = "PripPrognoz_GetOfftakePoints";
                try
                {
                    bc.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    return;
                    //MessageBox.Show(ex.Message, "Napaka pri zapisu v tabelo PripPrognoz_GetOfftakePoints");
                }
                
            }
        }
        public void GetOfftakePointMeasurementsInsert(DataTable table)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var bc = new SqlBulkCopy(conn);
                bc.DestinationTableName = "PripPrognoz_GetOfftakePointMeasurements";
                bc.WriteToServer(table);
            }
        }
        public void GetOfftakePointReadingsInsert(DataTable table)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var bc = new SqlBulkCopy(conn);
                bc.DestinationTableName = "PripPrognoz_GetOfftakePointReadings";
                bc.WriteToServer(table);
            }
        }
        public void GetOfftakePointAllocationsInsert(DataTable table)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var bc = new SqlBulkCopy(conn);
                bc.DestinationTableName = "PripPrognoz_GetOfftakePointAllocations";
                bc.WriteToServer(table);
            }
        }
    }

    public class PPOfftakePoint
    {    
        public string OfftakePointCode;
        public string CityGateCode;
		public string Status;		
		public string LoadType;		
        public string SupplierCode;
		public System.Nullable<decimal> MeasurementDeviceMultiplier;
		public System.Nullable<int> YearlyOfftake;
		public System.Nullable<System.DateTime> ValidFrom;
        public int IdOdjemnegaMesta;
    }

    public class PPOfftakePointModify
    //brez SupplierCode
    {
        public string OfftakePointCode;
        public string CityGateCode;
        public string Status;
        public string LoadType;
        public System.Nullable<decimal> MeasurementDeviceMultiplier;
        public System.Nullable<int> YearlyOfftake;
        public System.Nullable<System.DateTime> ValidFrom;
        public int IdOdjemnegaMesta;
    }

    public class PPOfftakePointChangeSupplier
    //za changeSupplier
    {
        public string OfftakePointCode;
        public string CityGateCode;
        public string SupplierCode;
        public System.Nullable<System.DateTime> ValidFrom;
    }


    public class PPOfftakePointReading
    {
        public string OfftakePointCode;
        public string CityGateCode;
        public System.DateTime ReadingDate;
        public int ReadingValue;
        public decimal Nm3ConversionFactor;
        public System.Nullable<int> NewReadingValue;
        public System.Nullable<decimal> NewNm3ConversionFactor;
    }
    public class PPOfftakePointReadingAnnul
    {
        public string OfftakePointCode;
        public string CityGateCode;
        public System.DateTime ReadingDate;
    }

    public class PPOfftakePointMeasurments
    {
        public string OfftakePointCode;
        public string CityGateCode;
        public List<PPOfftakePointMeasurment> Measurments;
    }

    //Za branje
    public class PPOfftakePointMeasurmentsR
    {
        public string OfftakePointCode;
        public string CityGateCode;
        public System.DateTime ReadingTime;
        public System.DateTime EntryTime;
        public int ReadingValue;
        public decimal ConversionFactor;
        public string ConversionUnit;
        public System.Boolean ReadingDeviceChanged;

        public PPOfftakePointMeasurmentsR(string offtakePointCode, string cityGateCode, DateTime readingTime, DateTime entryTime, int readingValue, decimal conversionFactor, string conversionUnit, bool readingDeviceChanged)
        {
            OfftakePointCode = offtakePointCode;
            CityGateCode = cityGateCode;
            ReadingTime = readingTime;
            EntryTime = entryTime;
            ReadingValue = readingValue;
            ConversionFactor = conversionFactor;
            ConversionUnit = conversionUnit;
            ReadingDeviceChanged = readingDeviceChanged;
        }
    }

    public class PPOfftakePointMeasurment
    {
        public System.DateTime ReadingTime;
        public int ReadingValue;
        public decimal Nm3ConversionFactor;
        public System.Nullable<int> NewReadingValue;
        public System.Nullable<decimal> NewNm3ConversionFactor;

    }

    public static class Methods
    {
        public const string AddOfftakePoints = "AddOfftakePoints";
        public const string ModifyOfftakePoints = "ModifyOfftakePoints";
        public const string ChangeOfftakePointsSupplier = "ChangeOfftakePointsSupplier";
        public const string GetOfftakePoints = "GetOfftakePoints";
        public const string AddOfftakePointsReadings = "AddOfftakePointsReadings";
        public const string AnnulOfftakePointsReadings = "AnnulOfftakePointsReadings";
        public const string GetOfftakePointsAllocations = "GetOfftakePointsAllocations";
        public const string AddOfftakePointsMeasurments = "AddOfftakePointsMeasurments";
        public const string AdjustOfftakePointsMeasurements = "AdjustOfftakePointsMeasurements";
        public const string GetOfftakePointsMeasurements = "GetOfftakePointsMeasurements";
        public const string GetOfftakePointsReadings = "GetOfftakePointsReadings";
        public const string AddCityGatesForecastDistribution = "AddCityGatesForecastDistribution";
        public const string AddCityGatesAllocationDistribution = "AddCityGatesAllocationDistribution ";
        public const string GetCityGateAllocations = "GetCityGateAllocations  ";
        public const string GetCityGatesBalance = "GetCityGatesBalance ";
        public const string GetCityGateForecasts = "GetCityGateForecasts  ";
        public const string GetTemperatures = "GetTemperatures ";
        public const string TestMethod = "TEST";           
    }

    public enum ServiceResult
    { 
        Error = 0, 
        OK = 1
    }

}
