using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteRepository.SQLiteQuery
{
    public class DeleteQuery : ISqliteQuery 
    {
        public string TableName { get; set; }
        public string Path { get; set; }
        public string Where { get; set; }
        public Tuple<string, List<string>> WhereIn { get; set; }
        public IQueryResult Execute()
        {
            return DeleteSqlite.Delete(this);
        }
        public IQueryResult Execute(SQLiteConnection conn, SQLiteTransaction tr)
        {
            return DeleteSqlite.Delete(conn, tr, this);
        }
    }
}
