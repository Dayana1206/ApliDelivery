using Microsoft.EntityFrameworkCore;
using ApliDelivery.Models;

namespace ApliDelivery.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurante> Restaurantes { get; set; }

        public DbSet<Producto> Productos { get; set; }
    }
}