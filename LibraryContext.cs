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
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;

namespace EF_Core_Transactions
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
            //Database.EnsureCreated();
        }
        
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LibrarySeeding;Trusted_Connection=True;");
            
            //WARNING IF INCLUDE IGNORED

            //ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning));
            
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //optionsBuilder.UseSqlServer(connectionString);
        }

        //MODEL SEED DATA
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(new Author { Id = 1, Name = "Vasea", Age = 40},
                                                  new Author { Id = 2, Name = "Petea", Age = 45},
                                                  new { Id = 3, Name = "Galea", Age = 35 },
                                                  new { Id = 4, Name = "Seroja", Age = 50 },
                                                  new { Id = 5, Name = "Ghena", Age = 25 });

            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 1, Name = "Gheorghe", Age = 50, Address = "Chisinau" },
                                                    new Customer { Id = 2, Name = "Michael", Age = 55, Address = "Atlanta" });

            modelBuilder.Entity<Order>().HasData(new { Id = 1, CustomerId = 1, Date = new DateTime (2021, 04, 07) },
                                                 new { Id = 2, CustomerId = 2, Date = new DateTime (2021, 04, 08) });
                        
            modelBuilder.Entity<Order>().OwnsOne<Payment>(p => p.Payment)
                .HasData(new { OrderId = 1, Id = 1, Type = "Cash", PaymentId = 1 },
                         new { OrderId = 2, Id = 2, Type = "Card", PaymentId = 2 });            
        }
    }
}
