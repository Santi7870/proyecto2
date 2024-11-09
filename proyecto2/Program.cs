using Microsoft.EntityFrameworkCore;
using proyecto2.Models;
using Microsoft.Extensions.DependencyInjection;
using proyecto2.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyecto2Context") ?? throw new InvalidOperationException("Connection string 'proyecto2Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura el contexto de base de datos usando la cadena de conexión
builder.Services.AddDbContext<Proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyecto2Context")));

// Configura la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Asegura que la cookie de sesión solo sea accesible a través de HTTP
    options.Cookie.IsEssential = true; // Marca la cookie como esencial
});

// Agrega soporte para la memoria caché distribuida (necesario para la sesión)
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Agrega el middleware de sesión
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

