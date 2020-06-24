using Kitchen.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Api.Data
{
    public class InventoryContext : DbContext    
    {
        public DbSet<Cupcake> Cupcakes {get; set;}

        public InventoryContext(DbContextOptions<InventoryContext> options) :base(options){
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cupcake>().HasData(Seed.Chocolate, Seed.Vanilla, Seed.RedVelvet, Seed.PumpkinSpice, Seed.Bubblegum, Seed.Unicorn);
        }

        
    }
}