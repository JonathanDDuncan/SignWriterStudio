using System;
using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public class DeleteFluent
    {
        public static DeleteFluent Initialize()
        {
            var query = new DeleteFluent();

            return query;
        }

        public string TableName { get; set; }
        public string Path { get; set; }
        public string Where { get; set; }
        public Tuple<string,List<string>> WhereIn { get; set; }
    }
}
