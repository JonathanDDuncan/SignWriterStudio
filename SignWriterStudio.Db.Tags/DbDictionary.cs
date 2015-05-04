using SQLiteRepository.SQLiteQuery;

namespace SignWriterStudio.Db.Tags
{
    public class DbDictionary
    {
        private const string TableName = "Dictionary";


        public static IQueryResult GetDictionaryEntries (string path, string where)
        {
            var query = DefaultQuery(path);
            query.Where = where;

            return query.Execute();

        }

        private static GetQuery DefaultQuery(string path)
        {
           return new GetQuery {Path = path, TableName = TableName};
        }


    }
}