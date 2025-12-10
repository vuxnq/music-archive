using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MusicArchive.Domain.Services;
using MusicArchive.Web.Models;
using MusicArchive.Domain.Models;

namespace MusicArchive.Web.Controllers;


public class UserController(
    IUserService userService
) : Controller {

    public IActionResult Index() {
        var users = userService.GetUsers();
        return View(users);
    }

    public IActionResult Detail(int id) {
        var user = userService.GetUser(id);
        return View(user);
    }

    public IActionResult Login() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password) {
        try {
            var user = userService.GetUserByUsername(username);
            if (user.Password == password) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.IsModerator ? "Moderator" : "User")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        } catch (KeyNotFoundException) {
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
    }

    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Join() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Join(UserAddDto model) {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Password != model.RepeatPassword) {
            ModelState.AddModelError("RepeatPassword", "Passwords do not match");
            return View(model);
        }

        try {
            var existing = userService.GetUserByUsername(model.Username);
            if (existing != null)
            {
                ModelState.AddModelError("Username", "Username is already taken");
                return View(model);
            }
        } catch (KeyNotFoundException) {
            // username unique - continue
        }

        var user = new User {
            Username = model.Username,
            Password = model.Password,
            IsModerator = false
        };
        userService.AddUser(user);
        TempData["SuccessMessage"] = "Account created successfully.";
        return RedirectToAction("Login");
    }
}
