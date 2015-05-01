using System;
using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public class CreateTableFluent
    {
        public static CreateTableFluent Initialize()
        {
            var query = new CreateTableFluent();

            return query;
        }

        public string TableName { get; set; }
        public string Path { get; set; }
        public Dictionary<string, string> Fields { get; set; }
        public string PrimaryKey { get; set; }
    }
}
