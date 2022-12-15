using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Management;
using Models;

namespace TestProject
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void Test_Management()
        {
            DBRepository tdb = new DBRepository();
            try
            {
                tdb.CreateBucket().Wait();
            }
            catch
            {
                Console.WriteLine("Not able to create. Try deleting");
                InvalidOperationException ex= null;
                throw ex;
            }
            
        }

        [TestMethod]
        public void Test_WriteBook()
        {
            DBRepository tdb = new DBRepository();

            Book book = new Book(TCondition.Excellent, "Hello", 000915, "Sade", 200, DateTime.UtcNow, 
                                "Belkis", TGenre.Tails, "24");

            try
            {
                tdb.WriteData(book).Wait();
            }
            catch
            {
                Console.Write("Not able to write point.");
                InvalidOperationException ex = null;
                throw ex;
            }
            
        }

        [TestMethod]
        public void Test_WriteMagazine()
        {
            DBRepository tdb = new DBRepository();

            Magazine magazine = new Magazine(TCondition.Good, "Rolling Stones", 010731, "Rolling Stones", 
                                        10, DateTime.UtcNow, 2, DateTime.Now);

            try
            {
                tdb.WriteData(magazine).Wait();
            }
            catch
            {
                Console.Write("Not able to write point.");
                InvalidOperationException ex = null;
                throw ex;
            }
        }

        [TestMethod]
        public void Test_QueryAll()
        {
            DBRepository tdb = new DBRepository();

            tdb.QueryAll();
        }

        [TestMethod]
        public void Test_QueryByMeasurement()
        {
            string measurement = "book";
            DBRepository tdb = new DBRepository();

            tdb.QueryByMeasurement(measurement);
        }
    }
}