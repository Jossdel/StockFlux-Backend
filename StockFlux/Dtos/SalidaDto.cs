namespace StockFlux.Dtos
{
    public class SalidaDto
    {
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}
