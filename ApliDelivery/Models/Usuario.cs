using System.ComponentModel.DataAnnotations;

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


        // Relación con Rol
        public int IdRol { get; set; }

        public Rol Rol { get; set; }


        // Relación con RecuperacionPassword
        public ICollection<RecuperacionPassword> RecuperacionesPassword { get; set; }
    }
}