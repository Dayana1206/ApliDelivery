using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public interface ICarritoService
    {
        List<CarritoItemDTO> ObtenerCarrito();

        void AgregarProducto(ProductoDTO producto);

        void EliminarProducto(int productoId);

        void VaciarCarrito();
    }
}