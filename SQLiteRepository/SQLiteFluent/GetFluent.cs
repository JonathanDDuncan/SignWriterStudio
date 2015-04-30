using System;
using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public class GetFluent
    {
        public static GetFluent Initialize()
        {
            var query = new GetFluent();

            return query;
        }

        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public string Path { get; set; }
        public Dictionary<string, Tuple<string, string>> Where { get; set; }
        public List<string> OrderBy { get; set; }
    }
}
