using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace StoreApplication.Library 
{
    public class Order 
    {

        public Customer customer = new Customer();

        public Product product = new Product();

        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; } = new DateTime();

        public int orderQuantity { get; set; }

        public void SerializeJsonToFile(string jsonFilePath, List<Order> data)
        {

            string json = JsonConvert.SerializeObject(data);

            // exceptions should be handled here
            File.WriteAllText(jsonFilePath, json);

        }
        public List<Order> DeserializeJsonFromFile(string jsonFilePath)
        {
            // exceptions should be handled here
            string json = File.ReadAllText(jsonFilePath);

            var data = JsonConvert.DeserializeObject<List<Order>>(json);

            return data;
        }

    }
}
