using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using SQLiteRepository.SQLiteQuery;

namespace SQLiteRepository.SQLiteQuery
{
    public class UpdateSqlite
    {

        private static IQueryResult Update(string path, string tableName, IEnumerable<string> columns, IEnumerable<List<object>> rows)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();
           
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    var result =  Update(conn, tr, tableName, columns, rows);
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

        private static IQueryResult Update(SQLiteConnection conn, SQLiteTransaction tr, string tableName, IEnumerable<string> columns, IEnumerable<List<object>> rows)
        {
            var totalRowsAffected = 0;
            var commandList = new List<string>();
            var columnNames = columns as IList<string> ?? columns.ToList();
            var sum = 0;
            foreach (var row in rows)
            {
                var values = StringUtil.GetUpdateValues(columnNames, row);
                var @where = StringUtil.GetUpdateWhere(columnNames, row);
                var sql = string.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, @where);
                var command = new SQLiteCommand(sql, conn) { Transaction = tr };
                commandList.Add(command.CommandText);
                sum += command.ExecuteNonQuery();
            }
            totalRowsAffected += sum;

            return new QueryResult { AffectedRows = totalRowsAffected, SqlCommands = commandList };
        }

        public static IQueryResult Update(UpdateQuery query)
        {
            return Update(query.Path, query.TableName, query.Columns, query.Values);
        }

        public static IQueryResult Update(SQLiteConnection conn, SQLiteTransaction tr, UpdateQuery query)
        {
            return Update(conn, tr, query.TableName, query.Columns, query.Values);
        }
    }
}