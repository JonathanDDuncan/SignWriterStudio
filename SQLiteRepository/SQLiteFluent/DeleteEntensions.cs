using System;
using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public static class DeleteEntensions
    {

        public static DeleteFluent Table(this DeleteFluent query, string tableName)
        {
            query.TableName = tableName;
            return query;
        }


        public static DeleteFluent Path(this DeleteFluent query, string path)
        {
            query.Path = path;
            return query;
        }

        public static DeleteFluent Where(this DeleteFluent query, string where)
        {
            query.Where  = where;
            return query;
        }
        public static DeleteFluent Where(this DeleteFluent query, Tuple<string,List<string>> wherein)
        {
            query.WhereIn = wherein;
            return query;
        }
       
    }
}