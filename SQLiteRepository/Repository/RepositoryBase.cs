using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteRepository.Repository
{
    class RepositoryBase
    {
        private TableDescription TableDescription { get; set; }

        public RepositoryBase(TableDescription tableDescription)
        {
            TableDescription = tableDescription;
        }

        public void CreateTable()
        {
            SQLiteFluent.CreateTableFluent.Initialize() ;


        }
    }
}
