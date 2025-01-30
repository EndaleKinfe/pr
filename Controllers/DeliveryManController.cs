using Microsoft.AspNetCore.Mvc;
using pr.Data;
using pr.Models;
using System.Collections.Generic;

namespace pr.Controllers
{
    public class DeliveryManController : Controller
    {
        private readonly DeliveryManAccessor _deliveryManAccessor;

        public DeliveryManController()
        {
            _deliveryManAccessor = new DeliveryManAccessor();
        }

        public IActionResult Index()
        {
            var deliveryMen = _deliveryManAccessor.GetAllDeliveryMen();
            return View(deliveryMen);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DeliveryMan deliveryMan)
        {
            if (ModelState.IsValid)
            {
                _deliveryManAccessor.AddDeliveryMan(deliveryMan);
                return RedirectToAction("Index");
            }
            return View(deliveryMan);
        }

        public IActionResult Edit(int id)
        {
            var deliveryMan = _deliveryManAccessor.GetDeliveryManById(id);
            return deliveryMan == null ? NotFound() : View(deliveryMan);
        }

        [HttpPost]
        public IActionResult Edit(DeliveryMan deliveryMan)
        {
            if (ModelState.IsValid)
            {
                _deliveryManAccessor.UpdateDeliveryMan(deliveryMan);
                return RedirectToAction("Index");
            }
            return View(deliveryMan);
        }

        public IActionResult Delete(int id)
        {
            var deliveryMan = _deliveryManAccessor.GetDeliveryManById(id);
            return deliveryMan == null ? NotFound() : View(deliveryMan);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _deliveryManAccessor.DeleteDeliveryMan(id);
            return RedirectToAction("Index");
        }
    }
}
