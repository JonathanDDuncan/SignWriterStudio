using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public static class InsertEntensions
    {

        public static InsertFluent Table(this InsertFluent query, string tableName)
        {
            query.TableName = tableName;
            return query;
        }
        
        public static InsertFluent Path(this InsertFluent query, string path)
        {
            query.Path = path;
            return query;
        }
        public static InsertFluent Values(this InsertFluent query, List<List<string>> values)
        {
            query.Values = values;
            return query;
        }

        public static InsertFluent Columns(this InsertFluent query, List<string> columns)
        {
            query.Columns = columns;
            return query;
        }
       
    }
}