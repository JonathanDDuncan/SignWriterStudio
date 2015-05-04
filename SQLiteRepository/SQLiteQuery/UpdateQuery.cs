using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteRepository.SQLiteQuery
{
    public class UpdateQuery : ISqliteQuery 
    {
        public string TableName { get; set; }
        public List<List<object>> Values { get; set; }
        public string Path { get; set; }
        public List<string> Columns { get; set; }
        public IQueryResult Execute()
        {
            return UpdateSqlite.Update(this);
        }
        public IQueryResult Execute(SQLiteConnection conn, SQLiteTransaction tr)
        {
            return UpdateSqlite.Update(conn,tr, this);
        }
    }
}
