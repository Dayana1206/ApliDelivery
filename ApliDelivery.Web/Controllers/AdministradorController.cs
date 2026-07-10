using Microsoft.AspNetCore.Mvc;

namespace ApliDelivery.Web.Controllers
{
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Clientes()
        {
            return View();
        }

        public IActionResult Administradores()
        {
            return View();
        }
        public IActionResult Reportes()
        {
            return View();
        }
    }
}