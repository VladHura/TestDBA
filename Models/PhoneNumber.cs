using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBA.Models
{
    internal class PhoneNumber
    {
        public Abonent Abonent { get; set; }
        public int HomeNumber { get; set; }
        public int WorkNumber { get; set; }
        public int MobileNumber { get; set; }
    }
}
