using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using pr.Models;
using pr.Data;

namespace pr.Controllers
{
public class FoodController : Controller
{
    private readonly FoodAccessor _foodAccessor;

    public FoodController()
    {
        _foodAccessor = new FoodAccessor();
    }

    // LIST VIEW
    public IActionResult Index()
    {
        List<Food> foodList = _foodAccessor.GetAllFoods();
        return View(foodList);
    }

    

    // CREATE - GET
    public IActionResult Create()
    {
        return View();
    }

    // CREATE - POST
    [HttpPost]
    public IActionResult Create(Food food)
    {
        if (ModelState.IsValid)
        {
            _foodAccessor.AddFood(food);
            return RedirectToAction("Index");
        }
        return View(food);
    }

    // EDIT - GET
    public IActionResult Edit(int id)
    {
        Food food = _foodAccessor.GetFoodById(id);
        if (food == null)
        {
            return NotFound();
        }
        return View(food);
    }

    // EDIT - POST
    [HttpPost]
    public IActionResult Edit(Food food)
    {
        if (ModelState.IsValid)
        {
            _foodAccessor.UpdateFood(food);
            return RedirectToAction("Index");
        }
        return View(food);
    }

    // DELETE - GET
    public IActionResult Delete(int id)
    {
        Food food = _foodAccessor.GetFoodById(id);
        if (food == null)
        {
            return NotFound();
        }
        return View(food);
    }

    // DELETE - POST
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _foodAccessor.DeleteFood(id);
        return RedirectToAction("Index");
    }
}

}
