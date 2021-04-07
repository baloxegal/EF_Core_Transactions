using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EF_Core_Introduction
{
    class Author : Person
    {
        public virtual List<Book> Books { get; set; }
        //public Book Book { get; set; }

        public Author()
        {
                
        }

        public Author(string name, int age) : base(name, age)
        {

        }

        public override string ToString()
        {
            return $"Author: Id - {Id}, Name - {Name}, Age - {Age}";
        }
    }
}
