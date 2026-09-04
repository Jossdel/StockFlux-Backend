namespace StockFlux.Dtos
{
    public class DashboardDto
    {
        public decimal TotalVentas { get; set; }

        public int TotalProductos { get; set; }

        public int ProductosStockBajo { get; set; }

        public decimal Ganancias { get; set; }
    }
}