using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SQLiteRepository.SQLiteQuery;

namespace SQLiteRepository.SQLiteQuery
{
    public class DeleteSqlite
    {


        private static IQueryResult Delete(string path, string tableName, string @where, Tuple<string, List<string>> whereIn = null)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();

            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    var result = Delete(conn, tr, tableName, @where, whereIn);
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

        private static IQueryResult Delete(SQLiteConnection conn, SQLiteTransaction tr, string tableName, string @where, Tuple<string, List<string>> whereIn = null)
        {
            var whereclause = CommonSqlite.GetWhereClause(@where, whereIn);
            var sql = string.Format("DELETE FROM {0}{1};", tableName, whereclause);
            var command = new SQLiteCommand(sql, conn) { Transaction = tr };
            var commandText = command.CommandText;
            var totalRowsAffected = command.ExecuteNonQuery();

            return new QueryResult { AffectedRows = totalRowsAffected, SqlCommands = new List<string> { commandText } };
        }

        public static IQueryResult Delete(SQLiteConnection conn, SQLiteTransaction tr, DeleteQuery query)
        {
            return Delete(conn, tr, query.TableName, query.Where, query.WhereIn);
        }

        public static IQueryResult Delete(DeleteQuery query)
        {
            return Delete(query.Path, query.TableName, query.Where, query.WhereIn);
        }
    }
}