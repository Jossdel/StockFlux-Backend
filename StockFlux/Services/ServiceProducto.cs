using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Dtos;
using StockFlux.Model;

namespace StockFlux.Services
{
    public class ServiceProducto
    {
        private readonly AppDbContext _context;

        public ServiceProducto(AppDbContext context)
        {
            _context = context;
        }

        // 1. OBTENER TODOS LOS PRODUCTOS
        public async Task<List<ProductoDto>> ObtenerTodos()
        {
            return await _context.Productos
                .AsNoTracking()
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    Stock = p.Stock,
                    StockMinimo = p.StockMinimo,
                    CategoriaId = p.CategoriaId
                })
                .ToListAsync();
        }

 
        // 3. BUSCAR PRODUCTOS POR NOMBRE (PROTEGIDO CONTRA NULLS)
        public async Task<List<ProductoDto>> BuscarProductos(string? nombre)
        {
            var query = _context.Productos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower()));
            }

            return await query.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                PrecioCompra = p.PrecioCompra,
                PrecioVenta = p.PrecioVenta,
                Stock = p.Stock,
                StockMinimo = p.StockMinimo,
                CategoriaId = p.CategoriaId
            }).ToListAsync();
        }

        // 4. CREAR PRODUCTO (CON VALIDACIÓN DE CATEGORÍA)
        public async Task<ProductoDto> CrearProducto(ProductoDto productoDto)
        {
            // Validar que la categoría exista
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == productoDto.CategoriaId);
            if (!categoriaExiste)
            {
                throw new ArgumentException($"La categoría con ID {productoDto.CategoriaId} no existe.");
            }

            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                PrecioCompra = productoDto.PrecioCompra,
                PrecioVenta = productoDto.PrecioVenta,
                Stock = productoDto.Stock,
                StockMinimo = productoDto.StockMinimo,
                CategoriaId = productoDto.CategoriaId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            productoDto.Id = producto.Id;
            return productoDto;
        }

        // 5. ACTUALIZAR PRODUCTO
        public async Task<ProductoDto?> ActualizarProducto(long id, ProductoDto productoDto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
            {
                return null;
            }

            // Validar categoría si cambió
            if (productoExistente.CategoriaId != productoDto.CategoriaId)
            {
                var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == productoDto.CategoriaId);
                if (!categoriaExiste)
                {
                    throw new ArgumentException($"La categoría con ID {productoDto.CategoriaId} no existe.");
                }
            }

            productoExistente.Nombre = productoDto.Nombre;
            productoExistente.Descripcion = productoDto.Descripcion;
            productoExistente.PrecioCompra = productoDto.PrecioCompra;
            productoExistente.PrecioVenta = productoDto.PrecioVenta;
            productoExistente.Stock = productoDto.Stock;
            productoExistente.StockMinimo = productoDto.StockMinimo;
            productoExistente.CategoriaId = productoDto.CategoriaId;

            await _context.SaveChangesAsync();

            return new ProductoDto
            {
                Id = productoExistente.Id,
                Nombre = productoExistente.Nombre,
                Descripcion = productoExistente.Descripcion,
                PrecioCompra = productoExistente.PrecioCompra,
                PrecioVenta = productoExistente.PrecioVenta,
                Stock = productoExistente.Stock,
                StockMinimo = productoExistente.StockMinimo,
                CategoriaId = productoExistente.CategoriaId,
            };
        }

        // 6. ELIMINAR PRODUCTO
        public async Task<bool> EliminarProducto(long id)
        {
            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
            {
                return false;
            }

            _context.Productos.Remove(productoExistente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}