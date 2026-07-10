using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApliDelivery.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string PasswordHash { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        // Llave foránea
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        public ICollection<RecuperacionPassword> RecuperacionesPassword { get; set; }
            = new List<RecuperacionPassword>();
    }
}