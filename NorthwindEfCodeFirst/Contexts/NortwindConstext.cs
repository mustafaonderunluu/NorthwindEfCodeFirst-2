using NorthwindEfCodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindEfCodeFirst.Contexts
{
    public class NortwindConstext:DbContext     //Bütün ilişkileri tanımlayacağımız yer.
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order>Orders { get; set; }
    }
}
