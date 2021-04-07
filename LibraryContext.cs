using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;

namespace EF_Core_Introduction
{
    class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        public LibraryContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(new Author { Id = 1, Name = "Vasea", Age = 40},
                                                  new Author { Id = 2, Name = "Petea", Age = 45},
                                                  new { Id = 3, Name = "Galea", Age = 35 },
                                                  new { Id = 4, Name = "Seroja", Age = 50 },
                                                  new { Id = 5, Name = "Ghena", Age = 25 });

            modelBuilder.Entity<Order>().HasData(new {Id = 1, Date = 2021 / 04 / 07},
                                                 new {Id = 2, Date = 2021 / 04 / 08});

            //modelBuilder.Entity<Order>().OwnsOne<Person>(p => p.Customer)
            //    .HasData( new Customer { Id = 6, Name = "Igor", Age = 39, Address = "Chisinau" });
        }
    }
}
