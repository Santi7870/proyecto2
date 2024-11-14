using Microsoft.EntityFrameworkCore;
using proyecto2.Models;
using Microsoft.Extensions.DependencyInjection;
using proyecto2.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyecto2Context") ?? throw new InvalidOperationException("Connection string 'proyecto2Context' not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Proyecto2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyecto2Context")));

// Configura la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

