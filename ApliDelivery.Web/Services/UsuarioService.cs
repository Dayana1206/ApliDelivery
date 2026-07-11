using System.Text;
using System.Text.Json;
using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioDTO?> ObtenerUsuario(int id)
        {
            var respuesta = await _httpClient.GetAsync($"https://localhost:7200/api/Usuario/{id}");

            if (!respuesta.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await respuesta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UsuarioDTO>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<bool> ActualizarUsuario(int id, ActualizarUsuarioDTO datos)
        {
            var json = JsonSerializer.Serialize(datos);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PutAsync(
                $"https://localhost:7200/api/Usuario/{id}",
                contenido);

            return respuesta.IsSuccessStatusCode;
        }
    }
}