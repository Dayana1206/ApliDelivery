using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApliDelivery.Data;
using ApliDelivery.Models;

namespace ApliDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestauranteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurante>>> GetRestaurantes()
        {
            return await _context.Restaurantes.ToListAsync();
        }

        // GET: api/Restaurante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurante>> GetRestaurante(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            return restaurante;
        }

        // POST: api/Restaurante
        [HttpPost]
        public async Task<ActionResult<Restaurante>> PostRestaurante(Restaurante restaurante)
        {
            _context.Restaurantes.Add(restaurante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurante), new { id = restaurante.RestauranteId }, restaurante);
        }

        // PUT: api/Restaurante/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurante(int id, Restaurante restaurante)
        {
            if (id != restaurante.RestauranteId)
            {
                return BadRequest();
            }

            _context.Entry(restaurante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Restaurantes.Any(e => e.RestauranteId == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Restaurante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurante(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            _context.Restaurantes.Remove(restaurante);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}