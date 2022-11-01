using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Persistence.Database.Models;

namespace Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MELCHOR;Database=Music;Trusted_Connection=True;");
        }
    }
}