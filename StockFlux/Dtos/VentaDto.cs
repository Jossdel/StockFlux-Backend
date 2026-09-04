

namespace StockFlux.Dtos
{
    public class VentaDto
    {
        public long Id { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public List<DetalleVentaDto> Detalles { get; set; } = new ();
    }
}