using Microsoft.EntityFrameworkCore;

namespace Examinationsuppgift_ASP.Models
{
    // Kopplingen till filvägsDatabas
    public class PrisContext : DbContext // Skapar databasen i en s.k. constructor (PrisContext) om den inte redan finns
    {
        // Specifierar vad databsen ska innehålla med hjälp av "code first". Hämtar data från klassen Priser.
        public DbSet<Priser> OmPriserna {  get; set; }
        public DbSet<PriserBeskrivning> OmPriserBeskrivning { get; set; } // Hämtar data från klassen PriserBeskrivning.
        public PrisContext()          // Constructor som kontrollerar om databasen finnns.
        {
            Database.EnsureCreated();   
        }

        // Specifierar vilken databas som används
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=prisData.db");
        }
    }
}
