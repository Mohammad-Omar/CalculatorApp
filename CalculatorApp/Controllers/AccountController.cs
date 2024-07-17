using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using CalculatorApp.Models;

public class AccountController : Controller {
    [HttpGet]
    public IActionResult Login() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model) {
        if (ModelState.IsValid) {
            if (model.Username == "admin" && model.Password == "password") {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("History", "Calculator"); 
            }
            else {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
        }

        return View(model);
    }
}