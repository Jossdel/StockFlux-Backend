using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Services;
using StockFlux.Hubs;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// SERVICES
// =====================================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// =====================================================
// SERVICIOS
// =====================================================

builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ServiceProducto>();
builder.Services.AddScoped<SalidaService>();
builder.Services.AddScoped<EntradaService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<VentaService>();

// =====================================================
// DATABASE - SQLITE
// =====================================================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// =====================================================
// SIGNALR
// =====================================================

builder.Services.AddSignalR();

// =====================================================
// CORS
// =====================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// =====================================================
// BUILD APP
// =====================================================

var app = builder.Build();

// =====================================================
// HTTP REQUEST PIPELINE
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS debe ir ANTES de HttpsRedirection
app.UseCors("Frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

// =====================================================
// CONTROLLERS
// =====================================================

app.MapControllers();

// =====================================================
// SIGNALR HUB
// =====================================================

app.MapHub<InventarioHub>("/hubs/inventario");

// =====================================================
// RUN
// =====================================================

app.Run();