namespace StockFlux.Model
{
    public class Salida
    {
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public string Motivo { get; set; }= string.Empty;
        public DateTime Fecha { get; set; }
    }
}
