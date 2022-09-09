using Microsoft.EntityFrameworkCore;
using OrderApi.Models;

namespace OrderApi.Data
{
    // Deze class connect met de database om data te versturen of op te halen
    public class OrderAPIDbContext : DbContext
    {
        public OrderAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        // Deze property werkt als een tabel voor de klant model
        public DbSet<Klant> Klanten  { get; set; }
        public DbSet<Bestelling> Bestellingen { get; set; }
        public DbSet<Product> Producten { get; set; }
    }
}
