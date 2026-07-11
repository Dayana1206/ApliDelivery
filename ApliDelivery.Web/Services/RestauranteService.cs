using System.Text.Json;
using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly HttpClient _httpClient;

        public RestauranteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RestauranteDTO>?> ObtenerRestaurantes()
        {
            var respuesta = await _httpClient.GetAsync("https://localhost:7200/api/Restaurante");

            if (!respuesta.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await respuesta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<RestauranteDTO>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<RestauranteDTO?> ObtenerRestaurante(int id)
        {
            var respuesta = await _httpClient.GetAsync($"https://localhost:7200/api/Restaurante/{id}");

            if (!respuesta.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await respuesta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RestauranteDTO>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}