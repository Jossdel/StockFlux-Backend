using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Dtos;
using StockFlux.Model;

namespace StockFlux.Services
{
    public class VentaService
    {
        private readonly AppDbContext _context;

        public VentaService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todas las ventas
        public async Task<List<VentaDto>> ObtenerTodas()
        {
            return await _context.Ventas
                .AsNoTracking()
                .Include(v => v.Detalles)
                .Select(v => new VentaDto
                {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    Total = v.Total,

                    Detalles = v.Detalles.Select(d => new DetalleVentaDto
                    {
                        Id = d.Id,
                        VentaId = d.VentaId,
                        ProductoId = d.ProductoId,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Subtotal
                    }).ToList()
                })
                .ToListAsync();
        }

        // Obtener una venta por ID
        public async Task<VentaDto?> ObtenerPorId(long id)
        {
            var venta = await _context.Ventas
                .AsNoTracking()
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
            {
                return null;
            }

            return new VentaDto
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,

                Detalles = venta.Detalles.Select(d => new DetalleVentaDto
                {
                    Id = d.Id,
                    VentaId = d.VentaId,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }

        // Crear una venta
        public async Task<VentaDto> CrearVenta(VentaDto ventaDto)
        {
            if (ventaDto.Detalles == null || !ventaDto.Detalles.Any())
            {
                throw new ArgumentException(
                    "La venta debe tener al menos un producto.");
            }

            var venta = new Venta
            {
                Fecha = ventaDto.Fecha,
                Total = 0
            };

            foreach (var detalleDto in ventaDto.Detalles)
            {
                var producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Id == detalleDto.ProductoId);

                if (producto == null)
                {
                    throw new ArgumentException(
                        $"El producto con ID {detalleDto.ProductoId} no existe.");
                }

                if (detalleDto.Cantidad <= 0)
                {
                    throw new ArgumentException(
                        "La cantidad debe ser mayor que 0.");
                }

                if (producto.Stock < detalleDto.Cantidad)
                {
                    throw new ArgumentException(
                        $"No hay suficiente stock del producto {producto.Nombre}.");
                }

                var subtotal = detalleDto.Cantidad * detalleDto.PrecioUnitario;

                var detalle = new DetalleVenta
                {
                    ProductoId = detalleDto.ProductoId,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario,
                    Subtotal = subtotal
                };

                venta.Detalles.Add(detalle);

                // Disminuir el stock
                producto.Stock -= detalleDto.Cantidad;

                // Sumar al total
                venta.Total += subtotal;
            }

            _context.Ventas.Add(venta);

            await _context.SaveChangesAsync();

            ventaDto.Id = venta.Id;
            ventaDto.Total = venta.Total;

            return ventaDto;
        }
    }
}