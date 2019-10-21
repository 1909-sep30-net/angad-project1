using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Data;
using StoreApplication.Data.Entities;
using StoreApplication.Business;

namespace StoreApplication.WebApp.Controllers
{
    public class OrderController : Controller
    {
        OrderData orderData = new OrderData();
        CustomerData custData = new CustomerData();
        ProductData prodData = new ProductData();
        LocationData locData = new LocationData();
        static int customer, product, location;

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult SelectCustomer()
        {

            List<Customers> customers = custData.ListCustomersDB();

            return View(customers);

        }

        public ActionResult SelectProducts()
        {
            List<Products> products = prodData.DisplayProductsDB();

            return View(products);
        }

        public ActionResult SelectLocation()
        {
            List<LocationsLogic> locs = locData.DisplayLocationsDB(product);

            return View(locs);
        }

        // GET: Order/Create
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int quantity = int.Parse(collection["Quantity"]);
                orderData.CreateOrderDB(product, customer, location, quantity);

                return RedirectToAction(nameof(ListOrders));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                customer = int.Parse(collection["selectCustomer"]);

                return RedirectToAction(nameof(SelectProducts));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                product = int.Parse(collection["selectProduct"]);

                return RedirectToAction(nameof(SelectLocation));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLocation(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                location = int.Parse(collection["selectLocation"]);

                return RedirectToAction(nameof(CreateOrder));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ListOrders()
        {
            List<OrdersLogic> orders = orderData.DisplayOrdersDB();

            return View(orders);
        }

        public ActionResult SelectCustomerForOrder()
        {

            List<Customers> customers = custData.ListCustomersDB();

            return View(customers);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectedCustomer(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                customer = int.Parse(collection["selectCustomer"]);

                return RedirectToAction(nameof(ListOrdersCustomer));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListOrdersCustomer()
        {
            List<OrdersLogic> orders = orderData.DisplayOrdersCustomerDB(customer);

            return View(orders);
        }

        public ActionResult SelectStoreForOrder()
        {
            List<LocationsLogic> locs = locData.DisplayAllLocationsDB();

            return View(locs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectedLocation(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                location = int.Parse(collection["selectLocation"]);

                return RedirectToAction(nameof(ListOrdersStore));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListOrdersStore()
        {
            List<OrdersLogic> orders = orderData.DisplayOrdersStoreDB(location);

            return View(orders);
        }

        #region To Be Added Later
        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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