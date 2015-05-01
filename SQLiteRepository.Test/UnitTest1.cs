using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLiteRepository.SQLiteFluent;

namespace SQLiteRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetData()
        {
            var path = @"C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (7).SWS";


            var selectColumns = new List<string> { "IDDictionary" };

            var query = GetFluent.Initialize()
                .Path(path)
                .Table("Dictionary")
                .Columns(selectColumns);

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
            var path = @"C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (7).SWS";

            var insertColumns = new List<string> { "IDDictionary", "IDSignLanguage", "GUID" };

            var values = new List<List<string>>
            {
                new List< string> {"112555" , "1" , Guid.NewGuid().ToString()} ,
                new List< string> {"112556" , "1" , Guid.NewGuid().ToString()} ,
                new List< string> {"112557" , "1" , Guid.NewGuid().ToString()}  
            };

            var query = InsertFluent.Initialize()
                .Path(path)
                .Table("Dictionary")
                .Columns(insertColumns)
                .Values(values);

            var result = InsertSqlite.Insert(query);

            Assert.AreEqual(3, result.AffectedRows);
        }

        [TestMethod]
        public void UpdateData()
        {
            var path = @"C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (7).SWS";

            var updateColumns = new List<string> { "IDDictionary", "IDSignLanguage", "GUID" };

            var newValues = new List<List<object>>
            {
                new List< object> {112555 , 1 , Guid.NewGuid()} ,
                new List< object> {112556 , 1 , Guid.NewGuid()} ,
                new List< object> {112557 , 1 , Guid.NewGuid()}  
            };

            var query = UpdateFluent.Initialize()
                .Path(path)
                .Table("Dictionary")
                .Columns(updateColumns)
                .Values(newValues);

            var result = UpdateSqlite.Update(query);

            Assert.AreEqual(3, result.AffectedRows);
        }

        [TestMethod]
        public void DeleteData()
        {
            var path = @"C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (7).SWS";

            var where = "[IDDictionary] = '112557'";

            var query = DeleteFluent.Initialize()
                .Path(path)
                .Table("Dictionary")
                .Where(where);


            var result = DeleteSqlite.Delete(query);

            Assert.AreEqual(1, result.AffectedRows);
        }

        [TestMethod]
        public void DeleteDataWhereIn()
        {
            var path = @"C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (7).SWS";

            var wherein = Tuple.Create("IDDictionary", new List<string> { "112555", "112556", "112557" });


            var query = DeleteFluent.Initialize()
                .Path(path)
                .Table("Dictionary")
                .Where(wherein);


            var result = DeleteSqlite.Delete(query);

            Assert.AreEqual(3, result.AffectedRows );
        }
    }
}
