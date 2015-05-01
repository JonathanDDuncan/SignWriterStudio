using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public class InsertFluent
    {
        public static InsertFluent Initialize()
        {
            var query = new InsertFluent();

            return query;
        }

        public string TableName { get; set; }
        public List<List<string>> Values { get; set; }
        public string Path { get; set; }
        public List<string> Columns { get; set; }
    }
}
