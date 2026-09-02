
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
        // GET: api/Productos
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto([FromBody] ProductoDto productoDto)
        {
            var productoCreado = await _serviceProducto.CrearProducto(productoDto);
            return Ok(productoCreado);
        }
        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> BuscarProductos([FromQuery] ProductoDto productoDto)
        {
            var productos = await _serviceProducto.BuscarProductos(productoDto);
            return Ok(productos);
        }

    }
}
