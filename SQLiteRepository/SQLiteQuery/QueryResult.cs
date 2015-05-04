using System.Collections.Generic;
using System.Dynamic;

namespace SQLiteRepository.SQLiteQuery
{
    public class QueryResult : IQueryResult
    { 
        public int AffectedRows { get; set; }
        public List<object> ScalarResults { get; set; }
      
        public List<List<ExpandoObject>> TabularResults { get; set; }
        public List<string> SqlCommands { get; set; }

    }
}
