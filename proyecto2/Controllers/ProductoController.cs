using Microsoft.AspNetCore.Mvc;

namespace tuProyecto.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Details(string id)
        {
            // Aquí puedes obtener el producto de la base de datos
            // por el id. Por ahora usamos un ejemplo.
            ViewData["Title"] = "Hoodie Clásico";

            return View();
        }
    }
}
