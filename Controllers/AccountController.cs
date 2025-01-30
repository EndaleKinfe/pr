using Microsoft.AspNetCore.Mvc;
using pr.Models;
using pr.Data;

public class AccountController : Controller
{
    private readonly UserAccessor _userAccessor;

    public AccountController()
    {
        _userAccessor = new UserAccessor();
    }

    // SIGN UP - GET
    public IActionResult Register()
    {
        return View();
    }

    // SIGN UP - POST
    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            if (_userAccessor.RegisterUser(user))
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Registration failed. Try again.");
        }
        return View(user);
    }

    // LOGIN - GET
    public IActionResult Login()
    {
        return View();
    }

    // LOGIN - POST
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _userAccessor.LoginUser(email, password);
        if (user != null)
        {
            // HttpContext.Session.SetString("UserEmail", user.Email);
            // HttpContext.Session.SetString("UserRole", user.Role);
            return RedirectToAction("Index", "Home"); // Redirect to home page
        }
        ModelState.AddModelError("", "Invalid email or password.");
        return View();
    }

    // LOGOUT
    public IActionResult Logout()
    {
        // HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

