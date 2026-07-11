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

        // POST: api/Auth/RecuperarPassword
        [HttpPost("RecuperarPassword")]
        public async Task<IActionResult> RecuperarPassword(RecuperarPasswordDTO recuperar)
        {
            // Buscar usuario por correo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == recuperar.Correo);

            if (usuario == null)
            {
                return BadRequest(new
                {
                    mensaje = "El correo no está registrado."
                });
            }

            // Generar código de 6 dígitos
            Random random = new Random();
            string codigo = random.Next(100000, 999999).ToString();

            // Crear registro de recuperación
            RecuperacionPassword recuperacion = new RecuperacionPassword
            {
                Codigo = codigo,
                FechaExpiracion = DateTime.Now.AddMinutes(15),
                Usado = false,
                IdUsuario = usuario.IdUsuario
            };

            _context.RecuperacionesPassword.Add(recuperacion);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Código generado correctamente.",
                codigo = codigo
            });
        }

        // POST: api/Auth/CambiarPassword
        [HttpPost("CambiarPassword")]
        public async Task<IActionResult> CambiarPassword(CambiarPasswordDTO cambiar)
        {
            // Buscar usuario
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == cambiar.Correo);

            if (usuario == null)
            {
                return BadRequest(new
                {
                    mensaje = "El correo no existe."
                });
            }

            // Buscar código
            var recuperacion = await _context.RecuperacionesPassword
                .Where(r => r.IdUsuario == usuario.IdUsuario &&
                            r.Codigo == cambiar.Codigo &&
                            r.Usado == false)
                .OrderByDescending(r => r.IdRecuperacionPassword)
                .FirstOrDefaultAsync();

            if (recuperacion == null)
            {
                return BadRequest(new
                {
                    mensaje = "Código incorrecto."
                });
            }

            // Verificar expiración
            if (recuperacion.FechaExpiracion < DateTime.Now)
            {
                return BadRequest(new
                {
                    mensaje = "El código ha expirado."
                });
            }

            // Cambiar contraseña
            usuario.PasswordHash = cambiar.NuevaPassword;

            // Marcar código como usado
            recuperacion.Usado = true;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Contraseña actualizada correctamente."
            });
        }
    }
}