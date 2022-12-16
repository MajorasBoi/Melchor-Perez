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
        public void Test_AddTemperaturePoint()
        {
            DBRepository tdb = new DBRepository();

            Sensor sensor = new Sensor(EType.Temperature, EBrand.Siemens, 15, "ABC", DateTime.UtcNow, 5);

            try
            {
                tdb.WriteData(sensor).Wait();
            }
            catch
            {
                Console.Write("Not able to write point.");
                InvalidOperationException ex = null;
                throw ex;
            }
            
        }

        [TestMethod]
        public void Test_AddHumidityPoint()
        {
            DBRepository tdb = new DBRepository();

            Sensor sensor = new Sensor(EType.Humidity, EBrand.EndressHauser, 10, "DEF", DateTime.UtcNow, 5);

            try
            {
                tdb.WriteData(sensor).Wait();
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
            string measurement = "Temperature";
            DBRepository tdb = new DBRepository();

            tdb.QueryByMeasurement(measurement);
        }

        [TestMethod]
        public void Test_QueryById()
        {
            int id = 5;
            DBRepository tdb = new DBRepository();

            tdb.QueryById(id);
        }

        [TestMethod]
        public void Test_DeleteAll()
        {
            string bucket = "Data Center";
            string organization = "Development";
            DBRepository tdb = new DBRepository();

            tdb.DeleteData(bucket, organization);
        }
    }
}