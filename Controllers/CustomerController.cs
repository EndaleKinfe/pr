using Microsoft.AspNetCore.Mvc;
using pr.Data;
using pr.Models;
using System.Collections.Generic;

namespace pr.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerAccessor _customerAccessor;

        public CustomerController()
        {
            _customerAccessor = new CustomerAccessor();
        }

        // LIST ALL CUSTOMERS
        public IActionResult Index()
        {
            var customers = _customerAccessor.GetAllCustomers();
            return View(customers);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerAccessor.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var customer = _customerAccessor.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerAccessor.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            var customer = _customerAccessor.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // DELETE - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _customerAccessor.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}
