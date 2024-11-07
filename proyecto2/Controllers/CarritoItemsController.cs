using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
using Microsoft.EntityFrameworkCore;  // Necesario para ToListAsync()

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
            // Devuelve todos los productos del carrito en la base de datos
            return View(await _context.CarritoItems.ToListAsync());
        }

        // Acción para agregar un producto al carrito
        [HttpGet]
        public IActionResult AgregarAlCarrito(string nombre, string color, string talla, decimal precio)
        {
            var carritoItem = new CarritoItem
            {
                Nombre = nombre,
                Color = color,
                Talla = talla,
                Precio = precio,
                Cantidad = 1  // Por defecto se agrega una unidad
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

        // Acción para comprar todos los productos del carrito
        public IActionResult ComprarTodo()
        {
            // Obtén todos los productos del carrito
            var carritoItems = _context.CarritoItems.ToList();

            // Aquí se puede agregar lógica para procesar el pago o redirigir al usuario a la página de pago
            if (carritoItems.Any())
            {
                // Ejemplo: Redirigir a una página de pago (puedes personalizar esta lógica)
                return RedirectToAction("Pago", "Pago", new { items = carritoItems });
            }

            // Si el carrito está vacío, redirigir a una página opcional
            return RedirectToAction("Index", "Home");
        }

        private bool CarritoItemExists(int id)
        {
            return _context.CarritoItems.Any(e => e.Id == id);
        }
    }
}
