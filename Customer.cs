using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Transactions
{
    class Customer : Person
    {   
        public string Address { get; set; }
        public string Card { get; set; }
        //public virtual List<Order> Purchases { get; set; }

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
