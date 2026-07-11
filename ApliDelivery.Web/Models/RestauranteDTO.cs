namespace ApliDelivery.Web.Models
{
    public class RestauranteDTO
    {
        public int RestauranteId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Horario { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;
    }
}