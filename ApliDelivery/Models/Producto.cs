namespace ApliDelivery.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Imagen { get; set; } = string.Empty;

        public bool Disponible { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public int RestauranteId { get; set; }

        public Restaurante? Restaurante { get; set; }
    }
}