using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Transactions
{
    abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public int? Age { get; set; }

        public Person()
        {
                
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;  
        }
    }
}
