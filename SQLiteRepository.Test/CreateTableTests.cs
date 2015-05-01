using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLiteRepository.SQLiteFluent;

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
            
            var query = CreateTableFluent.Initialize()
                .Path(path)
                .Table(tableName)
                .PrimaryKey("PrimaryKey") 
                .Fields(fields);

            CreateTableSqlite.CreateTable(query);

            var tableQuery = GetFluent.Initialize().Table(tableName).Path(path);

            var tableResult = GetSqlite.GetData(tableQuery);

            Assert.AreEqual(1, tableResult.TabularResults.Count());
        }

    }
}
