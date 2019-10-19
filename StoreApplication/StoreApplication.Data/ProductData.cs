using System;
using System.Collections.Generic;
using System.Text;
using StoreApplication.Library;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Data.Entities;
using System.Linq;

namespace StoreApplication.Data
{
    public class ProductData
    {

        Product prodData = new Product();
        public int ProductCount { get; set; }

        public void AddProductsDB(string productName, string productType, int storeLocation, int inventoryForLoc, int storeCount, List<int> storeLocationList, List<int> storeInventoryList)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            Products prod = new Products();
            
            prod.ProductName = productName;
            prod.ProductType = productType;

            context.Products.Add(prod);
            context.SaveChanges();

            for (int i = 0; i < storeCount; i++)
            {
                Locations tempLoc = new Locations();
                Inventory tempInv = new Inventory();
                
                tempLoc.LocationId = storeLocationList[i];
                
                var foundName = context.Locations.FirstOrDefault(p => p.LocationId == storeLocationList[i]);
                
                tempLoc.City = foundName.City;

                if (foundName is null)
                {
                    //context.Locations.Add(tempLoc);
                    //context.SaveChanges();
                    //context.Products.Add(prod);
                    //context.SaveChanges();
                }
                else
                {
                    tempLoc.LocationId = foundName.LocationId;
                    tempInv.LocationId = tempLoc.LocationId;
                    //tempInv.ProductId = prod.ProductId;
                }
                tempInv.ProductId = prod.ProductId;

                tempInv.Inventory1 = storeInventoryList[i];
                
                context.Inventory.Add(tempInv);
            }

            context.SaveChanges();
        }

        public List<Products> DisplayProductsDB()
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            ProductCount = context.Products.Count();

            return context.Products.ToList();
        }

    }
}
