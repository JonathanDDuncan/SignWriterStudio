using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteRepository.Repository
{
    class TableDescription
    {
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public Dictionary<string, string> Fields { get; set; }
    }
}
