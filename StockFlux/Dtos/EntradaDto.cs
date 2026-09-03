namespace StockFlux.Dtos
{
    public class EntradaDto
    {
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
