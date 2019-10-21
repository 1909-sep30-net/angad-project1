using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using StoreApplication.Library;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Data.Entities;
using System.Linq;
using StoreApplication.Business;

namespace StoreApplication.Data
{
    public class OrderData
    {

        Order order = new Order();

        Product product = new Product();
        Customer customer = new Customer();

        ProductData dataProduct = new ProductData();
        CustomerData dataCustomer = new CustomerData();

        public List<Customer> tempCustData = new List<Customer>();
        public List<Product> tempProdData = new List<Product>();

        public void CreateOrderDB(int selectProd, int selectCust, int citySelect, int quant)
        {

            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            Orders orders = new Orders();
            OrderedProducts orderedProds = new OrderedProducts();

            /*Random random = new Random();
            orders.OrderId = random.Next(10000, 99999);*/

            orders.CustomerId = selectCust;
            orders.Quantity = quant;
            //orders.OrderDate = DateTime.ParseExact(dateString, "MM/dd/yyyy", null);
            orders.OrderDate = DateTime.Now;

            context.Orders.Add(orders);
            context.SaveChanges();

            orderedProds.CustomerId = selectCust;
            orderedProds.OrderId = (int)orders.OrderId;
            orderedProds.ProductId = selectProd;
            orderedProds.LocationId = citySelect;

            context.OrderedProducts.Add(orderedProds);
            context.SaveChanges();

        }

        public List<OrdersLogic> DisplayOrdersDB()
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);
            using var context2 = new GameStoreContext(options);
            using var context3 = new GameStoreContext(options);
            using var context4 = new GameStoreContext(options);
            using var context5 = new GameStoreContext(options);

            List<OrdersLogic> orders = new List<OrdersLogic>();

            foreach (Orders order in context.Orders)
            {
                OrdersLogic tempOrder = new OrdersLogic();
                
                tempOrder.OrderId = order.OrderId;
                tempOrder.OrderDate = order.OrderDate;
                tempOrder.Quantity = (int) order.Quantity;
                
                var foundName = context2.Customers.FirstOrDefault(p => p.CustomerId == order.CustomerId);

                tempOrder.CustomerName = foundName.FirstName + " " + foundName.LastName;

                var foundProduct = context3.OrderedProducts.FirstOrDefault(p => p.CustomerId == foundName.CustomerId && p.OrderId == order.OrderId);
                var foundProductName = context4.Products.FirstOrDefault(p => p.ProductId == foundProduct.ProductId);
                var foundLocName = context5.Locations.FirstOrDefault(p => p.LocationId == foundProduct.LocationId);

                tempOrder.LocationName = foundLocName.City;
                tempOrder.ProductName = foundProductName.ProductName;

                orders.Add(tempOrder);
            }
            

            return orders;

        }

        public void DisplayOrdersCustomerDB(int customerId)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);
            using var context2 = new GameStoreContext(options);
            using var context3 = new GameStoreContext(options);
            using var context4 = new GameStoreContext(options);

            foreach (Orders order in context.Orders)
            {
                var foundName = context2.Customers.FirstOrDefault(p => p.CustomerId == order.CustomerId);
                var foundProduct = context3.OrderedProducts.FirstOrDefault(p => p.CustomerId == foundName.CustomerId && p.OrderId == order.OrderId);
                var foundProductName = context4.Products.FirstOrDefault(p => p.ProductId == foundProduct.ProductId);

                if (foundProduct.CustomerId == customerId)
                {
                    Console.Write($"OrderID: {order.OrderId} | Order Date: {order.OrderDate} | Quantity: {order.Quantity} | ");
                    Console.Write($"Customer Name: {foundName.FirstName + " " + foundName.LastName} | ");
                    Console.Write($"Game Name: {foundProductName.ProductName}\n");
                }
            }
        }

        public void DisplayOrdersStoreDB(int locationId)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);
            using var context2 = new GameStoreContext(options);
            using var context3 = new GameStoreContext(options);
            using var context4 = new GameStoreContext(options);
            
            foreach (Orders order in context.Orders)
            {
                var foundName = context2.Customers.FirstOrDefault(p => p.CustomerId == order.CustomerId);
                var foundProduct = context3.OrderedProducts.FirstOrDefault(p => p.CustomerId == foundName.CustomerId && p.OrderId == order.OrderId);
                var foundProductName = context4.Products.FirstOrDefault(p => p.ProductId == foundProduct.ProductId);
                
                if (foundProduct.LocationId == locationId)
                {
                    Console.Write($"OrderID: {order.OrderId} | Order Date: {order.OrderDate} | Quantity: {order.Quantity} | ");
                    Console.Write($"Customer Name: {foundName.FirstName + " " + foundName.LastName} | ");
                    Console.Write($"Game Name: {foundProductName.ProductName} | City: {foundProduct.LocationId}\n");
                }
            }
        }

    }
}
