using System.ComponentModel.DataAnnotations;

namespace StockFlux.Dtos
{
    public class ProductoDto
    {
        public long Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        [Required]
        public decimal PrecioCompra { get; set; }
        [Required]
        public decimal PrecioVenta { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int StockMinimo { get; set; }
        [Required]
        public long CategoriaId { get; set; }
    }
}
