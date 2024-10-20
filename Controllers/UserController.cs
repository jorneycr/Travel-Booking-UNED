using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class UserController : Controller
{
    private readonly TravelContext _context;

    public UserController(TravelContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(user);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            // Autenticar al usuario (puedes usar cookies o JWT)
            return RedirectToAction("Profile");
        }
        ModelState.AddModelError("", "Correo o contraseña incorrectos");
        return View();
    }

    public IActionResult Profile()
    {
        // Mostrar el perfil del usuario actual
        return View();
    }

    public IActionResult Logout()
    {
        // Cerrar sesión (limpiar cookies o tokens)
        return RedirectToAction("Login");
    }
}
