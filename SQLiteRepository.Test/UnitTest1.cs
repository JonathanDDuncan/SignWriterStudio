using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLiteRepository.SQLiteQuery;

namespace SQLiteRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        private string _path = @"C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (7).SWS";

        [TestMethod]
        public void GetData()
        {
           

            var selectColumns = new List<string> { "IDDictionary" };

            var query = new GetQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Columns = selectColumns
            };
            var result = GetSqlite.GetData(query);

            var firstOrDefault = result.TabularResults.FirstOrDefault();
            var actual = 0;
            if (firstOrDefault != null)
            {
                actual = firstOrDefault.Count;
            }

            Assert.AreEqual(1279, actual);
        }

        [TestMethod]
        public void InsertData()
        {
            
            var insertColumns = new List<string> { "IDDictionary", "IDSignLanguage", "GUID" };

            var values = new List<List<string>>
            {
                new List< string> {"112555" , "1" , Guid.NewGuid().ToString()} ,
                new List< string> {"112556" , "1" , Guid.NewGuid().ToString()} ,
                new List< string> {"112557" , "1" , Guid.NewGuid().ToString()}  
            };

            var query = new InsertQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Columns = insertColumns,
                Values = values
            };
            var result = query.Execute();

            Assert.AreEqual(3, result.AffectedRows);
        }

        [TestMethod]
        public void UpdateData()
        {
            
            var updateColumns = new List<string> { "IDDictionary", "IDSignLanguage", "GUID" };

            var newValues = new List<List<object>>
            {
                new List< object> {112555 , 1 , Guid.NewGuid()} ,
                new List< object> {112556 , 1 , Guid.NewGuid()} ,
                new List< object> {112557 , 1 , Guid.NewGuid()}  
            };

            var query = new UpdateQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Columns = updateColumns,
                Values = newValues
            };
            var result = query.Execute();

            Assert.AreEqual(3, result.AffectedRows);
        }

        [TestMethod]
        public void DeleteData()
        {
            
            var where = "[IDDictionary] = '112557'";

            var query = new DeleteQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Where = where
            };
            var result = query.Execute();

            Assert.AreEqual(1, result.AffectedRows);
        }

        [TestMethod]
        public void DeleteDataWhereIn()
        {
            
            var wherein = Tuple.Create("IDDictionary", new List<string> { "112555", "112556", "112557" });


            var query = new DeleteQuery
            {
                Path = _path,
                TableName = "Dictionary",
                WhereIn = wherein
            };

            var result = query.Execute();
            Assert.AreEqual(3, result.AffectedRows);
        }

        [TestMethod]
        public void UnitOfWork()
        {

            var insertColumns = new List<string> { "IDDictionary", "IDSignLanguage", "GUID" };

            var values = new List<List<string>>
            {
                new List< string> {"112555" , "1" , Guid.NewGuid().ToString()} ,
                new List< string> {"112556" , "1" , Guid.NewGuid().ToString()} ,
                new List< string> {"112557" , "1" , Guid.NewGuid().ToString()}  
            };

            var insertquery = new InsertQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Columns = insertColumns,
                Values = values
            };

            var wherein = Tuple.Create("IDDictionary", new List<string> { "112555", "112556", "112557" });

            var updateColumns = new List<string> { "IDDictionary", "IDSignLanguage", "GUID" };

            var newValues = new List<List<object>>
            {
                new List< object> {112555 , 1 , Guid.NewGuid()} ,
                new List< object> {112556 , 1 , Guid.NewGuid()} ,
                new List< object> {112557 , 1 , Guid.NewGuid()}  
            };

            var updatequery = new UpdateQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Columns = updateColumns,
                Values = newValues
            };
            var deletequery = new DeleteQuery
            {
                Path = _path,
                TableName = "Dictionary",
                WhereIn = wherein
            };
            var selectColumns = new List<string> { "IDDictionary" };

            var getquery = new GetQuery
            {
                Path = _path,
                TableName = "Dictionary",
                Columns = selectColumns
            };
            var unitofWork = new UnitOfWork();
           
            unitofWork.Queries.Add(insertquery);
            unitofWork.Queries.Add(updatequery);
            unitofWork.Queries.Add(deletequery);
            unitofWork.Queries.Add(getquery);

            var result = unitofWork.Execute();

            Assert.AreEqual(4, result.Count());
        }
    }
}
