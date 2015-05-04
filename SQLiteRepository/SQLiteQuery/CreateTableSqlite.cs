using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SQLiteRepository.SQLiteQuery;

namespace SQLiteRepository.SQLiteQuery
{
    public class CreateTableSqlite
    {
        public static IQueryResult CreateTable(string path, string tableName, string primaryKey, Dictionary<string, string> fields)
        {
            var conn = CommonSqlite.CreateConnection(path);
            conn.Open();
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    var result = CreateTable(conn, tr, tableName, fields);
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
        public static IQueryResult CreateTable(SQLiteConnection conn, SQLiteTransaction tr, CreateTableQuery query)
        {
            return CreateTable(conn, tr, query.TableName, query.Fields);
        }
        private static IQueryResult CreateTable(SQLiteConnection conn, SQLiteTransaction tr, string tableName, Dictionary<string, string> fields)
        {
            var columnTypes = StringUtil.ConcatenateColumnTypes(fields);
            var sql = string.Format("create table if not exists {0} ({1});", tableName, columnTypes);
            var command = new SQLiteCommand(sql, conn) {Transaction = tr};
            var commandText = command.CommandText;
            var totalRowsAffected = command.ExecuteNonQuery();
            return new QueryResult {AffectedRows = totalRowsAffected, SqlCommands = new List<string> {commandText}};
        }

        public static IQueryResult CreateTable(CreateTableQuery query)
        {
            return CreateTable(query.Path, query.TableName, query.PrimaryKey, query.Fields);
        }

       
    }
}