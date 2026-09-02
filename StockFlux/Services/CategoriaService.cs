using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Dtos;
using StockFlux.Model;

namespace StockFlux.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }
        // Obtener todas las categorías
        public async Task<List<CategoriaDto>> ObtenerCategorias()
        {
            var categorias = await _context.Categorias.Include(c => c.Productos).ToListAsync();
            return categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Name = c.Name,
                Productos = c.Productos.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    Stock = p.Stock,
                    StockMinimo = p.StockMinimo,
                    CategoriaId = p.CategoriaId
                }).ToList()
            }).ToList();
        }
        //crear una nueva categoría
        public async Task<CategoriaDto> CrearCategoria(CategoriaDto categoriaDto)
        {
            var categoria = new Categoria
            {
                Name = categoriaDto.Name
            };

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return new CategoriaDto
            {
                Id = categoria.Id,
                Name = categoria.Name
            };
        }
        // Actualizar una categoría existente
        public async Task<CategoriaDto?> ActualizarCategoria(
            long id,
            CategoriaDto categoriaDto)
        {
            var categoriaExiste = await _context.Categorias.FindAsync(id);

            if (categoriaExiste == null)
            {
                return null;
            }
            
            categoriaExiste.Name = categoriaDto.Name;

            await _context.SaveChangesAsync();

            return new CategoriaDto
            {
                Id = categoriaExiste.Id,
                Name = categoriaExiste.Name
            };
        }
        // Eliminar una categoría existente
        public async Task<bool> EliminarCategoria(long id)
        {
            var categoriaExistente =
                await _context.Categorias.FindAsync(id);

            if (categoriaExistente == null)
            {
                return false;
            }

            _context.Categorias.Remove(categoriaExistente);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}