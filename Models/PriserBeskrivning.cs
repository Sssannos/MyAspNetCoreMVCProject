namespace Examinationsuppgift_ASP.Models
{
    // Tabell två för Beskrivning.
    public class PriserBeskrivning
    {
        public int Id { get; set; }
        public string Beskrivning { get; set; }
        public List<Priser> Beskrivningar { get; set; } // Om ett Id har 1:M relation, d.v.s, fler än ett värde per Id.
    }
}
