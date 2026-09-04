using Microsoft.AspNetCore.Mvc;
using StockFlux.Dtos;
using StockFlux.Services;

namespace StockFlux.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ServiceProducto _serviceProducto;

        public ProductoController(ServiceProducto serviceProducto)
        {
            _serviceProducto = serviceProducto;
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto(
            [FromBody] ProductoDto productoDto)
        {
            try
            {
                var productoCreado =
                    await _serviceProducto.CrearProducto(productoDto);

                return Ok(productoCreado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ObtenerTodos()
        {
            var productos = await _serviceProducto.ObtenerTodos();

            return Ok(productos);
        }

        // GET: api/Productos/buscar?nombre=Iphone
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> BuscarProductos(
            [FromQuery] string? nombre)
        {
            var productos =
                await _serviceProducto.BuscarProductos(nombre);

            return Ok(productos);
        }

        // PUT: api/Productos/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ProductoDto>> ActualizarProducto(
            long id,
            [FromBody] ProductoDto productoDto)
        {
            try
            {
                var productoActualizado =
                    await _serviceProducto.ActualizarProducto(id, productoDto);

                if (productoActualizado == null)
                {
                    return NotFound(new
                    {
                        message = "El producto no existe."
                    });
                }

                return Ok(productoActualizado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> EliminarProducto(long id)
        {
            var eliminado =
                await _serviceProducto.EliminarProducto(id);

            if (!eliminado)
            {
                return NotFound(new
                {
                    message = "El producto no existe."
                });
            }

            return NoContent();
        }
    }
}