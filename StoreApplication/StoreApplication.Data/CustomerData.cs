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
        private bool addedCustomer;

        Library.Customer customer = new Library.Customer();
        public string[] nameHolder = new string[3];
        public int searchCount = 0;

        public int CustomerCount { get; set; }

        public void AddCustomer(string jsonFilePath, string fullName)
        {

            Customer newCustomer = new Customer();
            nameHolder = fullName.Split(' ');

            if (nameHolder.Length == 2)
            {
                newCustomer.FirstName = nameHolder[0];
                newCustomer.LastName = nameHolder[1];

                List<Customer> tempCustomer = new List<Customer>();
                
                //If the file already exists (i.e. Not the first time Adding a customer) It deserializes the already input data and adds that to the tempCustomer
                //The tempCustomer is then appended with the newCustomer
                if (File.Exists(jsonFilePath))
                {
                    tempCustomer.AddRange(customer.DeserializeJsonFromFile(jsonFilePath));
                    tempCustomer.Add(newCustomer);
                }
                else
                {
                    tempCustomer.Add(newCustomer);
                }
                addedCustomer = true;
                customer.SerializeJsonToFile(jsonFilePath, tempCustomer);
                
            }
            else
            {
                addedCustomer = false;
            }

        }

        public void AddCustomerDB(string fullName)
        {

            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            Customers newCust = new Customers();
            string[] name = fullName.Split(' ');

            newCust.FirstName = name[0];
            newCust.LastName = name[1];

            context.Customers.Add(newCust);

            context.SaveChanges();
        }

        public void DisplayCustomersDB()
        {

            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            foreach (Customers customer in context.Customers)
            {
                Console.WriteLine($"Id: {customer.CustomerId} | Name: {customer.FirstName} {customer.LastName}");
            }
            CustomerCount = context.Customers.Count();
        }

        public void SearchCustomersDB(string name)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            var foundName = context.Customers.FirstOrDefault(p => p.FirstName == name || p.LastName == name || p.FirstName + " " + p.LastName == name);

            if (foundName is null)
            {
                Console.WriteLine("No Record Found");
                return;
            }

            Console.WriteLine($"Id: {foundName.CustomerId} | Name: {foundName.FirstName} {foundName.LastName}");

        }

        public List<Customer> DisplayCustomers(string jsonFilePath)
        {
            List<Customer> tempData = new List<Customer>();
            if (File.Exists(jsonFilePath))
            {
                tempData = customer.DeserializeJsonFromFile(jsonFilePath);

                CustomerCount = tempData.Count;
            }
            return tempData;
        }

    }
}
