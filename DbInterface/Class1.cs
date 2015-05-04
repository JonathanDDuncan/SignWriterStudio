using System;
using System.Collections.Generic;
using System.IO;
using DbTags;
using SQLiteRepository.SQLiteQuery;

namespace DbInterface
{
    public class DbInterface1
    {
        public static void AddDoNotExportTags(string path)
        {
            var entriesIsPrivate = DbDictionary.GetDictionaryEntries(path, " isPrivate ");
              AddTags(path, entriesIsPrivate);
        }

        private static void AddTags(string path, IQueryResult entriesIsPrivate)
        {
            var valuesToInsert =new List<List<string>>();
            foreach (var tabularResult  in entriesIsPrivate.TabularResults)
            {
                var result = tabularResult as IDictionary<String, object>;
                if (result != null)
                {
                    var idDictionary = result["IDDictionary"].ToString();
                    valuesToInsert.Add(new List<string>
                    {
                        idDictionary,
                        "b9e38963-59e4-4878-ad68-922911dcce17",
                        Guid.NewGuid().ToString()
                    });
                }
               
            }
            var columns = new List<string> {"IDDictionary", "IdTag", "IdTagDictionary"};

            DbTags.tb1.Insert(path, columns, valuesToInsert);
        }
    }
}
