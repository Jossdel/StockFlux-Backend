using Microsoft.AspNetCore.Mvc;
using StockFlux.Dtos;
using StockFlux.Services;

namespace StockFlux.Controllers
{
    [Route("api/Dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/Dashboard/resumen
        [HttpGet("resumen")]
        public async Task<ActionResult<DashboardDto>> ObtenerResumen()
        {
            var resumen = await _dashboardService.ObtenerResumen();

            return Ok(resumen);
        }
    }
}