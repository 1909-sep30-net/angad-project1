using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Data.Entities;
using System.Linq;
using StoreApplication.Business;

namespace StoreApplication.Data
{
    public class LocationData
    {
        public int LocationCount { get; set; }
        public List<int> LocationPresent { get; set; }

        public List<LocationsLogic> DisplayLocationsDB(int prod)
        {
            //string connectionString = SecretConfiguration.configurationString;
            string connectionString = "";

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            var foundName = context.Inventory.FirstOrDefault(p => p.ProductId == prod);
            var foundCity = context.Locations.FirstOrDefault(p => p.LocationId == foundName.LocationId);
            
            var foundLoc = context.Inventory.Where(p => p.ProductId == prod).ToList();

            if (foundName is null)
            {
                //Console.WriteLine("No Record Found");
                return null;
            }

            List<LocationsLogic> locations = new List<LocationsLogic>();

            foreach (Inventory loc in foundLoc)
            {
                if(loc.ProductId == prod)
                {
                    LocationsLogic tempLoc = new LocationsLogic();
                    var foundLocName = context.Locations.FirstOrDefault(p => p.LocationId == loc.LocationId);
                    tempLoc.LocationId = (int)loc.LocationId;
                    tempLoc.City = foundLocName.City;
                    tempLoc.Inventory = GetInventoryDB((int)loc.LocationId, prod);
                    //Console.WriteLine($"Id: {loc.LocationId} | City: {foundLocName.City} | {GetInventoryDB((int)loc.LocationId, product)}");
                    locations.Add(tempLoc);
                }
            }
            return locations;

        }

        public List<LocationsLogic> DisplayAllLocationsDB()
        {
            //string connectionString = SecretConfiguration.configurationString;
            string connectionString = "";

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            List<LocationsLogic> locations = new List<LocationsLogic>();

            foreach (Locations loc in context.Locations)
            {
                LocationsLogic tempLoc = new LocationsLogic();
                tempLoc.LocationId = loc.LocationId;
                tempLoc.City = loc.City;
                locations.Add(tempLoc);
            }
            LocationCount = context.Locations.Count();
            return locations;

        }

        public int GetInventoryDB(int location, int product)
        {
            //string connectionString = SecretConfiguration.configurationString;
            string connectionString = "";

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

        public void InventoryUpdate(int location, int product, int amount)
        {
            //string connectionString = SecretConfiguration.configurationString;
            string connectionString = "";

            DbContextOptions<GameStoreContext> options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new GameStoreContext(options);

            var foundName = context.Inventory.FirstOrDefault(p => p.LocationId == location && p.ProductId == product);

            if (foundName is null)
            {
                return;
            }

            foundName.Inventory1 -= amount;

            context.Inventory.Update(foundName);
            context.SaveChanges();

        }

    }
}
