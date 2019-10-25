using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Data.Entities;
using System.Linq;

namespace StoreApplication.Data
{
    public class ProductData
    {

        public int ProductCount { get; set; }

        Products products = new Products();
        Inventory inventory = new Inventory();

        public void AddProductsDB(Products products, List<string> locs, List<string> inv, int storeCount)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            Products prod = new Products();
            
            prod.ProductName = products.ProductName;
            ////prod.ProductType = products.ProductType;
            prod.ProductUrl = products.ProductUrl;

            context.Products.Add(prod);
            context.SaveChanges();

            for (int i = 0; i < storeCount; i++)
            {
                Locations tempLoc = new Locations();
                Inventory tempInv = new Inventory();

                
                tempLoc.LocationId = int.Parse(locs[i]);
                
                var foundName = context.Locations.FirstOrDefault(p => p.LocationId == tempLoc.LocationId);
                
                tempLoc.City = foundName.City;

                if (foundName is null) { }
                else
                {
                    tempLoc.LocationId = foundName.LocationId;
                    tempInv.LocationId = tempLoc.LocationId;
                }
                tempInv.ProductId = prod.ProductId;

                tempInv.Inventory1 = int.Parse(inv[i]);
                
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
