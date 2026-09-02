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
        //crear producto
        public async Task<ProductoDto> CrearProducto(ProductoDto productoDto)
        {
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
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioCompra = producto.PrecioCompra,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                StockMinimo = producto.StockMinimo,
                CategoriaId = producto.CategoriaId
            };
        }
        // Obtener todos los productos
        public async Task<List<ProductoDto>> BuscarProductos( ProductoDto productoDto)
        {
        return await _context.Productos.Where(p => p.Nombre.Contains(productoDto.Nombre)).Select(p => new ProductoDto 
        { Id = p.Id,
          Nombre = p.Nombre,
          Descripcion = p.Descripcion,
          PrecioCompra = p.PrecioCompra,
          PrecioVenta = p.PrecioVenta,
          Stock = p.Stock,
          StockMinimo = p.StockMinimo,
          CategoriaId = p.CategoriaId
        }).ToListAsync();
        }

    }
}
