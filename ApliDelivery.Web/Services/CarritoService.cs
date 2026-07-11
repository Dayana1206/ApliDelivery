using System.Text.Json;
using ApliDelivery.Web.Models;
using Microsoft.AspNetCore.Http;

namespace ApliDelivery.Web.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string SESSION_KEY = "Carrito";

        public CarritoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CarritoItemDTO> ObtenerCarrito()
        {
            var session = _httpContextAccessor.HttpContext!.Session;

            var carritoJson = session.GetString(SESSION_KEY);

            if (string.IsNullOrEmpty(carritoJson))
                return new List<CarritoItemDTO>();

            return JsonSerializer.Deserialize<List<CarritoItemDTO>>(carritoJson)!
                   ?? new List<CarritoItemDTO>();
        }

        public void AgregarProducto(ProductoDTO producto)
        {
            var carrito = ObtenerCarrito();

            var existente = carrito.FirstOrDefault(x => x.ProductoId == producto.ProductoId);

            if (existente != null)
            {
                existente.Cantidad++;
            }
            else
            {
                carrito.Add(new CarritoItemDTO
                {
                    ProductoId = producto.ProductoId,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = 1
                });
            }

            Guardar(carrito);
        }

        public void EliminarProducto(int productoId)
        {
            var carrito = ObtenerCarrito();

            carrito.RemoveAll(x => x.ProductoId == productoId);

            Guardar(carrito);
        }

        public void VaciarCarrito()
        {
            _httpContextAccessor.HttpContext!.Session.Remove(SESSION_KEY);
        }

        private void Guardar(List<CarritoItemDTO> carrito)
        {
            var json = JsonSerializer.Serialize(carrito);

            _httpContextAccessor.HttpContext!.Session.SetString(SESSION_KEY, json);
        }
    }
}