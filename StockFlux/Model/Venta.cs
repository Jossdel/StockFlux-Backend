namespace StockFlux.Model
{
    public class Venta

    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

    }
}
