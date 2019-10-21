using System;

namespace StoreApplication.Business
{
    public class OrdersLogic
    {

        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string LocationName { get; set; }

    }
}
