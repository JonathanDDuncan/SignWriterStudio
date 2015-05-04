using System.Collections.Generic;
using SQLiteRepository.SQLiteQuery;

namespace SignWriterStudio.DbTags
{
    public static class DbTagsDictionary
    {
        private const string TableName = "TagDictionary";

        public static IQueryResult Insert(string path, List<string> columns, List<List<string>> valuesToInsert)
        {
            var query = DefaultInsertQuery(path);
            query.Columns = columns;
            query.Values = valuesToInsert;
            return query.Execute();
        }

        private static InsertQuery DefaultInsertQuery(string path)
        {
            return new InsertQuery { Path = path, TableName = TableName };
        }
    }
}