using Microsoft.EntityFrameworkCore;
using StockFlux.Model;


namespace StockFlux.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Entrada> Entradas { get; set; }

        public DbSet<Salida> Salidas { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Categoria -> Productos
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);

            // Venta -> DetallesVenta
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.VentaId);

            // Producto -> DetallesVenta
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId);

            // Producto -> Entradas
            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Producto)
                .WithMany()
                .HasForeignKey(e => e.ProductoId);

            // Producto -> Salidas
            modelBuilder.Entity<Salida>()
                .HasOne(s => s.Producto)
                .WithMany()
                .HasForeignKey(s => s.ProductoId);
        }
    }

}