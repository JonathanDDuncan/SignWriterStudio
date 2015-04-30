using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SQLiteRepository.SQLiteFluent
{
    public class StringUtil
    {
        public static string GetSelectNames(IEnumerable<string> selectitems)
        {
            return ConcatValues(selectitems, "[", "]", ", ");
        }

        public static List<string> GetColumnNames(IDataReader reader)
        {
            var schemaTable = reader.GetSchemaTable();
            return schemaTable != null ? (from DataRow row in schemaTable.Rows from DataColumn column in schemaTable.Columns where column.ColumnName == "ColumnName" select row[column].ToString()).ToList() : null;
        }

        public static string GetValues(IEnumerable<object> row)
        {
            return ConcatValues(row, "'", "'", ", ");
        }

        private static string ConcatValues(IEnumerable<object> row, string prepend, string append, string seperator)
        {
            return row.Select(x => prepend + x + append).Aggregate((current, next) => current + seperator + next);
        }

        public static string GetUpdateValues(IEnumerable<string> columnNames, IEnumerable<object> values)
        {
            return columnNames.Skip(1).Zip(values.Skip(1), Tuple.Create).Select(x => "[" + x.Item1 + "] = '" + x.Item2 + "'").Aggregate((current, next) => current + ", " + next);
        }

        public static string GetUpdateWhere(IEnumerable<string> columns, IEnumerable<object> row)
        {
            var columnName = columns.FirstOrDefault();
            var id = row.FirstOrDefault();

            if (columnName != null && id != null) return "[" + columnName + "] = '" + id + "'";
            return null;
        }

        public static string Concatenate(List<object> list)
        {
          return  ConcatValues(list, "", "", ", ");
        }
    }
}