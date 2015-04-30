using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace SQLiteRepository.SQLiteFluent
{
    public class UpdateSqlite
    {
        public static IFluentResult Update(UpdateFluent query)
        {
            return Update(query.Path, query.TableName, query.Columns, query.Values);
        }

        private static IFluentResult Update(string path, string tableName, IEnumerable<string> columns, IEnumerable<List<object>> rows)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();
            var totalRowsAffected = 0;
            var commandList = new List<string>();
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    int sum = 0;
                    foreach (List<object> row in rows)
                    {
                        string values = StringUtil.GetUpdateValues(columns, row);
                        string @where = StringUtil.GetUpdateWhere(columns, row);
                        string sql = string.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, @where);
                        SQLiteCommand command = new SQLiteCommand(sql, conn) {Transaction = tr};
                        commandList.Add(command.CommandText);
                        sum += command.ExecuteNonQuery();
                    }
                    totalRowsAffected += sum;
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


            return new FluentResult { AffectedRows = totalRowsAffected, SqlCommands = commandList };
      
        }
    }
}