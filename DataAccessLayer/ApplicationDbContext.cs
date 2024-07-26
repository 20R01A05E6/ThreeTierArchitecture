using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YourNamespace.Models;

namespace YourNamespace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }
    }
}

