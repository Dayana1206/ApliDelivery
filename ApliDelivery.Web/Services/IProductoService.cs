using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>?> ObtenerProductosPorRestaurante(int restauranteId);

        Task<ProductoDTO?> ObtenerProducto(int id);
    }
}