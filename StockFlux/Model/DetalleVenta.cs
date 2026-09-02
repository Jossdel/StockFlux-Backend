namespace StockFlux.Model
{
    public class DetalleVenta
    {
        public long Id { get; set; }
        public long VentaId { get; set; }
        public Venta Venta { get; set; } = null!;
        public long ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }

        public int Subtotal { get; set; }

    }
}