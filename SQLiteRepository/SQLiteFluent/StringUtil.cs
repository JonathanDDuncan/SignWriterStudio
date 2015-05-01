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
            return selectitems == null ? " * " : ConcatValues(selectitems, "[", "]", ", ");
        }

        public static List<string> GetColumnNames(IDataReader reader)
        {
            var schemaTable = reader.GetSchemaTable();
            return schemaTable != null ? (from DataRow row in schemaTable.Rows from DataColumn column in schemaTable.Columns where column.ColumnName == "ColumnName" select row[column].ToString()).ToList() : null;
        }

        public static string GetValues(IEnumerable<string> row)
        {
            return ConcatValues(row, "'", "'", ", ");
        }

        private static string ConcatValues(IEnumerable<string> row, string prepend, string append, string seperator)
        {
            return Concat(row.Select(x => prepend + x + append),seperator);
        }

        internal static string Concat(IEnumerable<string> row, string seperator)
        {
            return row.Aggregate((current, next) => current + seperator + next);
        }
        public static string Concatenate(IEnumerable<string> items)
        {
            return items.Aggregate((current, next) => current + next);
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

        

        public static string RemoveLastChar(string str)
        {
            return str.Substring(0, str.Length - 1);
        }

        public static string ConcatenateColumnTypes(Dictionary<string, string> fields)
        {
           return Concat(fields.Select(x => "[" + x.Key + "] " + x.Value),  ", ");
        }

       
    }
}