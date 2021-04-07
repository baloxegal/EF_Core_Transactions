using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Introduction
{
    class Customer : Person
    {        
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Card { get; set; }
        public virtual List<Order> Purchases { get; set; }

        public Customer()
        {
            
        }

        public Customer(string name, int age, string address, string card) : base(name, age)
        {
            Address = address;
            Card = card;
        }
    }
}
