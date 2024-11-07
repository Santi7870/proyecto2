using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
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

        // Acción para realizar la compra
        [HttpPost]
        public IActionResult ComprarTodo()
        {
            var carritoItems = _context.CarritoItems.ToList();

            if (carritoItems.Count == 0)
            {
                // Si no hay productos en el carrito, retornar a la vista con un mensaje de advertencia
                TempData["Mensaje"] = "Tu carrito está vacío. No puedes realizar la compra.";
                return RedirectToAction(nameof(Index));
            }

            // Crear una nueva compra
            var compra = new Compra
            {
                Usuario = "Usuario Ejemplo", // Asegúrate de obtener el nombre del usuario desde la sesión o autenticación
                FechaCompra = DateTime.Now,
                Total = carritoItems.Sum(x => x.Precio * x.Cantidad)
            };

            // Agregar los items del carrito a la compra
            compra.CarritoItems = carritoItems;

            // Guardar la compra en la base de datos
            _context.Compras.Add(compra);
            _context.SaveChanges();

            // Eliminar los items del carrito después de realizar la compra
            _context.CarritoItems.RemoveRange(carritoItems);
            _context.SaveChanges();

            // Mostrar el mensaje de compra exitosa
            TempData["Mensaje"] = "Compra realizada con éxito.";

            return RedirectToAction(nameof(Index));
        }

        private bool CarritoItemExists(int id)
        {
            return _context.CarritoItems.Any(e => e.Id == id);
        }
    }
}


