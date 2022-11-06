using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        #region Tables

        public virtual DbSet<Mobile_Phone> Mobiles { get; set; }

        //public DbSet<Laptop> Laptops { get; set; }

        #endregion

        public RepositoryContext()
        {

        }

        
        public RepositoryContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);
            //base.OnModelCreating(modelBuilder);
        }
    }

    
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            try
            {
                var connectionString = @"Data Source=MELCHOR;Initial Catalog=HumanResourcesDB;User ID=sa;Password=americanpie248";
                optionsBuilder.UseSqlServer(connectionString);
            }
            catch (Exception)
            {
                //handle errror here.. means DLL has no sattelite configuration file.
            }

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
