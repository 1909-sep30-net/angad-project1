using System;
using System.Collections.Generic;

namespace StoreApplication.Data.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderedProducts = new HashSet<OrderedProducts>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? Quantity { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
    }
}
