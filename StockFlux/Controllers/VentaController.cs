using Microsoft.AspNetCore.Mvc;
using StockFlux.Dtos;
using StockFlux.Services;

namespace StockFlux.Controllers
{
    [Route("api/Ventas")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaService _ventaService;

        public VentaController(VentaService ventaService)
        {
            _ventaService = ventaService;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDto>>> ObtenerTodas()
        {
            var ventas = await _ventaService.ObtenerTodas();

            return Ok(ventas);
        }

        // GET: api/Ventas/1
        [HttpGet("{id:long}")]
        public async Task<ActionResult<VentaDto>> ObtenerPorId(long id)
        {
            var venta = await _ventaService.ObtenerPorId(id);

            if (venta == null)
            {
                return NotFound(new
                {
                    message = "La venta no existe."
                });
            }

            return Ok(venta);
        }

        // POST: api/Ventas
        [HttpPost]
        public async Task<ActionResult<VentaDto>> CrearVenta(
            [FromBody] VentaDto ventaDto)
        {
            try
            {
                var ventaCreada =
                    await _ventaService.CrearVenta(ventaDto);

                return Ok(ventaCreada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}