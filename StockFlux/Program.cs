using Microsoft.EntityFrameworkCore;
using StockFlux.Data;
using StockFlux.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ServiceProducto>();

// Configuracion de SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(
builder.Configuration.GetConnectionString("DefaultConnection")
)
);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
