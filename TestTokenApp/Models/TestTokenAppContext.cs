using System;
// added these using statements
using TestTokenApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestTokenApp.Data
{
    public partial class TestTokenAppContext : DbContext
    {
        //public AcumenAdminContext() {
        //}

        public TestTokenAppContext(DbContextOptions<TestTokenAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LocalAdminPassword> LocalAdminPasswords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-TestTokenApp-C089B35D-37D5-4F35-A2C8-5AB786500773;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
