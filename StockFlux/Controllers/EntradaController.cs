using Microsoft.AspNetCore.Mvc;
using StockFlux.Dtos;
using StockFlux.Services;

namespace StockFlux.Controllers
{
    [Route("api/Entradas")]
    [ApiController]
    public class EntradaController : ControllerBase
    {
        private readonly EntradaService _entradaService;

        public EntradaController(EntradaService entradaService)
        {
            _entradaService = entradaService;
        }

        // GET: api/Entradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntradaDto>>> ObtenerTodas()
        {
            var entradas = await _entradaService.ObtenerTodas();

            return Ok(entradas);
        }

        // POST: api/Entradas
        [HttpPost]
        public async Task<ActionResult<EntradaDto>> CrearEntrada(
            [FromBody] EntradaDto entradaDto)
        {
            try
            {
                var entradaCreada =
                    await _entradaService.CrearEntrada(entradaDto);

                return Ok(entradaCreada);
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