using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteRepository.SQLiteQuery
{
    public class CreateTableQuery: ISqliteQuery 
    { 
        public string TableName { get; set; }
        public string Path { get; set; }
        public Dictionary<string, string> Fields { get; set; }
        public string PrimaryKey { get; set; }
        public IQueryResult Execute()
        {
            return CreateTableSqlite.CreateTable(this);
        }
        public IQueryResult Execute(SQLiteConnection conn, SQLiteTransaction tr)
        {
            return CreateTableSqlite.CreateTable(conn, tr, this);
        }


    }
}
