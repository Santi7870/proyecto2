using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
using System.Linq;

namespace proyecto2.Controllers
{
    public class AccountController : Controller
    {
        private readonly Proyecto2Context _context;

        public AccountController(Proyecto2Context context)
        {
            _context = context;
        }

        // Vista de registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Método POST de registro
        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        // Vista de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Método POST de inicio de sesión
        [HttpPost]
        public IActionResult Login(string email, string contraseña)
        {
            var usuario = _context.Usuarios
                                  .FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

            if (usuario != null)
            {
                // Redirige a la página de "HOLA MUNDO" si las credenciales son correctas
                return RedirectToAction("HolaMundo");
            }
            else
            {
                // Si las credenciales no coinciden, muestra un mensaje de error
                ModelState.AddModelError("", "Credenciales incorrectas. Intenta nuevamente.");
                return View();
            }
        }

        // Vista "HOLA MUNDO"
        public IActionResult HolaMundo()
        {
            return View();
        }
    }
}



