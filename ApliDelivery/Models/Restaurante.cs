namespace ApliDelivery.Models
{
    public class Restaurante
    {
        public int RestauranteId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public double Latitud { get; set; }

        public double Longitud { get; set; }

        public string Horario { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}