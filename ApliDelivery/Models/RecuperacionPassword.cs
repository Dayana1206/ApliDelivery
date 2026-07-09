using System.ComponentModel.DataAnnotations;

namespace ApliDelivery.Models
{
    public class RecuperacionPassword
    {
        [Key]
        public int IdRecuperacionPassword { get; set; }

        public string Codigo { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public bool Usado { get; set; }


        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
    }
}