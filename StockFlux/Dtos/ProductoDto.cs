namespace StockFlux.Dtos
{
    public class ProductoDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public long CategoriaId { get; set; }
    }
}
