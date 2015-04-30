using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;

namespace SQLiteRepository.SQLiteFluent
{
    public class GetSqlite
    {
        private static IFluentResult  GetData(string path, string tableName, IEnumerable<string> columns = null, Dictionary<string, Tuple<string, string>> where = null, List<string> orderBy = null)
        {
            var conn = CommonSqlite.CreateConnection(path);
            var columnNames = StringUtil.GetSelectNames(columns);
            var sql = string.Format("select {0} from {1}", columnNames, tableName);
            conn.Open();
            var command = new SQLiteCommand(sql, conn);

            List<ExpandoObject> table = null;
            using (var rdr = command.ExecuteReader())
            {
                var readColumns = StringUtil.GetColumnNames(rdr);
                if (readColumns != null)
                {
                    table = ReadRows(rdr, readColumns);
                }
            }
            conn.Close();
            return new FluentResult {TabularResults = new List<List<ExpandoObject>> {table}};
        }

        private static List<ExpandoObject> ReadRows(SQLiteDataReader rdr, List<string> readColumns)
        {
            var table = new List<ExpandoObject>(); ;
            while (rdr.Read())
            {
                dynamic newRow = new ExpandoObject();
                var row = newRow as IDictionary<String, object>;
                foreach (var columnName in readColumns)
                {
                    row[columnName] = rdr[columnName];
                }
                table.Add(newRow);
            }
            return table;
        }

        public static IFluentResult GetData(GetFluent query)
        {
            var result = GetData(query.Path, query.TableName, query.Columns, query.Where, query.OrderBy);
            return result;
        }
    }
}