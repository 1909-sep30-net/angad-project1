using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Data.Entities;
using System.Linq;

namespace StoreApplication.Data
{
    public class LocationData
    {
        public int LocationCount { get; set; }
        public List<int> LocationPresent { get; set; }

        public void DisplayLocationsDB(int product)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            var foundName = context.Inventory.FirstOrDefault(p => p.ProductId == product);
            var foundCity = context.Locations.FirstOrDefault(p => p.LocationId == foundName.LocationId);
            
            var foundLoc = context.Inventory.Where(p => p.ProductId == product).ToList();

            if (foundName is null)
            {
                Console.WriteLine("No Record Found");
                return;
            }

            foreach (Inventory loc in foundLoc)
            {
                if(loc.ProductId == product)
                {
                    var foundLocName = context.Locations.FirstOrDefault(p => p.LocationId == loc.LocationId);
                    Console.WriteLine($"Id: {loc.LocationId} | City: {foundLocName.City} | {GetInventoryDB((int)loc.LocationId, product)}");
                    
                }
                //LocationPresent.Add((int)loc.LocationId);
            }
            

        }

        public void DisplayAllLocationsDB()
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            foreach (Locations loc in context.Locations)
            {
                Console.WriteLine($"Id: {loc.LocationId} | City: {loc.City}");
            }
            LocationCount = context.Locations.Count();

        }

        public int GetInventoryDB(int location, int product)
        {
            string connectionString = SecretConfiguration.configurationString;

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            var foundName = context.Inventory.FirstOrDefault(p => p.LocationId == location && p.ProductId == product);

            if (foundName is null)
            {
                return 0;
            }

            return (int)foundName.Inventory1;
        }

    }
}
