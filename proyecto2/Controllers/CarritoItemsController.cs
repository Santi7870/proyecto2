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

        // GET: CarritoItems (Vista de carrito de compras)
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

            TempData["Mensaje"] = "Producto agregado al carrito.";
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

        // Acción para realizar la compra de todos los productos del carrito
        [HttpPost]
        public IActionResult ComprarTodo(string usuario)
        {
            // Verificar si el usuario está autenticado
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account");
            }

            // Obtener todos los productos del carrito del usuario
            var carritoItems = _context.CarritoItems.Where(ci => ci.Usuario == usuario).ToList();

            if (!carritoItems.Any())
            {
                // Si el carrito está vacío, redirigir a la página del carrito
                return RedirectToAction("Index", "CarritoItems");
            }

            // Calcular el total de la compra (precio total)
            decimal precioTotal = carritoItems.Sum(ci => ci.Precio * ci.Cantidad);

            // Crear la compra
            var compra = new Compra
            {
                Usuario = usuario,
                PrecioTotal = precioTotal,
                Fecha = DateTime.Now
            };

            // Guardar la compra en la base de datos
            _context.Compras.Add(compra);
            _context.SaveChanges();

            // Asignar la CompraId a cada item del carrito
            foreach (var item in carritoItems)
            {
                item.CompraId = compra.Id;
            }

            // Guardar los cambios
            _context.SaveChanges();

            // Opcional: Limpiar el carrito después de la compra
            _context.CarritoItems.RemoveRange(carritoItems);
            _context.SaveChanges();

            // Redirigir a la página de confirmación
            return RedirectToAction("CompraExitosa");
        }



        // Acción para realizar una compra directa (sin agregar al carrito)
        [HttpGet]
        public IActionResult ComprarDirecto(string nombre, string color, string talla, decimal precio)
        {
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(usuarioNombre))
            {
                TempData["Mensaje"] = "Debes iniciar sesión para realizar la compra.";
                return RedirectToAction("Login", "Account");
            }

            // Crear el objeto de compra
            var compra = new Compra
            {
                Usuario = usuarioNombre,
                PrecioTotal = precio,
                Fecha = DateTime.Now
            };

            // Guardar la compra en la base de datos
            _context.Compras.Add(compra);
            _context.SaveChanges();

            TempData["Mensaje"] = "Compra realizada con éxito.";
            return RedirectToAction("HolaMundo", "Account"); // Redirigir a donde se desee, por ejemplo a la página principal.
        }

        // Verificar si el producto existe en el carrito
        private bool CarritoItemExists(int id)
        {
            return _context.CarritoItems.Any(e => e.Id == id);
        }
    }
}











