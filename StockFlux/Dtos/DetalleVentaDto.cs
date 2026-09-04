namespace StockFlux.Dtos
{
    public class DetalleVentaDto
    {
        public long Id { get; set; }

        public long VentaId { get; set; }

        public long ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}