using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadioTaxisAPI.Data.Entities;

namespace RadioTaxisAPI.Data.Repository
{
    public class LibraryDbContext : IdentityDbContext
    {
        public DbSet<BusinessEntity> Business { get; set; }
        public DbSet<DriverEntity> Drivers { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusinessEntity>().ToTable("Business");
            modelBuilder.Entity<BusinessEntity>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BusinessEntity>().HasMany(c => c.Drivers).WithOne(v => v.Business);

            modelBuilder.Entity<DriverEntity>().ToTable("Drivers");
            modelBuilder.Entity<DriverEntity>().Property(v => v.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DriverEntity>().HasOne(v => v.Business).WithMany(c => c.Drivers);
        }
    }

   
}       


        //dotnet tool install --global dotnet-ef
        //dotnet ef migrations add InitialCreate
        //dotnet ef database update
