using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public class UpdateFluent
    {
        public static UpdateFluent Initialize()
        {
            var query = new UpdateFluent();

            return query;
        }

        public string TableName { get; set; }
        public List<List<object>> Values { get; set; }
        public string Path { get; set; }
        public List<string> Columns { get; set; }
    }
}
