using System.Collections.Generic;
using System.Dynamic;

namespace SQLiteRepository.SQLiteQuery
{
    public interface IQueryResult
    {
        int AffectedRows { get; set; }
        List<object> ScalarResults { get; set; }
        List<List<ExpandoObject>> TabularResults { get; set; }
        List<string> SqlCommands { get; set; }
    }
}