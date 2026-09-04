using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Dtos;
using StockFlux.Model;

namespace StockFlux.Services
{
    public class SalidaService
    {
        private readonly AppDbContext _context;

        public SalidaService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todas las salidas
        public async Task<List<SalidaDto>> ObtenerTodas()
        {
            return await _context.Salidas
                .AsNoTracking()
                .Select(s => new SalidaDto
                {
                    Id = s.Id,
                    ProductoId = s.ProductoId,
                    Cantidad = s.Cantidad,
                    Motivo = s.Motivo,
                    Fecha = s.Fecha
                })
                .ToListAsync();
        }

        // Crear una salida
        public async Task<SalidaDto> CrearSalida(SalidaDto salidaDto)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == salidaDto.ProductoId);

            if (producto == null)
            {
                throw new ArgumentException(
                    $"El producto con ID {salidaDto.ProductoId} no existe.");
            }

            if (salidaDto.Cantidad <= 0)
            {
                throw new ArgumentException(
                    "La cantidad debe ser mayor que 0.");
            }

            if (producto.Stock < salidaDto.Cantidad)
            {
                throw new ArgumentException(
                    "No hay suficiente stock para realizar la salida.");
            }

            var salida = new Salida
            {
                ProductoId = salidaDto.ProductoId,
                Cantidad = salidaDto.Cantidad,
                Motivo = salidaDto.Motivo,
                Fecha = salidaDto.Fecha
            };

            // Disminuir el stock
            producto.Stock -= salidaDto.Cantidad;

            _context.Salidas.Add(salida);

            await _context.SaveChangesAsync();

            salidaDto.Id = salida.Id;

            return salidaDto;
        }
    }
}