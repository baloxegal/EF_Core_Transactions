﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Transactions
{
    class Payment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Card { get; set; }
    }
}
