
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Dtos;
using StockFlux.Hubs;
using StockFlux.Model;

namespace StockFlux.Services
{
    public class EntradaService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<InventarioHub> _hubContext;

        public EntradaService(
            AppDbContext context,
            IHubContext<InventarioHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // Obtener todas las entradas
        public async Task<List<EntradaDto>> ObtenerTodas()
        {
            return await _context.Entradas
                .AsNoTracking()
                .Select(e => new EntradaDto
                {
                    Id = e.Id,
                    ProductoId = e.ProductoId,
                    Cantidad = e.Cantidad,
                    Fecha = e.Fecha
                })
                .ToListAsync();
        }

        // Crear una entrada
        public async Task<EntradaDto> CrearEntrada(EntradaDto entradaDto)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == entradaDto.ProductoId);

            if (producto == null)
            {
                throw new ArgumentException(
                    $"El producto con ID {entradaDto.ProductoId} no existe.");
            }

            if (entradaDto.Cantidad <= 0)
            {
                throw new ArgumentException(
                    "La cantidad debe ser mayor que 0.");
            }

            var entrada = new Entrada
            {
                ProductoId = entradaDto.ProductoId,
                Cantidad = entradaDto.Cantidad,
                Fecha = entradaDto.Fecha
            };

            // Aumentar el stock
            producto.Stock += entradaDto.Cantidad;

            // Guardar la entrada
            _context.Entradas.Add(entrada);

            await _context.SaveChangesAsync();

            // Obtener el ID generado por SQLite
            entradaDto.Id = entrada.Id;

            // Avisar a todos los clientes conectados
            await _hubContext.Clients.All.SendAsync(
                "InventarioActualizado"
            );

            return entradaDto;
        }
    }
}
