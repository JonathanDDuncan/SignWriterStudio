using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace SQLiteRepository.SQLiteQuery 
{
    public class InsertSqlite
    {
      

        private static IQueryResult Insert(string path, string tableName, IEnumerable<string> columns, IEnumerable<List<string>> rows)
        {
            var conn = CommonSqlite.CreateConnection(path);
           
            conn.Open();
           
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    var result = Insert(conn, tr, tableName, columns, rows);
                   
                 
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
        private static IQueryResult Insert(SQLiteConnection conn, SQLiteTransaction tr, string tableName, IEnumerable<string> columns, IEnumerable<List<string>> rows)
        {
            var columnNames = StringUtil.GetSelectNames(columns);
           
            var totalRowsAffected = 0;
            var commandList = new List<string>();
           

                    int sum = 0;
                    foreach (List<string> row in rows)
                    {
                        var values = StringUtil.GetValues(row);
                        var sql = string.Format("INSERT INTO {0} ({1})  VALUES ({2});", tableName, columnNames, values);
                        var command = new SQLiteCommand(sql, conn);

                        commandList.Add(command.CommandText);
                        sum += command.ExecuteNonQuery();
                    }
                    totalRowsAffected += sum;
                  

         


            return new QueryResult { AffectedRows = totalRowsAffected, SqlCommands = commandList };

        }
        public static IQueryResult Insert(InsertQuery query)
        {
          
            var result = Insert(query.Path, query.TableName, query.Columns, query.Values);
            return result;
        }

        public static IQueryResult Insert(SQLiteConnection conn, SQLiteTransaction tr, InsertQuery query)
        {
            var result = Insert(conn, tr, query.TableName, query.Columns, query.Values);
            return result;
        }
    }
}