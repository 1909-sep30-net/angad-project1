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

            LocationData locDat = new LocationData();

            Orders orders = new Orders();
            OrderedProducts orderedProds = new OrderedProducts();

            orders.CustomerId = selectCust;
            orders.Quantity = quant;
            orders.OrderDate = DateTime.Now;

            context.Orders.Add(orders);
            context.SaveChanges();

            orderedProds.CustomerId = selectCust;
            orderedProds.OrderId = (int)orders.OrderId;
            orderedProds.ProductId = selectProd;
            orderedProds.LocationId = citySelect;

            context.OrderedProducts.Add(orderedProds);
            context.SaveChanges();

            locDat.InventoryUpdate((int) orderedProds.LocationId, (int) orderedProds.ProductId, (int) orders.Quantity);

            //Add Decrementing Functionality To The Inventory Of The Location The Order is Created From.1

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

        public List<OrdersLogic> DisplayOrdersCustomerDB(int customerId)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);
            using var context2 = new GameStoreContext(options);
            using var context3 = new GameStoreContext(options);
            using var context4 = new GameStoreContext(options);

            List<OrdersLogic> orders = new List<OrdersLogic>();

            foreach (Orders order in context.Orders)
            {
                OrdersLogic tempOrder = new OrdersLogic();

                var foundName = context2.Customers.FirstOrDefault(p => p.CustomerId == order.CustomerId);
                var foundProduct = context3.OrderedProducts.FirstOrDefault(p => p.CustomerId == foundName.CustomerId && p.OrderId == order.OrderId);
                var foundProductName = context4.Products.FirstOrDefault(p => p.ProductId == foundProduct.ProductId);
                var foundLocName = context4.Locations.FirstOrDefault(p => p.LocationId == foundProduct.LocationId);

                if (foundProduct.CustomerId == customerId)
                {

                    tempOrder.OrderId = order.OrderId;
                    tempOrder.OrderDate = order.OrderDate;
                    tempOrder.Quantity = (int)order.Quantity;
                    tempOrder.CustomerName = foundName.FirstName + " " + foundName.LastName;
                    tempOrder.LocationName = foundLocName.City;
                    tempOrder.ProductName = foundProductName.ProductName;

                    orders.Add(tempOrder);
                }
            }
            return orders;
        }

        public List<OrdersLogic> DisplayOrdersStoreDB(int locationId)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);
            using var context2 = new GameStoreContext(options);
            using var context3 = new GameStoreContext(options);
            using var context4 = new GameStoreContext(options);

            List<OrdersLogic> orders = new List<OrdersLogic>();

            foreach (Orders order in context.Orders)
            {
                OrdersLogic tempOrder = new OrdersLogic();

                var foundName = context2.Customers.FirstOrDefault(p => p.CustomerId == order.CustomerId);
                var foundProduct = context3.OrderedProducts.FirstOrDefault(p => p.CustomerId == foundName.CustomerId && p.OrderId == order.OrderId);
                var foundProductName = context4.Products.FirstOrDefault(p => p.ProductId == foundProduct.ProductId);
                var foundLocName = context4.Locations.FirstOrDefault(p => p.LocationId == foundProduct.LocationId);

                if (foundProduct.LocationId == locationId)
                {
                    tempOrder.OrderId = order.OrderId;
                    tempOrder.OrderDate = order.OrderDate;
                    tempOrder.Quantity = (int)order.Quantity;
                    tempOrder.CustomerName = foundName.FirstName + " " + foundName.LastName;
                    tempOrder.LocationName = foundLocName.City;
                    tempOrder.ProductName = foundProductName.ProductName;

                    orders.Add(tempOrder);
                }
            }
            return orders;
        }

    }
}
