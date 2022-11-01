using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Database;
using Persistence.Database.Models;
using System;

namespace Test
{
    [TestClass]
    public class TestDb
    {
        [TestMethod]
        public void TestInsert()
        {
            using(var obj = new AppDbContext())
            {
                obj.Add(new Song
                {
                    Description = "Prueba",
                    Title = "The Trooper",
                    Duration = TimeSpan.FromSeconds(250)
                });

                obj.SaveChanges();
            }

        }
    }
}