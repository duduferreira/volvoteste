using Microsoft.EntityFrameworkCore;
using volvoTest.Models;

namespace volvoTest.Data
{
    public class VolvoContext : DbContext
    {
        public VolvoContext(DbContextOptions<VolvoContext> options) : base(options)
        {
        }

        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caminhao>().ToTable("Caminhao");
            modelBuilder.Entity<Modelo>().ToTable("Modelo");
        }
    }
}