using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
using proyecto2.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace proyecto2.Controllers
{
    public class CarritoItemsController : Controller
    {
        private readonly Proyecto2Context _context;

        public CarritoItemsController(Proyecto2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
            if (string.IsNullOrEmpty(usuarioNombre))
            {
                TempData["Mensaje"] = "Debes iniciar sesión para ver el carrito.";
                return RedirectToAction("Login", "Account");
            }

            var carritoItems = await _context.CarritoItems
                .Where(c => c.Usuario == usuarioNombre)
                .ToListAsync();

            return View(carritoItems);
        }

        [HttpGet]
        public IActionResult AgregarAlCarrito(string nombre, string color, string talla, decimal precio)
        {
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(usuarioNombre))
            {
                TempData["Mensaje"] = "Debes iniciar sesión para agregar productos al carrito.";
                return RedirectToAction("Login", "Account");
            }

            var carritoItem = new CarritoItem
            {
                Nombre = nombre,
                Color = color,
                Talla = talla,
                Precio = precio,
                Cantidad = 1,
                Usuario = usuarioNombre
            };

            _context.CarritoItems.Add(carritoItem);
            _context.SaveChanges();

            TempData["Mensaje"] = "Producto agregado al carrito.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EliminarDelCarrito(int id)
        {
            var item = _context.CarritoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.CarritoItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ComprarTodo(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("HolaMundo", "Account");
            }

            var carritoItems = _context.CarritoItems.Where(ci => ci.Usuario == usuario).ToList();

            if (!carritoItems.Any())
            {
                return RedirectToAction("Index", "CarritoItems");
            }

            decimal precioTotal = carritoItems.Sum(ci => ci.Precio * ci.Cantidad);

            var compra = new Compra
            {
                Usuario = usuario,
                PrecioTotal = precioTotal,
                Fecha = DateTime.Now
            };

            _context.Compras.Add(compra);
            _context.SaveChanges();

            foreach (var item in carritoItems)
            {
                item.CompraId = compra.Id;
            }

            _context.SaveChanges();

            _context.CarritoItems.RemoveRange(carritoItems);
            _context.SaveChanges();

            return RedirectToAction("CompraExitosa");
        }



        [HttpGet]
        public IActionResult ComprarDirecto(string nombre, string color, string talla, decimal precio)
        {
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(usuarioNombre))
            {
                TempData["Mensaje"] = "Debes iniciar sesión para realizar la compra.";
                return RedirectToAction("Login", "Account");
            }

            var compra = new Compra
            {
                Usuario = usuarioNombre,
                NombreProducto = nombre,
                PrecioTotal = precio,
                Fecha = DateTime.Now
            };

            _context.Compras.Add(compra);
            _context.SaveChanges();

            TempData["Mensaje"] = "Compra realizada con éxito.";
            return RedirectToAction("HolaMundo", "Account"); 
        }

        private bool CarritoItemExists(int id)
        {
            return _context.CarritoItems.Any(e => e.Id == id);
        }
    }
}











