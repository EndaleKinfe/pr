using Microsoft.AspNetCore.Mvc;
using pr.Data;
using pr.Models;
using System.Collections.Generic;

namespace pr.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RestaurantAccessor _restaurantAccessor;

        public RestaurantController()
        {
            _restaurantAccessor = new RestaurantAccessor();
        }

        public IActionResult Index()
        {
            var restaurants = _restaurantAccessor.GetAllRestaurants();
            return View(restaurants);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _restaurantAccessor.AddRestaurant(restaurant);
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        public IActionResult Edit(int id)
        {
            var restaurant = _restaurantAccessor.GetRestaurantById(id);
            return restaurant == null ? NotFound() : View(restaurant);
        }

        [HttpPost]
        public IActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _restaurantAccessor.UpdateRestaurant(restaurant);
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        public IActionResult Delete(int id)
        {
            var restaurant = _restaurantAccessor.GetRestaurantById(id);
            return restaurant == null ? NotFound() : View(restaurant);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _restaurantAccessor.DeleteRestaurant(id);
            return RedirectToAction("Index");
        }
    }
}
