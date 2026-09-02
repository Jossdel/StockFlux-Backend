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
        public async Task<ActionResult<IEnumerable<CategoriaDto>>>ObtenerCategorias()
        {
            var categorias = await _categoriaService.ObtenerCategorias();
            return Ok(categorias);
        }
        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> CrearCategoria([FromBody] CategoriaDto categoriaDto)
        {
            var categoriaCreada= await _categoriaService.CrearCategoria(categoriaDto);
            return Ok(categoriaCreada);
        }
    }
}
