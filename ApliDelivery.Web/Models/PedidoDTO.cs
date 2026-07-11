namespace ApliDelivery.Web.Models
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }

        public DateTime Fecha { get; set; }

        public string Cliente { get; set; } = string.Empty;

        public List<CarritoItemDTO> Productos { get; set; }
            = new List<CarritoItemDTO>();

        public decimal Total
        {
            get
            {
                return Productos.Sum(x => x.Subtotal);
            }
        }

        public string Estado { get; set; } = "Preparando";
    }
}