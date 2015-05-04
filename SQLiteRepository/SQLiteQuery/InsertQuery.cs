using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteRepository.SQLiteQuery
{
    public class InsertQuery : ISqliteQuery 
    {
        public string TableName { get; set; }
        public List<List<string>> Values { get; set; }
        public string Path { get; set; }
        public List<string> Columns { get; set; }
        public IQueryResult Execute()
        {
            return InsertSqlite.Insert(this);
        }
        public IQueryResult Execute(SQLiteConnection conn, SQLiteTransaction tr)
        {
            return InsertSqlite.Insert(conn,tr, this);
        }
    }
}
