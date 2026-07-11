using System.Text;
using System.Text.Json;
using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public class AuthFacade : IAuthFacade
    {
        private readonly HttpClient _httpClient;

        public AuthFacade(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Login(LoginDTO login)
        {
            var json = JsonSerializer.Serialize(login);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            return await _httpClient.PostAsync(
                "https://localhost:7200/api/Auth/Login",
                contenido);
        }

        public async Task<HttpResponseMessage> Register(RegistroDTO registro)
        {
            var json = JsonSerializer.Serialize(registro);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            return await _httpClient.PostAsync(
                "https://localhost:7200/api/Auth/Registro",
                contenido);
        }

        public async Task<HttpResponseMessage> RecuperarPassword(RecuperarPasswordDTO recuperar)
        {
            var json = JsonSerializer.Serialize(recuperar);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            return await _httpClient.PostAsync(
                "https://localhost:7200/api/Auth/RecuperarPassword",
                contenido);
        }

        public async Task<HttpResponseMessage> CambiarPassword(CambiarPasswordDTO cambiar)
        {
            var json = JsonSerializer.Serialize(cambiar);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            return await _httpClient.PostAsync(
                "https://localhost:7200/api/Auth/CambiarPassword",
                contenido);
        }
    }
}
