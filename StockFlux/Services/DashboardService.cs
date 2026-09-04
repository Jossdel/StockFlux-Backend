using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Dtos;

namespace StockFlux.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> ObtenerResumen()
        {
            var totalVentas = await _context.Ventas
                .SumAsync(v => v.Total);

            var totalProductos = await _context.Productos
                .CountAsync();

            var productosStockBajo = await _context.Productos
                .CountAsync(p => p.Stock <= p.StockMinimo);

            var ganancias = await _context.DetallesVenta
                .SumAsync(d =>
                    d.Subtotal - (d.Producto.PrecioCompra * d.Cantidad));

            return new DashboardDto
            {
                TotalVentas = totalVentas,
                TotalProductos = totalProductos,
                ProductosStockBajo = productosStockBajo,
                Ganancias = ganancias
            };
        }
    }
}