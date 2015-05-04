using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLiteRepository.SQLiteQuery
{
    public class GetQuery : ISqliteQuery 
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public string Path { get; set; }
        public string Where { get; set; }
        public Tuple<string, List<string>> WhereIn { get; set; }
        public List<string> OrderBy { get; set; }
        public IQueryResult Execute()
        {
            return GetSqlite.GetData(this);
        }
        public IQueryResult Execute(SQLiteConnection conn, SQLiteTransaction tr)
        {
            return GetSqlite.GetData(conn, tr, this);
        }
    }
}
