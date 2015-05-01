using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace SQLiteRepository.SQLiteFluent
{
    public class CreateTableSqlite
    {
        public static IFluentResult CreateTable(CreateTableFluent query)
        {
            return CreateTable(query.Path, query.TableName, query.PrimaryKey, query.Fields);
        }

        private static IFluentResult CreateTable(string path, string tableName, string primaryKey, Dictionary<string, string> fields)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();

            var columnTypes = StringUtil.ConcatenateColumnTypes(fields);
            int totalRowsAffected;
            string commandText;
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    string sql = string.Format("create table if not exists {0} ({1});", tableName, columnTypes);
                    var command = new SQLiteCommand(sql, conn) { Transaction = tr };
                    commandText = command.CommandText;
                    totalRowsAffected = command.ExecuteNonQuery();

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


            return new FluentResult { AffectedRows = totalRowsAffected, SqlCommands = new List<string> { commandText } };
      
        }

         
    }
}