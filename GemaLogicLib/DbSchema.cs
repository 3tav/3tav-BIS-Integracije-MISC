using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GemaLogicLib
{
    public static class DbSchema
    {
        public static DataTable CreateDtOdjemnaMesta()
        {
            var dt = new DataTable();

            DataColumn column;

            column = dt.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = dt.Columns.Add();
            column.ColumnName = "idPaketa";
            column.DataType = typeof(int);

            column = dt.Columns.Add();
            column.ColumnName = "externalIdent";
            column.DataType = typeof(string);

            column = dt.Columns.Add();
            column.ColumnName = "idLora";
            column.DataType = typeof(int);

            column = dt.Columns.Add();
            column.ColumnName = "name";
            column.DataType = typeof(string);

            column = dt.Columns.Add();
            column.ColumnName = "productId";
            column.DataType = typeof(int);

            column = dt.Columns.Add();
            column.ColumnName = "vpis_datetime";
            column.DataType = typeof(DateTime);

            return dt;
        }

        public static DataTable CreateDtOdcitki()
        {
            var table = new DataTable();
            DataColumn column;

            column = table.Columns.Add();
            column.ColumnName = "id";
            column.DataType = typeof(int);
            column.AutoIncrement = true;

            column = table.Columns.Add();
            column.ColumnName = "idPaketa";
            column.DataType = typeof(int);
            column.AllowDBNull = false; 

            column = table.Columns.Add();
            column.ColumnName = "Oznaka";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Datum";
            column.DataType = typeof(DateTime);            
            column.AllowDBNull = false; 

            column = table.Columns.Add();
            column.ColumnName = "externalId";
            column.DataType = typeof(int);
            column.AllowDBNull = false; 

            column = table.Columns.Add();
            column.ColumnName = "externalIdOzn";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Odcitek";
            column.DataType = typeof(decimal);

            column = table.Columns.Add();
            column.ColumnName = "IdEnote";
            column.DataType = typeof(int);

            column = table.Columns.Add();
            column.ColumnName = "vpis_datetime";
            column.DataType = typeof(DateTime);

            return table;
        }


    }
}
