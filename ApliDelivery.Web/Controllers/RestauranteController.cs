using Microsoft.AspNetCore.Mvc;

namespace ApliDelivery.Web.Controllers
{
    public class RestauranteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Detalle()
        {
            return View();
        }
    }
}