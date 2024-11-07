using Microsoft.EntityFrameworkCore;
using proyecto2.Models;
using Microsoft.Extensions.DependencyInjection;
using proyecto2.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyecto2Context") ?? throw new InvalidOperationException("Connection string 'proyecto2Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura el contexto de base de datos usando la cadena de conexi�n
builder.Services.AddDbContext<Proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyecto2Context")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
