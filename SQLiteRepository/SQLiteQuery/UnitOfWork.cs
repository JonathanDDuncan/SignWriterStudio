using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using SQLiteRepository.SQLiteQuery;

namespace SQLiteRepository.SQLiteQuery
{
    public class UnitOfWork
    {
        private readonly List<ISqliteQuery> _queries = new List<ISqliteQuery>();
        public List<ISqliteQuery> Queries
        {
            get { return _queries; }
        }

        public List<IQueryResult> Execute()
        {
            var cumresult = new List<IQueryResult>();

            var conn = GetConnection(_queries);
            conn.Open();
            using (var tr = conn.BeginTransaction())
            {
                try
                {
                    foreach (var sqliteQuery in _queries)
                    {
                        var result = sqliteQuery.Execute(conn,tr);
                       cumresult.Add(result);
                    }
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


            return cumresult;
        }


        private static SQLiteConnection GetConnection(IEnumerable<ISqliteQuery> queries)
        {
            var firstQuery = queries.FirstOrDefault();

            if (firstQuery == null) return null;
            var conn = CommonSqlite.CreateConnection(firstQuery.Path);
            return conn;
        }
    }
}
