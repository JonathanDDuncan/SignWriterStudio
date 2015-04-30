using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public static class UpdateEntensions
    {

        public static UpdateFluent Table(this UpdateFluent query, string tableName)
        {
            query.TableName = tableName;
            return query;
        }

        public static UpdateFluent Path(this UpdateFluent query, string path)
        {
            query.Path = path;
            return query;
        }
        public static UpdateFluent Values(this UpdateFluent query, List<List<object>> values)
        {
            query.Values = values;
            return query;
        }

        public static UpdateFluent Columns(this UpdateFluent query, List<string> columns)
        {
            query.Columns = columns;
            return query;
        }
       
    }
}