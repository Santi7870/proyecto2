using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
using proyecto2.Data;

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
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contraseña)
        {
            var usuario = _context.Usuarios
                                  .FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

                return RedirectToAction("HolaMundo");
            }
            else
            {
                ModelState.AddModelError("", "Credenciales incorrectas. Intenta nuevamente.");
                return View();
            }
        }

        public IActionResult HolaMundo()
        {
            return View();
        }
    }
}




