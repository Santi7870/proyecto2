using Microsoft.AspNetCore.Mvc;
using proyecto2.Models;
using System.Collections.Generic;
using System.Linq;

public class AccountController : Controller
{
    // Lista en memoria de usuarios simulando una base de datos
    private static List<Usuario> usuarios = new List<Usuario>();

    // Vista de Registro
    public IActionResult Register()
    {
        return View();
    }

    // Acción de Registro
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            // Simulando la verificación de si el correo ya está registrado
            if (usuarios.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                return View(usuario);
            }

            // Agregar el nuevo usuario a la lista en memoria
            usuarios.Add(usuario);

            // Redirigir a la página de login
            return RedirectToAction("Login");
        }

        return View(usuario);
    }

    // Vista de Login
    public IActionResult Login()
    {
        return View();
    }

    // Acción de Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string email, string contraseña)
    {
        var usuario = usuarios.FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

        if (usuario != null)
        {
            // Aquí puedes almacenar el usuario en la sesión si deseas
            return RedirectToAction("Dashboard"); // Página a la que se redirige después de iniciar sesión
        }

        ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
        return View();
    }
}

