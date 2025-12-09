using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MusicArchive.Domain.Services;

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}