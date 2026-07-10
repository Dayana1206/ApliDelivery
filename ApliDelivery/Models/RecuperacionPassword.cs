using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}