using System.ComponentModel.DataAnnotations;

namespace StockFlux.Dtos
{
    public class CategoriaDto
    {
        public long Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string Name { get; set; } = string.Empty;
        public List<ProductoDto> Productos { get; set; } = new List<ProductoDto>();
    }
}
