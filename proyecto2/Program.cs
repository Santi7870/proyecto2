using Microsoft.EntityFrameworkCore;
using proyecto2.Models;
using Microsoft.Extensions.DependencyInjection;
using proyecto2.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión para la base de datos (sólo una vez)
builder.Services.AddDbContext<Proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Proyecto2Context") ?? 
                         throw new InvalidOperationException("Connection string 'proyecto2Context' not found.")));

// Agregar servicios MVC (Controladores y Vistas)
builder.Services.AddControllersWithViews();

// Configurar la sesión (si es necesario)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Agregar caché distribuido en memoria
builder.Services.AddDistributedMemoryCache();

// Opcional: Configuración de CORS si se requiere acceso desde un dominio diferente
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Opcional: Configuración de Swagger si estás exponiendo una API


var app = builder.Build();

// Usar Swagger en desarrollo (opcional)


// Configuración de middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();

// Usar CORS si es necesario
app.UseCors("AllowAll");

// Usar el enrutamiento de controladores
app.UseRouting();

// Configurar autorización si es necesario (por ejemplo, si tienes autenticación)
app.UseAuthorization();

// Habilitar sesión
app.UseSession();

// Definir la ruta predeterminada para los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicación
app.Run();


