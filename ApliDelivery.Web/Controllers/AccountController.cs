using Microsoft.AspNetCore.Mvc;
using ApliDelivery.Web.Models;
using System.Text;
using System.Text.Json;

namespace ApliDelivery.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var json = JsonSerializer.Serialize(login);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PostAsync(
                "https://localhost:7200/api/Auth/Login",
                contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View(login);
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();

            using var documento = JsonDocument.Parse(resultado);

            string rol = documento.RootElement.GetProperty("rol").GetString();

            if (rol == "Administrador")
            {
                return RedirectToAction("Index", "Administrador");
            }

            return RedirectToAction("Index", "Cliente");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistroDTO registro)
        {
            var json = JsonSerializer.Serialize(registro);

            var contenido = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PostAsync("https://localhost:7200/api/Auth/Registro", contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "No fue posible registrar el usuario.";

            return View(registro);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}