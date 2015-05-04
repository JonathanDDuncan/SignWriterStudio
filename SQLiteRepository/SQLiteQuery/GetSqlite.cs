using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;

namespace SQLiteRepository.SQLiteQuery
{
    public class GetSqlite
    {
        private static IQueryResult  GetData(string path, string tableName, IEnumerable<string> columns = null, string @where = null, Tuple<string, List<string>> whereIn = null, List<string> orderBy = null)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();
             

            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    var result = GetData(conn, tr, tableName,  columns,   @where, whereIn,   orderBy);
                    tr.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private static IQueryResult GetData(SQLiteConnection conn, SQLiteTransaction tr, string tableName, IEnumerable<string> columns = null, string @where = null, Tuple<string, List<string>> whereIn = null, List<string> orderBy = null)
        {
            var columnNames = StringUtil.GetSelectNames(columns);
            var whereclause = CommonSqlite.GetWhereClause(@where, whereIn);
            var sql = string.Format("select {0} from {1}{2}", columnNames, tableName, whereclause);

            var command = new SQLiteCommand(sql, conn) { Transaction = tr };

            List<ExpandoObject> table = null;
            using (var rdr = command.ExecuteReader())
            {
                var readColumns = StringUtil.GetColumnNames(rdr);
                if (readColumns != null)
                {
                    table = ReadRows(rdr, readColumns);
                }
            }
            return new QueryResult { TabularResults = new List<List<ExpandoObject>> { table } };
        }
        private static List<ExpandoObject> ReadRows(IDataReader rdr, List<string> readColumns)
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

        public static IQueryResult GetData(GetQuery query)
        {
            var result = GetData(query.Path, query.TableName, query.Columns, query.Where, query.WhereIn, query.OrderBy);
            return result;
        }

        public static IQueryResult GetData(SQLiteConnection conn, SQLiteTransaction tr, GetQuery query)
        {
            var result = GetData(conn, tr, query.TableName, query.Columns, query.Where, query.WhereIn, query.OrderBy);
            return result;
        }
    }
}