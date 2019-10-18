using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace StoreApplication.Library 
{
    public class Customer 
    {

        List<Order> customerOrders = new List<Order>();

        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string defaultLocation = "Arlington";
        public int CustomerCount { get; set; }

        public void SerializeJsonToFile(string jsonFilePath, List<Customer> data)
        {
            //Need to Append Instead of Creating a New File Everytime
            string json = JsonConvert.SerializeObject(data);

            // exceptions should be handled here
            File.WriteAllText(jsonFilePath, json);

        }
        
        public List<Customer> DeserializeJsonFromFile(string jsonFilePath)
        {
            // exceptions should be handled here
            string json = File.ReadAllText(jsonFilePath);

            var data = JsonConvert.DeserializeObject<List<Customer>>(json);

            return data;
        }
        
    }
}