using Microsoft.VisualStudio.TestTools.UnitTesting;
using Devices;
using System;
using Tech_Store;

namespace TestProject
{
    [TestClass]
    public class TestDevices
    {
        [TestMethod]
        public void Test_CreateAMobilePhone()
        {
            EBrand brand = EBrand.LG;
            float price = 200;
            DateTime date = DateTime.Now;
            EMobileDataGen mdg = EMobileDataGen.LTE;
            short cr = 12;
            EMobileStorage storage = EMobileStorage._256gB;

            Mobile_Phone mobile = new Mobile_Phone(brand, price, date, mdg, cr, storage);

            Assert.AreEqual(brand, mobile.Brand);
            Assert.AreEqual(price, mobile.Price);
            Assert.AreEqual(date, mobile.Date);
            Assert.AreEqual(mdg, mobile.MobileDataGen);
            Assert.AreEqual(cr, mobile.CameraRes);
            Assert.AreEqual(storage, mobile.Storage);
        }

    }


    [TestClass]
    public class TestTechStore
    {
        [TestMethod]
        public void Test_CreateATechStore()
        {
            EBrand brand = EBrand.LG;
            float price = 200;
            DateTime date = DateTime.Now;
            EMobileDataGen mdg = EMobileDataGen.LTE;
            short cr = 12;
            EMobileStorage storage = EMobileStorage._256gB;

            Mobile_Phone mobile = new Mobile_Phone(brand, price, date, mdg, cr, storage);
            Store tech = new Store();

            tech.AddMobilePhone(mobile);

            Assert.AreEqual(mobile, tech.GetElementAt(0));

        }
    }
}