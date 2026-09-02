namespace StockFlux.Model
{
    public class Entrada
    {
        public long Id { get; set; }
        public long  ProductoId  { get; set; }

        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

    }
}
