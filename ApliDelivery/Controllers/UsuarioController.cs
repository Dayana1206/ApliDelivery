using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApliDelivery.Data;
using ApliDelivery.Models;
using ApliDelivery.DTOs;

namespace ApliDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApliDeliveryContext _context;

        public UsuarioController(ApliDeliveryContext context)
        {
            _context = context;
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }

        // Obtener usuario por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // Crear usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        // Actualizar usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, ActualizarUsuarioDTO datos)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Solo actualizar los campos permitidos
            usuario.Telefono = datos.Telefono;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Perfil actualizado correctamente."
            });
        }

        // Eliminar usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}