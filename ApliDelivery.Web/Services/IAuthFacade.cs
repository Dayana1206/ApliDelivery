using ApliDelivery.Web.Models;

namespace ApliDelivery.Web.Services
{
    public interface IAuthFacade
    {
        Task<HttpResponseMessage> Login(LoginDTO login);

        Task<HttpResponseMessage> Register(RegistroDTO registro);

        Task<HttpResponseMessage> RecuperarPassword(RecuperarPasswordDTO recuperar);

        Task<HttpResponseMessage> CambiarPassword(CambiarPasswordDTO cambiar);
    }
}