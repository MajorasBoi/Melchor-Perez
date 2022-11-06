using Microsoft.VisualStudio.TestTools.UnitTesting;
using Devices;
using Repository;
using System;

namespace TestRepository
{
    [TestClass]
    public class MobileRepositoryTests
    {
        IMobileRepository _repository;

        public MobileRepositoryTests()
        {
            var connectionString = @"Data Source=MELCHOR;Initial Catalog=HumanResourcesDB;User ID=sa;Password=americanpie248";
            _repository = new DBRepository(connectionString);
        }

        [TestMethod]
        public void Can_CreateMobile_Test()
        {
            // arrange
            EBrand brand = EBrand.Huawei;
            float price = 200;
            DateTime date = DateTime.Now;
            EMobileDataGen mdg = EMobileDataGen.LTE;
            short cr = 12;
            EMobileStorage ms = EMobileStorage._256gB;
            
            // act
            _repository.BeginTransaction();
            var mobile = _repository.CreateMobile(brand, price, date, mdg, cr, ms);
            _repository.CommitTransaction();

            // assert
            Assert.IsNotNull(mobile);
            Assert.AreNotEqual(0, mobile.Price);
            Assert.AreEqual(brand, mobile.Brand);
            Assert.AreEqual(ms, mobile.Storage);
        }
    }
}