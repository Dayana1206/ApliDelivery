using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApliDelivery.Data;
using ApliDelivery.Models;

namespace ApliDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly ApliDeliveryContext _context;

        public RolController(ApliDeliveryContext context)
        {
            _context = context;
        }


        // Obtener todos los roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }


        // Obtener rol por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }


        // Crear rol
        [HttpPost]
        public async Task<ActionResult<Rol>> CrearRol(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            return Ok(rol);
        }


        // Actualizar rol
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, Rol rol)
        {
            if (id != rol.IdRol)
            {
                return BadRequest();
            }

            _context.Entry(rol).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Eliminar rol
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(rol);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}