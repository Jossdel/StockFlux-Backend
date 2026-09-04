using Microsoft.AspNetCore.Mvc;
using StockFlux.Dtos;
using StockFlux.Services;

namespace StockFlux.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObtenerCategorias()
        {
            var categorias = await _categoriaService.ObtenerCategorias();

            return Ok(categorias);
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> CrearCategoria(
            [FromBody] CategoriaDto categoriaDto)
        {
            var categoriaCreada =
                await _categoriaService.CrearCategoria(categoriaDto);

            return Ok(categoriaCreada);
        }

        // PUT: api/Categorias/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult<CategoriaDto>> ActualizarCategoria(
            long id,
            [FromBody] CategoriaDto categoriaDto)
        {
            var categoriaActualizada =
                await _categoriaService.ActualizarCategoria(id, categoriaDto);

            if (categoriaActualizada == null)
            {
                return NotFound(new
                {
                    message = "La categoría no existe."
                });
            }

            return Ok(categoriaActualizada);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> EliminarCategoria(long id)
        {
            var eliminada =
                await _categoriaService.EliminarCategoria(id);

            if (!eliminada)
            {
                return NotFound(new
                {
                    message = "La categoría no existe."
                });
            }

            return NoContent();
        }
    }
}