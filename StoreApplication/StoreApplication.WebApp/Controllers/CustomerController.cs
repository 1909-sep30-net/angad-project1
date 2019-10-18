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
    public class CustomerController : Controller
    {

        CustomerData customerData = new CustomerData();
        static string searchedName;
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult AddCustomer()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var customers = new Customers
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"]
                };


                customerData.AddCustomerDB(customers);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListCustomers()
        {
            List<Customers> customers = customerData.ListCustomersDB();

            return View(customers);
        }

        public ActionResult SearchCustomerDisplay()
        {
            List<Customers> customers = customerData.SearchCustomersDB(searchedName);

            return View(customers);
        }

        // GET: Customer/Create
        public ActionResult SearchIndex()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchCustomers(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var customers = new Customers
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"]
                };

                searchedName = customers.FirstName + customers.LastName;

                return RedirectToAction(nameof(SearchCustomerDisplay));
            }
            catch
            {
                return View();
            }
        }

        #region To Be Done Later
        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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