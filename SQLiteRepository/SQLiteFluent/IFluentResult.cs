using System.Collections.Generic;
using System.Dynamic;

namespace SQLiteRepository.SQLiteFluent
{
    public interface IFluentResult
    {
        int AffectedRows { get; set; }
        List<object> ScalarResults { get; set; }
        List<List<ExpandoObject>> TabularResults { get; set; }
        List<string> SqlCommands { get; set; }
    }
}