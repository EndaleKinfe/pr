using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pr.Data;
using pr.Models;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderAccessor _orderAccessor;
        private readonly CustomerAccessor _customerAccessor;
        private readonly RestaurantAccessor _restaurantAccessor;
        private readonly DeliveryManAccessor _deliveryManAccessor;

        public OrderController()
        {
            _orderAccessor = new OrderAccessor();
            _customerAccessor = new CustomerAccessor();
            _restaurantAccessor = new RestaurantAccessor();
            _deliveryManAccessor = new DeliveryManAccessor();
        }

        // LIST ALL ORDERS
        public IActionResult Index()
        {
            var orders = _orderAccessor.GetAllOrders();
            return View(orders);
        }

        // CREATE ORDER - GET
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_customerAccessor.GetAllCustomers(), "Id", "Name");
            ViewBag.Restaurants = new SelectList(_restaurantAccessor.GetAllRestaurants(), "Id", "Name");
            return View();
        }

        // CREATE ORDER - POST
        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderAccessor.AddOrder(order);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = new SelectList(_customerAccessor.GetAllCustomers(), "Id", "Name");
            ViewBag.Restaurants = new SelectList(_restaurantAccessor.GetAllRestaurants(), "Id", "Name");
            return View(order);
        }

        // EDIT ORDER - GET
        public IActionResult Edit(int id)
        {
            var order = _orderAccessor.GetOrderById(id);
            if (order == null) return NotFound();

            ViewBag.Customers = new SelectList(_customerAccessor.GetAllCustomers(), "Id", "Name", order.CustomerId);
            ViewBag.Restaurants = new SelectList(_restaurantAccessor.GetAllRestaurants(), "Id", "Name", order.RestaurantId);
            ViewBag.DeliveryMen = new SelectList(_deliveryManAccessor.GetAllDeliveryMen(), "Id", "Name", order.DeliveryManId);
            
            return View(order);
        }

        // EDIT ORDER - POST
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderAccessor.UpdateOrder(order);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = new SelectList(_customerAccessor.GetAllCustomers(), "Id", "Name", order.CustomerId);
            ViewBag.Restaurants = new SelectList(_restaurantAccessor.GetAllRestaurants(), "Id", "Name", order.RestaurantId);
            ViewBag.DeliveryMen = new SelectList(_deliveryManAccessor.GetAllDeliveryMen(), "Id", "Name", order.DeliveryManId);
            return View(order);
        }

        // DELETE ORDER - GET
        public IActionResult Delete(int id)
        {
            var order = _orderAccessor.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        // DELETE ORDER - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderAccessor.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
