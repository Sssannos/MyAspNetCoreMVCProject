using Examinationsuppgift_ASP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Examinationsuppgift_ASP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index(string returnUrl = "")
        {
            @ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // Vid submit, logga in. Async & await (asynkront inloggning) motverkar applikationen "fryser" fast medan långsamma processer mellan bl. annat databasförfrågningar, nätverk etc.
        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel, string returnUrl = "")
        {
            // Kollar om userModel är true -> Om användarnamn & Lösen är korrekt.
            bool validUser = CheckUser(userModel);

            if (validUser == true) 
            {
                // If true (användarnamn & Lösen är korrekt) loggas användaren in.
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, userModel.UserName));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (returnUrl != "")
                    return Redirect(returnUrl);
                else 
                    return RedirectToAction("Index", "MinSida"); /* Redirect till "Min sida" om man går via /user log in */
            }
            else
            {
                ViewBag.ErrorMessage = "Fel inlogg, försök igen";
                @ViewData["ReturnUrl"] = returnUrl;     
                return View();
            }
        }

        // Kollar användaren i databasen med hjälp av "Hard coded" kod. Rekommenderas ej. 
        private bool CheckUser(UserModel userModel) 
        {
            if (userModel.UserName.ToUpper() == "ADMIN" && userModel.Password == "pwd")
            { 
                return true;
            }
            else 
                return false;
        }

        // Logga ut. Async & await (asynkront utloggning) motverkar applikationen "fryser" fast medan långsamma processer mellan bl. annat databasförfrågningar, nätverk etc.
        public async Task<IActionResult> SignOutUser()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
