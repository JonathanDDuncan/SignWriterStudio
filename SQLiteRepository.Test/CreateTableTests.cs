using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLiteRepository.SQLiteQuery;

namespace SQLiteRepository.Test
{
    [TestClass]
    public class CreateTableTests
    {
        [TestMethod]
        public void CreateTable()
        {
            var path = Path.GetTempFileName();
            const string tableName = "MyNewTable";
            var fields = new Dictionary<string, string>
            {
                {"PrimaryKey", "int"},
                {"SecondField", "TEXT"},
                {"ThirdField", "TEXT"}
            };

            var query = new CreateTableQuery
            {
                Path = path,
                TableName = tableName,
                PrimaryKey = "PrimaryKey",
                Fields = fields
            };

            query.Execute();

            var tableQuery = new GetQuery{ TableName = tableName, Path = path  };

            var tableResult = tableQuery.Execute();

            Assert.AreEqual(1, tableResult.TabularResults.Count());
        }

    }
}
