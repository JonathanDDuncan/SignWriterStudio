using System;
using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
 public static class GetEntensions
    {
      
        public static GetFluent Table(this GetFluent query, string tableName)
        {
            query.TableName = tableName;
            return query;
        }
        public static GetFluent Columns(this GetFluent query, List<string> columns)
        {
            query.Columns = columns;
            return query;
        }

        public static GetFluent Path(this GetFluent query, string path)
        {
            query.Path = path;
            return query;
        }

        public static GetFluent Where(this GetFluent query, Dictionary<string, Tuple<string, string>> where)
        {
            query.Where = where;
            return query;
        }

        public static GetFluent OrderBy(this GetFluent query, List<string> orderBy)
        {
            query.OrderBy = orderBy;
            return query;
        }
    }
}