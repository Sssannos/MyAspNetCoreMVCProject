using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Når endast MinSida-sidan om inloggad, med hjälp av Authorize
namespace Examinationsuppgift_ASP.Controllers
{
    [Authorize]
    public class MinSidaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}