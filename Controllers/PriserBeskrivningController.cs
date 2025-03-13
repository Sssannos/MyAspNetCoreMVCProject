using Examinationsuppgift_ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examinationsuppgift_ASP.Controllers
{
    // Controller för PriserBeskrivning. Tabell 2 (VG)
    public class PriserBeskrivningController : Controller
    {
        public IActionResult Index()
        {
            using (PrisContext db = new PrisContext())
            {
                List<PriserBeskrivning> PrisBeskrivningLista = db.OmPriserBeskrivning.ToList(); // Hämtar informationen inom tabellen "PriserBeskrivning" i db och visas som en lista i PriserBeskrivning-sidan (index).
                return View(PrisBeskrivningLista);
            }
        }

        /* Returnerar (går till) sidan för CreatePriser */
        [HttpGet]
        public IActionResult CreatePriser()
        {
            return View();
        }

        /* (Create). Hanterar informationen som skickats i formuläret med hjälp av HttpPost */
        [HttpPost]
        public IActionResult CreatePriser(PriserBeskrivning nyPriserBeskrivning)
        {
            using (PrisContext db = new PrisContext())
            {
                db.OmPriserBeskrivning.Add(nyPriserBeskrivning); // Lägger till tabellen i databasen.
                db.SaveChanges();
            }
            return RedirectToAction("index"); // Hoppar till Index sidan när data lagts till i tabellen. 
        }

        /* (Read). Läser av specifikt id på tabellen PriserBeskrivning, "läs mer" */
        public IActionResult InfoPriser(int id)
        {
            using (PrisContext db = new PrisContext())
            {
                PriserBeskrivning priserBeskrivning = db.OmPriserBeskrivning.Find(id);
                List<PriserBeskrivning> PriserBeskrivningLista = db.OmPriserBeskrivning.Include(r => r.Beskrivningar).ToList();
                return View(priserBeskrivning);
            }
        }

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

        /* (Delete). Returnerar ett visst id i tabellen för PriserBeskrivning-databasen och visar upp i en lista i "DeletePriser"-sidan  */
        public IActionResult DeletePriser(int id)
        {
            using (PrisContext db = new PrisContext())
            {
                PriserBeskrivning priserBeskrivning = db.OmPriserBeskrivning.Find(id);
                List<PriserBeskrivning> PriserBeskrivningLista = db.OmPriserBeskrivning.Include(r => r.Beskrivningar).ToList();
                return View(priserBeskrivning);
            }
        }

        /* (Delete) När "radera" knappen klickas uppdateras tabellen med nya värden och sparas i databasen. */
        [HttpPost]
        public IActionResult DeletePriser(PriserBeskrivning priserBeskrivning)
        {
            using (PrisContext db = new PrisContext())
            {
                db.OmPriserBeskrivning.Remove(priserBeskrivning);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
