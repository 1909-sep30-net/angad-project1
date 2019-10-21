using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Data;
using StoreApplication.Data.Entities;

namespace StoreApplication.WebApp.Controllers
{
    public class ProductController : Controller
    {

        ProductData prodData = new ProductData();

        #region LocInvValueForASP
        public int StoreLocs { get; set; }
        public int Location1 { get; set; }
        public int Inventory1 { get; set; }
        public int Location2 { get; set; }
        public int Inventory2 { get; set; }
        public int Location3 { get; set; }
        public int Inventory3 { get; set; }
        public int Location4 { get; set; }
        public int Inventory4 { get; set; }
        public int Location5 { get; set; }
        public int Inventory5 { get; set; }
        public int Location6 { get; set; }
        public int Inventory6 { get; set; }
        #endregion

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                //TODO: Add insert logic here
                int storeCount;
                storeCount = int.Parse(collection["storeCount"]);

                #region Locations&Inventory
                List<string> locs = new List<string>();
                locs.Add(collection["Location1"]);
                locs.Add(collection["Location2"]);
                locs.Add(collection["Location3"]);
                locs.Add(collection["Location4"]);
                locs.Add(collection["Location5"]);
                locs.Add(collection["Location6"]);

                List<string> inv = new List<string>();
                inv.Add(collection["Inventory1"]);
                inv.Add(collection["Inventory2"]);
                inv.Add(collection["Inventory3"]);
                inv.Add(collection["Inventory4"]);
                inv.Add(collection["Inventory5"]);
                inv.Add(collection["Inventory6"]);
                #endregion

                Products product = new Products
                {
                    ProductName = collection["ProductName"],
                    ProductType = collection["ProductType"],
                };

                prodData.AddProductsDB(product, locs, inv, storeCount);

                return RedirectToAction(nameof(ListProducts));
            }
            catch
            {
                return View(AddProduct());
            }
        }

        public ActionResult ListProducts()
        {
            List<Products> products = prodData.DisplayProductsDB();

            return View(products);
        }

        #region To Be Added Later
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}