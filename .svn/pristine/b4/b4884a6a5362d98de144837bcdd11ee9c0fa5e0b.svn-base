using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PripravljalecPrognozLib
{
    public static class PripravljalecPrognozSchema
    {
        public static DataTable GetOfftakePointsDataTable()
        {
            DataTable table = new DataTable();
            DataColumn column;

            column = table.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = table.Columns.Add();
            column.ColumnName = "OfftakePointCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CityGateCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "MeasurementDeviceMultiplier";
            column.DataType = typeof(decimal);

            column = table.Columns.Add();
            column.ColumnName = "LoadType";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Status";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "SupplierCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "YearlyOfftake";
            column.DataType = typeof(int);

            column = table.Columns.Add();
            column.ColumnName = "vpis_datetime";
            column.DataType = typeof(DateTime);
            column.DefaultValue = DateTime.Now;

            return table;
        }

        public static DataTable GetOfftakePointsMeasurmentsDataTable()
        {
            DataTable table = new DataTable();
            DataColumn column;

            column = table.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = table.Columns.Add();
            column.ColumnName = "OfftakePointCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CityGateCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "readingTime";
            column.DataType = typeof(DateTime);

            column = table.Columns.Add();
            column.ColumnName = "entryTime";
            column.DataType = typeof(DateTime);

            column = table.Columns.Add();
            column.ColumnName = "readingValue";
            column.DataType = typeof(int);

            column = table.Columns.Add();
            column.ColumnName = "conversionFactor";
            column.DataType = typeof(decimal);

            column = table.Columns.Add();
            column.ColumnName = "conversionUnit";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "ReadingDeviceChanged";
            column.DataType = typeof(bool);

            column = table.Columns.Add();
            column.ColumnName = "vpis_datetime";
            column.DataType = typeof(DateTime);
            column.DefaultValue = DateTime.Now;

            return table;
        }
        public static DataTable GetOfftakePointsReadingsDataTable()
        {
            DataTable table = new DataTable();
            DataColumn column;

            column = table.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = table.Columns.Add();
            column.ColumnName = "OfftakePointCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CityGateCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "ReadingDate";
            column.DataType = typeof(DateTime);

            column = table.Columns.Add();
            column.ColumnName = "entryTime";
            column.DataType = typeof(DateTime);

            column = table.Columns.Add();
            column.ColumnName = "readingValue";
            column.DataType = typeof(int);

            column = table.Columns.Add();
            column.ColumnName = "conversionFactor";
            column.DataType = typeof(decimal);

            column = table.Columns.Add();
            column.ColumnName = "conversionUnit";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "ReadingDeviceChanged";
            column.DataType = typeof(bool);

            column = table.Columns.Add();
            column.ColumnName = "vpis_datetime";
            column.DataType = typeof(DateTime);
            column.DefaultValue = DateTime.Now;

            return table;
        }
        public static DataTable GetOfftakePointsAllocationsDataTable()
        {
            DataTable table = new DataTable();
            DataColumn column;

            column = table.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = table.Columns.Add();
            column.ColumnName = "GasDay";
            column.DataType = typeof(DateTime);

            column = table.Columns.Add();
            column.ColumnName = "OfftakePointCode";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "CityGateCode";
            column.DataType = typeof(string);


            column = table.Columns.Add();
            column.ColumnName = "Allocation";
            column.DataType = typeof(decimal);

            column = table.Columns.Add();
            column.ColumnName = "Unit";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "vpis_datetime";
            column.DataType = typeof(DateTime);
            column.DefaultValue = DateTime.Now;

            return table;
        }

        public static DataRow GetOfftakePointsDataRow(DataTable table, string offtakePointCode, string cityGateCode, decimal? measurementDeviceMultiplier, string loadType, string status, string supplierCode, int? yearlyOfftake)
        {
            DataRow row = table.NewRow();
            row["offtakePointCode"] = offtakePointCode;
            row["cityGateCode"] = cityGateCode;
            row["measurementDeviceMultiplier"] = (measurementDeviceMultiplier == null ? (object)DBNull.Value : measurementDeviceMultiplier);
            row["loadType"] = loadType;
            row["status"] = status;
            row["supplierCode"] = supplierCode;
            row["yearlyOfftake"] = (yearlyOfftake == null ? (object)DBNull.Value : yearlyOfftake);
            return row;
        }
        public static DataRow GetOfftakePointsMeasurmentsDataRow(DataTable table, string offtakePointCode, string cityGateCode, DateTime readingTime, DateTime entryTime, int readingValue, decimal conversionFactor, string conversionUnit, bool readingDeviceChanged)
        {
            DataRow row = table.NewRow();
            row["offtakePointCode"] = offtakePointCode;
            row["cityGateCode"] = cityGateCode;
            row["readingTime"] = readingTime;
            row["entryTime"] = entryTime;
            row["readingValue"] = readingValue;
            row["conversionFactor"] = conversionFactor;
            row["conversionUnit"] = conversionUnit;
            row["readingDeviceChanged"] = readingDeviceChanged;
            return row;
        }
        public static DataRow GetOfftakePointsReadingsDataRow(DataTable table, string offtakePointCode, string cityGateCode, DateTime readingDate, DateTime entryTime, int readingValue, decimal conversionFactor, string conversionUnit, bool readingDeviceChanged)
        {
            DataRow row = table.NewRow();
            row["offtakePointCode"] = offtakePointCode;
            row["cityGateCode"] = cityGateCode;
            row["readingDate"] = readingDate;
            row["entryTime"] = entryTime;
            row["readingValue"] = readingValue;
            row["conversionFactor"] = conversionFactor;
            row["conversionUnit"] = conversionUnit;
            row["readingDeviceChanged"] = readingDeviceChanged;
            return row;
        }
        public static DataRow GetOfftakePointsAllocationsDataRow(DataTable table, DateTime gasDay, string offtakePointCode, string cityGateCode, decimal allocation, string unit)
        {
            DataRow row = table.NewRow();
            row["GasDay"] = gasDay;
            row["OfftakePointCode"] = offtakePointCode;
            row["cityGateCode"] = cityGateCode;
            row["Allocation"] = allocation;
            row["Unit"] = unit;
            return row;
        }

    }
}
