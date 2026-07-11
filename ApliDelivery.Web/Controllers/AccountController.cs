using Microsoft.AspNetCore.Mvc;
using ApliDelivery.Web.Models;
using System.Text.Json;
using ApliDelivery.Web.Services;

namespace ApliDelivery.Web.Controllers
{
    public class AccountController : Controller
    {
        // Patrón Facade
        private readonly IAuthFacade _authFacade;

        public AccountController(IAuthFacade authFacade)
        {
            _authFacade = authFacade;
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var respuesta = await _authFacade.Login(login);

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

        // REGISTRO
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistroDTO registro)
        {
            var respuesta = await _authFacade.Register(registro);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "No fue posible registrar el usuario.";

            return View(registro);
        }

        // RECUPERAR CONTRASEÑA
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(RecuperarPasswordDTO recuperar)
        {
            var respuesta = await _authFacade.RecuperarPassword(recuperar);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "El correo no está registrado.";
                return View(recuperar);
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();

            using var documento = JsonDocument.Parse(resultado);

            ViewBag.Codigo = documento.RootElement.GetProperty("codigo").GetString();
            ViewBag.Correo = recuperar.Correo;
            ViewBag.MostrarCambio = true;

            return View(recuperar);
        }

        // CAMBIAR CONTRASEÑA
        [HttpPost]
        public async Task<IActionResult> CambiarPassword(
            string correo,
            string codigo,
            string nuevaPassword)
        {
            CambiarPasswordDTO cambiar = new CambiarPasswordDTO
            {
                Correo = correo,
                Codigo = codigo,
                NuevaPassword = nuevaPassword
            };

            var respuesta = await _authFacade.CambiarPassword(cambiar);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "No fue posible cambiar la contraseña.";
                return View("ForgotPassword");
            }

            TempData["Mensaje"] = "Contraseña actualizada correctamente.";

            return RedirectToAction("Login");
        }
    }
}