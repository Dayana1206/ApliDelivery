using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApliDelivery.Data;
using ApliDelivery.Models;

namespace ApliDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApliDeliveryContext _context;

        public ProductoController(ApliDeliveryContext context)
        {
            _context = context;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .Include(p => p.Restaurante)
                .ToListAsync();
        }

        // Obtener productos de un restaurante
        [HttpGet("Restaurante/{restauranteId}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosPorRestaurante(int restauranteId)
        {
            var productos = await _context.Productos
                .Where(p => p.RestauranteId == restauranteId && p.Disponible)
                .ToListAsync();

            return Ok(productos);
        }

        // Obtener un producto por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // Crear producto
        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProducto),
                new { id = producto.ProductoId },
                producto);
        }
    }
}