using Microsoft.AspNetCore.Mvc;
using StockFlux.Dtos;
using StockFlux.Services;

namespace StockFlux.Controllers
{
    [Route("api/Salidas")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly SalidaService _salidaService;

        public SalidaController(SalidaService salidaService)
        {
            _salidaService = salidaService;
        }

        // GET: api/Salidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalidaDto>>> ObtenerTodas()
        {
            var salidas = await _salidaService.ObtenerTodas();

            return Ok(salidas);
        }

        // POST: api/Salidas
        [HttpPost]
        public async Task<ActionResult<SalidaDto>> CrearSalida(
            [FromBody] SalidaDto salidaDto)
        {
            try
            {
                var salidaCreada =
                    await _salidaService.CrearSalida(salidaDto);

                return Ok(salidaCreada);
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