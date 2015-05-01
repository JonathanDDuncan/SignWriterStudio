using System;
using System.Collections.Generic;

namespace SQLiteRepository.SQLiteFluent
{
    public static class CreateTableEntensions  
    {

        public static CreateTableFluent Table(this CreateTableFluent query, string tableName)
        {
            query.TableName = tableName;
            return query;
        }


        public static CreateTableFluent Path(this CreateTableFluent query, string path)
        {
            query.Path = path;
            return query;
        }

        public static CreateTableFluent PrimaryKey(this CreateTableFluent query, string primaryKey)
        {
            query.PrimaryKey = primaryKey;
            return query;
        }

        public static CreateTableFluent PrimaryKey(this CreateTableFluent query, Dictionary<string, string> fields)
        {
            query.Fields = fields;
            return query;
        }
    }
     
}