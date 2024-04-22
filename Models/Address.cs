using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestDBA.Models
{
    internal class Address
    {
        public Streets Street { get; set; }
        public int Building { get; set; }
        public int Apartment { get; set; }
    }
}
