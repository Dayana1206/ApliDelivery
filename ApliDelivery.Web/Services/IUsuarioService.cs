using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO?> ObtenerUsuario(int id);

        Task<bool> ActualizarUsuario(int id, ActualizarUsuarioDTO datos);
    }
}