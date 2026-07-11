using System.Text.Json;
using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoDTO>?> ObtenerProductosPorRestaurante(int restauranteId)
        {
            var respuesta = await _httpClient.GetAsync(
                $"https://localhost:7200/api/Producto/Restaurante/{restauranteId}");

            if (!respuesta.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await respuesta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ProductoDTO>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<ProductoDTO?> ObtenerProducto(int id)
        {
            var respuesta = await _httpClient.GetAsync(
                $"https://localhost:7200/api/Producto/{id}");

            if (!respuesta.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await respuesta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProductoDTO>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}