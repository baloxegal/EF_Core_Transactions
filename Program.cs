using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core_Transactions
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var libraryContext = new LibraryContext())
            {
                //CUSTOM INITIALIZATION LOGIC FOR SEEDING DATA 
                libraryContext.Authors.Add(new Author { Name = "Aleftina", Age = 38 });
                libraryContext.SaveChanges();                
            }
            //TRANSACTION
            using (var context = new LibraryContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Database.ExecuteSqlRaw(@"UPDATE Authors SET Name = 'Addelina' WHERE Name LIKE 'V%'");
                        
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            //TRANSACTION ACROSS MULTIPLE CONTEXT
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            var options = new DbContextOptionsBuilder<LibraryContext>()
                //.UseLazyLoadingProxies()
                .UseSqlServer(connectionString)
                .Options;
            using(var libraryContext = new LibraryContext(options))
            {
                using(var transaction = libraryContext.Database.BeginTransaction())
                {
                    try
                    {
                        libraryContext.Authors.Add(new Author { Name = "Gabriela", Age = 33 });
                        libraryContext.SaveChanges();
                        using(var libraryContext_1 = new LibraryContext(options))
                        {
                            libraryContext_1.Database.UseTransaction((System.Data.Common.DbTransaction)transaction);
                            var authorList = libraryContext_1.Authors.ToList();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        //Why AUTO ROLLBACK?
                        transaction.Rollback();
                    }
                }

                //DEPRECATED!!!???
                //ENLIST TRANSACTION MANAGES DISTRIBUTED TRANSACTIONS
                using (var transactions = new CommittableTransaction(
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    string connectionString1 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    var connection = new SqlConnection(connectionString1);
                    try
                    {
                        var options1 = new DbContextOptionsBuilder<LibraryContext>()
                        .UseSqlServer(connectionString)
                        .Options;
                        using(var context1 = new LibraryContext(options))
                        {
                            context1.Database.OpenConnection();
                            context1.Database.EnlistTransaction(transactions);

                            var command = connection.CreateCommand();
                            command.CommandText = (@"UPDATE Authors SET Name = 'Siluana' WHERE Name LIKE 'A%'");
                            command.ExecuteNonQuery();

                            context1.Authors.Add(new Author { Name = "Vasea", Age = 41 });
                            context1.SaveChanges();
                            context1.Database.CloseConnection();
                        }
                        transactions.Commit();
                    }
                    catch (Exception e)
                    {
                        //Why AUTO ROLLBACK?
                        transactions.Rollback();
                        Console.WriteLine(e.Message);
                    }
                }

                //EAGER LOADING - ONE LEVEL
                
                var orderPaym = libraryContext.Orders.Include(p => p.Payment)
                .Where(t => t.Id == 1)
                .FirstOrDefault();                
                Console.WriteLine($"Id={orderPaym.Id}, Date={orderPaym.Date}, Payment={orderPaym.Payment.Type}, PaymentId={orderPaym.Payment.Id}");
                Console.WriteLine("===========================================");
                
                //EAGER LOADING - MULTIPLE LEVELS VARIANT 1

                //var orderPaym4 = libraryContext.Orders.Include(p => p.Payment)
                //                                        .ThenInclude(c => c.Cards)
                //                                            .ThenInclude(g => g.Costs)
                //.Where(t => t.Id == 1)
                //.FirstOrDefault();
                //Console.WriteLine($"Id={orderPaym.Id}, Date={orderPaym.Date}, Payment={orderPaym.Payment.Type}, PaymentId={orderPaym.Payment.Id}");
                //Console.WriteLine("===========================================");

                //EAGER LOADING - MULTIPLE LEVELS VARIANT 2

                //var orderPaym4 = libraryContext.Orders.Include(p => p.Payment)
                //                                        .ThenInclude(c => c.Cards)
                //                                      .Include(p => p.Payment)
                //                                        .ThenInclude(g => g.Costs)
                //.Where(t => t.Id == 1)
                //.FirstOrDefault();
                //Console.WriteLine($"Id={orderPaym.Id}, Date={orderPaym.Date}, Payment={orderPaym.Payment.Type}, PaymentId={orderPaym.Payment.Id}");
                //Console.WriteLine("===========================================");

                //EXPLICIT LOADING

                var orderPaym2 = libraryContext.Orders.FirstOrDefault();
                libraryContext.Entry(orderPaym2).Reference("Payment").Load();
                Console.WriteLine($"Id={orderPaym2.Id}, Date={orderPaym2.Date}, Payment={orderPaym2.Payment.Type}, PaymentId={orderPaym2.Payment.Id}");
                Console.WriteLine("===========================================");

                //EXPLICIT LOADING FOR COLECTIONS

                //var orderPaym3 = libraryContext.Orders.FirstOrDefault();
                //libraryContext.Entry(orderPaym2).Collection("Payment").Load();
                //Console.WriteLine($"Id={orderPaym2.Id}, Date={orderPaym2.Date}, Payment={orderPaym2.Payment.Type}, PaymentId={orderPaym2.Payment.Id}");
                //Console.WriteLine("===========================================");

                //LAZY LOADING

                var orderPaym3 = libraryContext.Orders.Where(o => o.Id == 2).FirstOrDefault();
                Console.WriteLine($"Id={orderPaym3.Id}, Date={orderPaym3.Date}, Payment={orderPaym3.Payment.Type}, PaymentId={orderPaym3.Payment.Id}");
                Console.WriteLine("===========================================");

                ////JOIN

                //var authorsFromOrders = libraryContext.Orders.Join(libraryContext.Books,
                //    orderBook => orderBook.Id,
                //    book => book.Id,
                //    (b, a) => new {bookFromOrder = b.Id, bookAuthor = a.Id};

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Companies.Add(new Company {Name = "Amdaris"});
                    db.Companies.Add(new Company { Name = "Google" });
                    db.Companies.Add(new Company { Name = "Microsoft" });
                    db.SaveChanges();

                    db.Users.Add(new User { Name = "Vasea", Age = 10, CompanyId = 1 });
                    db.Users.Add(new User { Name = "Maxim", Age = 20, CompanyId = 1 });
                    db.Users.Add(new User { Name = "Valentin", Age = 30, CompanyId = 1 });
                    db.Users.Add(new User { Name = "Igor", Age = 30, CompanyId = 2 });
                    db.Users.Add(new User { Name = "Serioja", Age = 40, CompanyId = 2 });
                    db.Users.Add(new User { Name = "Petea", Age = 20, CompanyId = 2 });
                    db.Users.Add(new User { Name = "Ghena", Age = 50, CompanyId = 3 });
                    db.Users.Add(new User { Name = "Stiopa", Age = 60, CompanyId = 3 });
                    db.SaveChanges();

                    var users = db.Users.Join(db.Companies,
                        u => u.CompanyId,
                        c => c.Id,
                        (u, c) => new
                        {
                            Name = u.Name,
                            Company = c.Name,
                            Age = u.Age
                        });
                    foreach (var u in users)
                        Console.WriteLine($"{u.Name} ({u.Company}) - {u.Age}");

                    //var users1 = db.Users.Join(db.Companies,
                    //    u => u.CompanyId,
                    //    c => c.Id,
                    //    (u, c) => new
                    //    {
                    //        Name = u.Name,
                    //        Company = c.Name,
                    //    });
                    //foreach (var u in users)
                    //    Console.WriteLine($"{u.Name} ({u.Company}) - {u.Age}");
                }
            }
        }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder/*.UseLazyLoadingProxies()*/.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=NewUserDataBase;Trusted_Connection=True;");
        }
    }
}