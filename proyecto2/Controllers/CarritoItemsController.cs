using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
using proyecto2.Data;
using Microsoft.EntityFrameworkCore;

namespace proyecto2.Controllers
{
    public class CarritoItemsController : Controller
    {
        private readonly Proyecto2Context _context;

        public CarritoItemsController(Proyecto2Context context)
        {
            _context = context;
        }

        // GET: CarritoItems
        public async Task<IActionResult> Index()
        {
            var carritoItems = await _context.CarritoItems.ToListAsync();
            return View(carritoItems);
        }

        // Acción para agregar un producto al carrito
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

            return RedirectToAction(nameof(Index));
        }

        // Acción para eliminar un producto del carrito
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

        // Acción para realizar la compra
        [HttpPost]
        public IActionResult ComprarTodo()
        {
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(usuarioNombre))
            {
                TempData["Mensaje"] = "Debes iniciar sesión para realizar una compra.";
                return RedirectToAction("Login", "Account");
            }

            var carritoItems = _context.CarritoItems
                                       .Where(x => x.Usuario == usuarioNombre)
                                       .ToList();

            if (carritoItems.Count == 0)
            {
                TempData["Mensaje"] = "Tu carrito está vacío. No puedes realizar la compra.";
                return RedirectToAction(nameof(Index));
            }



            TempData["Mensaje"] = "Compra realizada con éxito.";

            return RedirectToAction(nameof(Index));
        }

        private bool CarritoItemExists(int id)
        {
            return _context.CarritoItems.Any(e => e.Id == id);
        }
    }
}










