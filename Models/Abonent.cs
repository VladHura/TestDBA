using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestDBA.Models
{
    internal class Abonent
    {
        public string FirstName { get; set; }
        public string SecondName {  get; set; }
        public string LastName { get; set; }
        public Address Address {  get; set; }
    }
}
