using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace SQLiteRepository.SQLiteFluent
{
    public class InsertSqlite
    {
        public static IFluentResult Insert(InsertFluent query)
        {
            var conn = CommonSqlite.CreateConnection(query.Path);
            var result = Insert(conn, query.TableName, query.Columns, query.Values);
            return result;
        }

        private static IFluentResult Insert(SQLiteConnection conn, string tableName, IEnumerable<string> columns, IEnumerable<List<object>> rows)
        {
            var columnNames = StringUtil.GetSelectNames(columns);
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
                        string values = StringUtil.GetValues(row);
                        string sql = string.Format("INSERT INTO {0} ({1})  VALUES ({2});", tableName, columnNames, values);
                        SQLiteCommand command = new SQLiteCommand(sql, conn);

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


            return new FluentResult { AffectedRows = totalRowsAffected, SqlCommands =  commandList  };
       
        }
    }
}