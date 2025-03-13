//using System.ComponentModel.DataAnnotations.Schema;

namespace Examinationsuppgift_ASP.Models
{
    // Tabell ett för Priser
    public class Priser
    {
        public int Id { get; set; }

        public string Namn { get; set; }

        public int Pris { get; set; }

        public int PriserBeskrivningId { get; set; } // Property som sammankopplar till tabellen PriserBeskrivning (tabell två) med hjälp av Id-värdet
        public PriserBeskrivning priserBeskrivning { get; set; } // Sammankopplar till tabellen PriserBeskrivning (tabell två) med hjälp av objektet som hämtar alla objekt inom klassen OmPriser.
    }
}
