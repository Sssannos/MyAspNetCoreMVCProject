using Microsoft.AspNetCore.Mvc;

namespace Examinationsuppgift_ASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
