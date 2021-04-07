using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Introduction
{
    class Order
    {
        public int Id { get; set; }
        public virtual Person Customer { get; set; }
        public DateTime Date { get; set; }
        public virtual List<Book> Purchases { get; set; }

        public Order()
        {
                
        }

        public Order(Person customer, DateTime date, List<Book> purchases)
        {
            Customer = customer;
            Date = date;
            Purchases = purchases;  
        }

        public Order(Person customer, DateTime date, Book book)
        {
            Customer = customer;
            Date = date;
            Purchases = new List<Book>();
            Purchases.Add(book);
        }
    }
}
