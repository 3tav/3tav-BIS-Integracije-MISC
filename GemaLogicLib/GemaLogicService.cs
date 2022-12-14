using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;

namespace GemaLogicLib
{
    public class GemaLogicService
    {
        GemaLogicServiceClient.DataRetrieverBulkEndpointClient _svc = null;
        DbService _db = null;
        private string _omIdentifikator;
        private string _dataSourceGroupExternalIdent;
        public void Init()
        {           
            _svc = new GemaLogicServiceClient.DataRetrieverBulkEndpointClient();
            _svc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["GemaLogicUsername"].ToString();
            _svc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["GemaLogicPassword"].ToString();
            _dataSourceGroupExternalIdent = ConfigurationManager.AppSettings["GemaLogicGroupExternalIdent"].ToString();
            _omIdentifikator = ConfigurationManager.AppSettings["omIdentifikator"].ToString();            
            ServicePointManager.Expect100Continue = false;
            _db = new DbService();              
        }


        public void GetOdjemnaMesta()
        {
            var datum = DateTime.Now;
            var idPaketa = _db.CreatePaket(datum, datum, null);

            var dsList = GetAllDataSourcesInGroup();
            var table = DbSchema.CreateDtOdjemnaMesta();
            foreach (var l in dsList)
            {
                if (_omIdentifikator.Length > 0)
                {
                    if (l.name.StartsWith(_omIdentifikator))
                        table.Rows.Add(_db.CreateRowOdjemnaMesta(table, idPaketa, l.externalIdent, (int)l.id, l.name, (int)l.productId));
                }
                else
                {
                    table.Rows.Add(_db.CreateRowOdjemnaMesta(table, idPaketa, l.externalIdent, (int)l.id, l.name, (int)l.productId));            
                }                
            }
            _db.BulkInsertOdjemnaMesta(table);
            _db.ExecuteSQLProc("dbo.BIS_LoraObdelava");

        }

        public void GetZadnjiOdcitek()
        {
            var datum = DateTime.Now;
            var idPaketa = _db.CreatePaket(datum, datum, null);            
            var naborOm = CreateNaborArray(_db.GetNaborOmZadnji());
            var zadnjiOdcitki = ReadLastValue(naborOm);
            var table = DbSchema.CreateDtOdcitki();

            foreach (var o in zadnjiOdcitki)
            {
                decimal odcitek;
                if (decimal.TryParse(o.result1.ToString(), out odcitek))
                {
                    try
                    {
                        table.Rows.Add(_db.CreateRowOdcitki(table, idPaketa, null, o.time, (int)o.dataSourceId, o.dataSourceId.ToString(), odcitek, -1));
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                    }
                }                
            }

            _db.BulkInsertOdcitki(table);
        }

        public void GetOdcitkiZadnjiDan()
        {
            var datumOd = DateTime.Now.Date.AddDays(-1);
            var datumDo = DateTime.Now.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
            GetOdcitki(datumOd, datumDo);
        }

        public void GetOdcitki(DateTime datumOd, DateTime datumDo)
        {            
            var idPaketa = _db.CreatePaket(datumOd, datumDo, null);
            var naborOm = CreateNaborArray(_db.GetNaborOmZadnji());
            var readingsAggr = ReadAggregatedDataListH(naborOm, datumOd, datumDo);
            var table = DbSchema.CreateDtOdcitki();
            decimal odcitek;
            foreach (var rdr in readingsAggr)
            {
                if (rdr == null)
                    continue;
                
                if (rdr.item == null)
                    continue;

                try
                {
                    foreach (var o in rdr.item)
                    {
                        if (o.result1 == null)
                            continue;

                        if (decimal.TryParse(o.result1.ToString(), out odcitek))
                        {
                            try
                            {
                                table.Rows.Add(_db.CreateRowOdcitki(table, idPaketa, null, o.time, (int)o.dataSourceId, o.dataSourceId.ToString(), odcitek, -1));
                            }
                            catch (Exception ex)
                            {
                                var msg = ex.Message;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            _db.BulkInsertOdcitki(table);
        }

        public long?[] CreateNaborArray(List<int> naborList)
        {
            var dataSourceIdsArray = new long?[naborList.Count];
            int i = 0;
            foreach (var id in naborList)
            {
                dataSourceIdsArray[i] = id;
                i++;
            }
            return dataSourceIdsArray;
        }

        public GemaLogicServiceClient.dataSource[] GetAllDataSourcesInGroup()
        {
            var q = new GemaLogicServiceClient.dataSourceGroupQuery();

            q.dataSourceGroupExternalIdent = _dataSourceGroupExternalIdent;
            q.dataSourceUsageType = GemaLogicServiceClient.dataSourceGroupUsageType.Export;
            q.dataSourceUsageTypeSpecified = true;
            return _svc.getAllDataSourcesInGroup(q);

        }

        public GemaLogicServiceClient.mValue[] ReadLastValue(long?[] dataSourceIds)
        {
            var q = new GemaLogicServiceClient.gListQuery();
            q.dataSourceIds = dataSourceIds;
            return _svc.readLastValue(q);
        }
 
        public GemaLogicServiceClient.tValue[] ReadData( DateTime dateFrom, DateTime dateTo)
        {
            var q = new GemaLogicServiceClient.gQuery();
         
            q.fromD = dateFrom;
            q.fromDSpecified = true;

            q.toD = dateTo;
            q.toDSpecified = true;

            return _svc.readData(q);
        }

        public GemaLogicServiceClient.tValue[] ReadData(long dataSourceId, DateTime dateFrom, DateTime dateTo)
        {
            var q = new GemaLogicServiceClient.gQuery();

            q.dataSourceId = dataSourceId;
            q.dataSourceIdSpecified = true;

            q.fromD = dateFrom;
            q.fromDSpecified = true;

            q.toD = dateTo;
            q.toDSpecified = true;

           return _svc.readData(q);
        }

        public GemaLogicServiceClient.mValueArray[] ReadAggregatedDataListH(long?[] dataSourceIds, DateTime dateFrom, DateTime dateTo)
        { 
            var q = new GemaLogicServiceClient.gListQuery();
            q.dataSourceIds = dataSourceIds;
            q.fromD = dateFrom;
            q.fromDSpecified = true;
            q.toD = dateTo;
            q.toDSpecified = true;
            q.timeInterval = GemaLogicServiceClient.timeInterval.h;
            q.timeIntervalSpecified = true;

            return _svc.readAggregatedDataList(q);
        }
    }
 
}
