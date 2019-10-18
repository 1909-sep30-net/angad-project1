using System;
using System.Collections.Generic;

namespace StoreApplication.Data.Entities
{
    public partial class OrderedProducts
    {
        public int Opid { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? LocationId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Locations Location { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
