# StockFlux

# 📦 StockFlux

**StockFlux** es un sistema de gestión de inventario diseñado para pequeños negocios. El proyecto busca facilitar el control de productos, categorías, entradas, salidas y ventas desde una API REST.

## 🚀 Tecnologías

- C#
- ASP.NET Core
- Entity Framework Core
- SQLite
- Swagger
- Git / GitHub

## ✨ Funcionalidades

- 📦 Gestión de productos
- 🏷️ Gestión de categorías
- 📥 Registro de entradas de inventario
- 📤 Registro de salidas de inventario
- 🛒 Gestión de ventas
- 💰 Control de precios y ganancias
- ⚠️ Control de stock mínimo
- 🔎 Búsqueda de productos
- 📊 Información para el dashboard

## 🏗️ Arquitectura

El backend utiliza una separación por capas para mantener el código organizado y facilitar su mantenimiento:

```text
Request
   ↓
Controller
   ↓
Service
   ↓
DbContext
   ↓
SQLite
```

### Estructura del proyecto

```text
StockFlux/
├── Controllers/
├── Data/
│   └── AppDbContext.cs
├── Dtos/
├── Model/
│   ├── Categoria.cs
│   ├── Producto.cs
│   ├── Entrada.cs
│   ├── Salida.cs
│   ├── Venta.cs
│   └── DetalleVenta.cs
├── Services/
├── Migrations/
└── Program.cs
```

## 🗄️ Base de datos

StockFlux utiliza **SQLite** junto con **Entity Framework Core** para almacenar y gestionar la información.

Principales relaciones:

```text
Categoria 1 ──── * Producto
Producto  1 ──── * Entrada
Producto  1 ──── * Salida
Producto  1 ──── * DetalleVenta
Venta     1 ──── * DetalleVenta
```

## 📖 API

La API utiliza endpoints REST para gestionar los diferentes recursos del sistema.

Ejemplo:

```http
GET    /api/productos
POST   /api/productos
GET    /api/productos/buscar
PUT    /api/productos/{id}
DELETE /api/productos/{id}
```

También se utiliza **Swagger** para documentar y probar los endpoints de la API.

## ⚙️ Instalación

Clona el repositorio:

```bash
git clone https://github.com/TU-USUARIO/StockFlux.git
```

Entra al proyecto:

```bash
cd StockFlux
```

Restaura las dependencias:

```bash
dotnet restore
```

Ejecuta las migraciones:

```bash
dotnet ef database update
```

Inicia la aplicación:

```bash
dotnet run
```

Después puedes acceder a Swagger desde la URL mostrada por ASP.NET Core.

## 👨‍💻 Estado del proyecto

🚧 **En desarrollo**

Actualmente se está trabajando en la implementación de los diferentes módulos del sistema y en la integración con el frontend.

## 📌 Objetivo

El objetivo de StockFlux es crear una solución sencilla y práctica que permita a pequeños negocios llevar un mejor control de su inventario, ventas y ganancias.

---

⭐ Si te parece interesante el proyecto, ¡dale una estrella al repositorio!
