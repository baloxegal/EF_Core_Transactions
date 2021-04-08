using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Transactions
{
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public virtual List<Author> Authors { get; set; }
        //public int AuthorId { get; set; }
        //public virtual Author Author { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public virtual List<Order> Orders { get; set; }

        public Book()
        {
                
        }

        public Book(string title, string description, string cover, List<Author> authors, int price, bool available)
        {
            Title = title;
            Description = description;
            Cover = cover;
            Authors = authors;
            Price = price;
            Available = available;
        }
        public Book(string title, string description, string cover, Author author, int price, bool available)
        {
            Title = title;
            Description = description;
            Cover = cover;
            Authors = new List<Author>();
            Authors.Add(author);
            Price = price;
            Available = available;
        }
    }
}
