using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteRepository.SQLiteFluent
{
    public class DeleteSqlite
    {
        public static IFluentResult Delete(DeleteFluent query)
        {
            return Delete(query.Path, query.TableName, query.Where, query.WhereIn );
        }

        private static IFluentResult  Delete(string path, string tableName, string @where, Tuple<string,List<object>> whereIn = null)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();

            var whereclause = CommonSqlite.GetWhereClause(@where, whereIn);
            int totalRowsAffected;
            string commandText;
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    string sql = string.Format("DELETE FROM {0}{1};", tableName, whereclause);
                    var command = new SQLiteCommand(sql, conn) { Transaction = tr };
                    commandText = command.CommandText;
                    totalRowsAffected  = command.ExecuteNonQuery();
 
                    tr.Commit();
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


            return new FluentResult {AffectedRows = totalRowsAffected, SqlCommands = new List<string> {commandText}};
        }
    }
}