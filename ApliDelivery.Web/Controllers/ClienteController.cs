using Microsoft.AspNetCore.Mvc;

namespace ApliDelivery.Web.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
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