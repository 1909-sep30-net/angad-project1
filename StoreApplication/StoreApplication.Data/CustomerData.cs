using System;
using System.IO;
using System.Collections.Generic;
using StoreApplication.Library;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Data.Entities;
using System.Linq;

namespace StoreApplication.Data
{
    public class CustomerData
    {
        Library.Customer customer = new Library.Customer();
        public string[] nameHolder = new string[3];
        public int searchCount = 0;

        public int CustomerCount { get; set; }

        public void AddCustomerDB(Customers customers)
        {

            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            context.Customers.Add(customers);

            context.SaveChanges();
        }

        public List<Customers> ListCustomersDB()
        {

            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            CustomerCount = context.Customers.Count();

            return context.Customers.ToList();
        }

        public List<Customers> SearchCustomersDB(string name)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            var foundName = context.Customers.Where(p => p.FirstName == name || p.LastName == name || p.FirstName + " " + p.LastName == name).ToList();

            return foundName;

        }

    }
}
