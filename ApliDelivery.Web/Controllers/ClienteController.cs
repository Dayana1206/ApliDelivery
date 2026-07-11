using Microsoft.AspNetCore.Mvc;
using ApliDelivery.Web.Models;
using ApliDelivery.Web.Services;
using System.Text.Json;


namespace ApliDelivery.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRestauranteService _restauranteService;
        private readonly IProductoService _productoService;
        private readonly ICarritoService _carritoService;

        public ClienteController(
            IUsuarioService usuarioService,
            IRestauranteService restauranteService,
            IProductoService productoService,
            ICarritoService carritoService)
        {
            _usuarioService = usuarioService;
            _restauranteService = restauranteService;
            _productoService = productoService;
            _carritoService = carritoService;
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

        public async Task<IActionResult> Menu(int restauranteId)
        {
            var productos = await _productoService
                .ObtenerProductosPorRestaurante(restauranteId);

            return View(productos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(
            int productoId,
            int restauranteId)
        {
            var producto = await _productoService.ObtenerProducto(productoId);

            if (producto == null)
            {
                TempData["Error"] = "No se encontró el producto.";

                return RedirectToAction(
                    "Menu",
                    new { restauranteId });
            }

            _carritoService.AgregarProducto(producto);

            TempData["Mensaje"] = "Producto agregado al carrito.";

            return RedirectToAction(
                "Menu",
                new { restauranteId });
        }

        public IActionResult Carrito()
        {
            var carrito = _carritoService.ObtenerCarrito();
            return View(carrito);
        }

        [HttpPost]
        public IActionResult EliminarDelCarrito(int productoId)
        {
            _carritoService.EliminarProducto(productoId);
            return RedirectToAction("Carrito");
        }

        [HttpPost]
        public IActionResult VaciarCarrito()
        {
            _carritoService.VaciarCarrito();
            return RedirectToAction("Carrito");
        }
        [HttpPost]
        public IActionResult ConfirmarPedido()
        {
            var carrito = _carritoService.ObtenerCarrito();

            if (carrito.Count == 0)
            {
                TempData["Error"] = "El carrito está vacío.";
                return RedirectToAction("Carrito");
            }

            var session = HttpContext.Session;

            var pedidosJson = session.GetString("Pedidos");

            List<PedidoDTO> pedidos;

            if (string.IsNullOrEmpty(pedidosJson))
            {
                pedidos = new List<PedidoDTO>();
            }
            else
            {
                pedidos = JsonSerializer.Deserialize<List<PedidoDTO>>(pedidosJson)
                          ?? new List<PedidoDTO>();
            }

            var nuevoPedido = new PedidoDTO
            {
                PedidoId = pedidos.Count + 1,
                Fecha = DateTime.Now,
                Estado = "Preparando",
                Cliente = HttpContext.Session.GetString("Nombre") ?? "",
                Productos = carrito
            };

            pedidos.Add(nuevoPedido);

            session.SetString(
                "Pedidos",
                JsonSerializer.Serialize(pedidos));

            _carritoService.VaciarCarrito();

            TempData["PedidoConfirmado"] =
                "Pedido confirmado correctamente.";

            return RedirectToAction("Pedidos");
        }

        public async Task<IActionResult> Perfil()
        {
            int? idUsuario =
                HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario =
                await _usuarioService.ObtenerUsuario(idUsuario.Value);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPerfil(
            ActualizarUsuarioDTO datos)
        {
            int? idUsuario =
                HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool actualizado =
                await _usuarioService.ActualizarUsuario(
                    idUsuario.Value,
                    datos);

            if (!actualizado)
            {
                ViewBag.Error =
                    "No fue posible actualizar el perfil.";
            }
            else
            {
                ViewBag.Mensaje =
                    "Perfil actualizado correctamente.";
            }

            var usuario =
                await _usuarioService.ObtenerUsuario(
                    idUsuario.Value);

            return View("Perfil", usuario);
        }

        public IActionResult Pedidos()
        {
            var pedidosJson = HttpContext.Session.GetString("Pedidos");

            List<PedidoDTO> pedidos;

            if (string.IsNullOrEmpty(pedidosJson))
            {
                pedidos = new List<PedidoDTO>();
            }
            else
            {
                pedidos = System.Text.Json.JsonSerializer.Deserialize<List<PedidoDTO>>(pedidosJson)
                          ?? new List<PedidoDTO>();
            }

            return View(pedidos);
        }

        public IActionResult DetallePedido(int id)
        {
            var pedidosJson = HttpContext.Session.GetString("Pedidos");

            if (string.IsNullOrEmpty(pedidosJson))
            {
                return RedirectToAction("Pedidos");
            }

            var pedidos = System.Text.Json.JsonSerializer.Deserialize<List<PedidoDTO>>(pedidosJson);

            if (pedidos == null)
            {
                return RedirectToAction("Pedidos");
            }

            var pedido = pedidos.FirstOrDefault(p => p.PedidoId == id);

            if (pedido == null)
            {
                return RedirectToAction("Pedidos");
            }

            return View(pedido);
        }

        public IActionResult Seguimiento(int id)
        {
            var pedidosJson = HttpContext.Session.GetString("Pedidos");

            if (string.IsNullOrEmpty(pedidosJson))
            {
                return RedirectToAction("Pedidos");
            }

            var pedidos = System.Text.Json.JsonSerializer.Deserialize<List<PedidoDTO>>(pedidosJson);

            if (pedidos == null)
            {
                return RedirectToAction("Pedidos");
            }

            var pedido = pedidos.FirstOrDefault(p => p.PedidoId == id);

            if (pedido == null)
            {
                return RedirectToAction("Pedidos");
            }

            return View(pedido);
        }
    }
}