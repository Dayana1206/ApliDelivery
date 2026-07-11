using Microsoft.AspNetCore.Mvc;
using ApliDelivery.Web.Models;
using ApliDelivery.Web.Services;

namespace ApliDelivery.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRestauranteService _restauranteService;
        private readonly IProductoService _productoService;

        public ClienteController(
            IUsuarioService usuarioService,
            IRestauranteService restauranteService,
            IProductoService productoService)
        {
            _usuarioService = usuarioService;
            _restauranteService = restauranteService;
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Restaurantes()
        {
            var restaurantes = await _restauranteService.ObtenerRestaurantes();

            return View(restaurantes);
        }

        // NUEVO
        public async Task<IActionResult> Menu(int restauranteId)
        {
            var productos = await _productoService.ObtenerProductosPorRestaurante(restauranteId);

            return View(productos);
        }

        public async Task<IActionResult> Perfil()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = await _usuarioService.ObtenerUsuario(idUsuario.Value);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPerfil(ActualizarUsuarioDTO datos)
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool actualizado = await _usuarioService.ActualizarUsuario(idUsuario.Value, datos);

            if (!actualizado)
            {
                ViewBag.Error = "No fue posible actualizar el perfil.";
            }
            else
            {
                ViewBag.Mensaje = "Perfil actualizado correctamente.";
            }

            var usuario = await _usuarioService.ObtenerUsuario(idUsuario.Value);

            return View("Perfil", usuario);
        }

        public IActionResult Pedidos()
        {
            return View();
        }

        public IActionResult DetallePedido()
        {
            return View();
        }

        public IActionResult Seguimiento()
        {
            return View();
        }
    }
}