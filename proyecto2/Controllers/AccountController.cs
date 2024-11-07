using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;

namespace proyecto2.Controllers
{
    public class AccountController : Controller
    {
        private readonly Proyecto2Context _context;

        public AccountController(Proyecto2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges(); // Asegúrate de que esta línea esté presente para guardar en la base de datos
                return RedirectToAction("Login");
            }
            return View(usuario); // Si el modelo no es válido, vuelve a mostrar la vista de registro
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}


