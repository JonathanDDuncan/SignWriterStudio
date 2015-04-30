using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;

namespace SQLiteRepository.SQLiteFluent
{
    public class CommonSqlite
    {

        public static SQLiteConnection CreateConnection(string path)
        {
            return new SQLiteConnection(@"Data Source=" + path + ";Version=3;");
        }

        internal static object GetWhereClause(string @where, Tuple<string,List<object>> whereIn)
        {
            if (!string.IsNullOrEmpty( where))
            {
                return " WHERE " + where;
            }
            if (whereIn != null && whereIn.Item2.Any())
            {

                return " WHERE [" + whereIn.Item1 + "] IN (" + StringUtil.Concatenate(whereIn.Item2) + ")";
            }
            return string.Empty;
        }
    }
}
