using Microsoft.EntityFrameworkCore;
using ApliDelivery.Models;

namespace ApliDelivery.Data
{
    public class ApliDeliveryContext : DbContext
    {
        public ApliDeliveryContext(DbContextOptions<ApliDeliveryContext> options)
            : base(options)
        {

        }


        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<RecuperacionPassword> RecuperacionesPassword { get; set; }
    }
}