using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Management;

namespace TestProject
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestManagement()
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


    }
}