using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace StoreApplication.Library
{
    public class Product
    {

        public string ProductName { get; set; }
        public int Id { get; set; }
        public string ProductType { get; set; }
        public int ProductCount { get; set; }

        public List<Location> location = new List<Location>();
        
        public void SerializeJsonToFile(string jsonFilePath, List<Product> data)
        {
            //Need to Append Instead of Creating a New File Everytime
            string json = JsonConvert.SerializeObject(data);

            // exceptions should be handled here
            File.WriteAllText(jsonFilePath, json);

        }
        public List<Product> DeserializeJsonFromFile(string jsonFilePath)
        {
            // exceptions should be handled here
            string json = File.ReadAllText(jsonFilePath);

            var data = JsonConvert.DeserializeObject<List<Product>>(json);

            return data;
        }

    }
}