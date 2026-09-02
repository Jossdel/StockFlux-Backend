namespace StockFlux.Model;


    public class Categoria
    {
        public long Id { get; set; }
    
        public string Name { get; set; } = string.Empty;
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }

