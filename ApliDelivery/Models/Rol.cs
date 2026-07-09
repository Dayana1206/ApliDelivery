using System.ComponentModel.DataAnnotations;

namespace ApliDelivery.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        public string Nombre { get; set; }


        // Relación con Usuario
        public ICollection<Usuario> Usuarios { get; set; }
    }
}