using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApliDelivery.Data;
using ApliDelivery.Models;
using ApliDelivery.DTOs;

namespace ApliDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApliDeliveryContext _context;

        public AuthController(ApliDeliveryContext context)
        {
            _context = context;
        }

        // ===========================
        // REGISTRO
        // ===========================
        [HttpPost("Registro")]
        public async Task<IActionResult> Registro(RegistroDTO registro)
        {
            // Verificar si el correo ya existe
            var usuarioExiste = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == registro.Correo);

            if (usuarioExiste != null)
            {
                return BadRequest(new
                {
                    mensaje = "El correo ya está registrado."
                });
            }

            // Buscar el rol Cliente
            var rolCliente = await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == "Cliente");

            if (rolCliente == null)
            {
                return BadRequest(new
                {
                    mensaje = "No existe el rol Cliente."
                });
            }

            // Crear usuario
            Usuario usuario = new Usuario
            {
                Nombre = registro.Nombre,
                Apellido = registro.Apellido,
                Correo = registro.Correo,
                Telefono = registro.Telefono,
                PasswordHash = registro.Password,
                Estado = true,
                FechaRegistro = DateTime.Now,
                IdRol = rolCliente.IdRol
            };

            // Guardar usuario
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente.",
                usuario = usuario.Nombre
            });
        }

        // ===========================
        // LOGIN
        // ===========================
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            // Buscar usuario por correo
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == login.Correo);

            if (usuario == null)
            {
                return BadRequest(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }

            // Verificar contraseña
            if (usuario.PasswordHash != login.Password)
            {
                return BadRequest(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }

            // Login exitoso
            return Ok(new
            {
                mensaje = "Inicio de sesión correcto.",
                idUsuario = usuario.IdUsuario,
                nombre = usuario.Nombre,
                rol = usuario.Rol.Nombre
            });
        }
    }
}