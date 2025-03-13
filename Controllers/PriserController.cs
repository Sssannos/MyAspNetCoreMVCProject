using Microsoft.AspNetCore.Mvc;
using Examinationsuppgift_ASP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examinationsuppgift_ASP.Controllers
{
    public class PriserController : Controller
    {
        public IActionResult Index()
        {
            // (Read). Stänger ned db när den db den skickat data till webbsidan.
            using (PrisContext db = new PrisContext())
            {
                /* Hämtar informationen inom tabellen "Priserna" i db och gör om till lista. 
                   Lambdauttryck som tar med priserBeskrivning från Ompriserna, d.v.s. Beskrivning skall visas i listan med Priser. */
                List<Priser> prisLista = db.OmPriserna.Include(r => r.priserBeskrivning).ToList();
                return View(prisLista);
            }
        }

        /* (Read). Läser av specifikt id på tabellen, "läs mer" */
        public IActionResult InfoPriser(int id)
        {
            using (PrisContext db = new PrisContext())
            {
                Priser priser = db.OmPriserna.Find(id);
                List<Priser> prisLista = db.OmPriserna.Include(r => r.priserBeskrivning).ToList();
                return View(priser);
            }
        }

        /* Returnerar (går till) sidan för CreatePriser */
        [HttpGet]
        public IActionResult CreatePriser()
        {
            using (PrisContext db = new PrisContext())
            {
                ViewBag.PriserBeskrivningId = new SelectList(db.OmPriserBeskrivning.ToList(), "Id", "Beskrivning");    // Visar upp Id och beskrivning från klassen PriserBeskrivning i CreatePriser.
            }
            return View();
        }

        /* (Create). Hanterar informationen som skickats i formuläret med hjälp av HttpPost */
        [HttpPost]
        public IActionResult CreatePriser(Priser nyPris)
        {
            using (PrisContext db = new PrisContext())
            {
                db.OmPriserna.Add(nyPris); // Lägger till tabellen i databasen.
                db.SaveChanges();
            }
            return RedirectToAction("index"); // Hoppar till Index sidan när data lagts till i tabellen. 
        }

        /* (Edit). Uppdatera ett specifikt id i tabellen */
        public IActionResult Edit(int id) 
        {
            using (PrisContext db = new PrisContext())
            {
                Priser priser = db.OmPriserna.Find(id);
                ViewBag.PriserBeskrivningId = new SelectList(db.OmPriserBeskrivning.ToList(), "Id", "Beskrivning");    // Visar upp de olika prisbeskrivningarna från klassen PriserBeskrivning.
                return View(priser);
            }
        }

        /* (Edit) När "spara" knappen klickas uppdateras tabellen med nya värden och sparas i databasen. */
        [HttpPost]
        public IActionResult Edit(Priser priser)
        {
            using (PrisContext db = new PrisContext())
            {
                db.Update(priser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /* (Delete). Returnerar ett visst id i tabellen för Priser-db och visar upp i en lista i "DeletePriser"-sidan  */
        public IActionResult DeletePriser(int id)
        {
            using (PrisContext db = new PrisContext())
            {
                Priser priser = db.OmPriserna.Find(id);
                List<Priser> prisLista = db.OmPriserna.Include(r => r.priserBeskrivning).ToList();
                return View(priser);
            }
        }

        /* (Delete) När "radera" knappen klickas uppdateras tabellen med nya värden och sparas i databasen. */
        [HttpPost]
        public IActionResult DeletePriser(Priser priser)
        {
            using (PrisContext db = new PrisContext())
            {
                db.OmPriserna.Remove(priser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
