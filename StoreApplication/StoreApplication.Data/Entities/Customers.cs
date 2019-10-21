using System;
using System.Collections.Generic;

namespace StoreApplication.Data.Entities
{
    public partial class Customers
    {
        public Customers()
        {
            OrderedProducts = new HashSet<OrderedProducts>();
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
