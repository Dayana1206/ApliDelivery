using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public interface IRestauranteService
    {
        Task<List<RestauranteDTO>?> ObtenerRestaurantes();

        Task<RestauranteDTO?> ObtenerRestaurante(int id);
    }
}