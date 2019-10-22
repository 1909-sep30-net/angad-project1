using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebApp.Models
{
    public class OrdersModel
    {

        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string LocationName { get; set; }

    }
}
