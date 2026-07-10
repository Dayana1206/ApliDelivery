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

        public DbSet<Restaurante> Restaurantes { get; set; }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuario -> Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            // RecuperacionPassword -> Usuario
            modelBuilder.Entity<RecuperacionPassword>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.RecuperacionesPassword)
                .HasForeignKey(r => r.IdUsuario);

            // Producto -> Restaurante
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Restaurante)
                .WithMany(r => r.Productos)
                .HasForeignKey(p => p.RestauranteId);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2);
        }
    }
}