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
        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "El usuario se ha registrado correctamente.";
            return RedirectToAction("Login");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocurrió un error al registrar el usuario. Inténtalo de nuevo.";
            return View(user); // Retornar la misma vista para mostrar el error.
        }
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Por favor, ingrese ambos campos.");
            return View();
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            // Aquí podrías implementar la lógica de autenticación, como establecer cookies o JWT.
            return RedirectToAction("Profile");
        }

        TempData["ErrorMessage"] = "Correo o contraseña incorrectos.";
        return View();
    }

    public IActionResult Profile()
    {
        var email = User.Identity.Name;  // Obtener el correo del usuario autenticado

        if (email == null)
        {
            TempData["ErrorMessage"] = "No se ha iniciado sesión.";
            return RedirectToAction("Login");
        }

        var user = _context.Users
                    .Where(u => u.Email == email)
                    .Select(u => new User
                    {
                        FullName = u.FullName,
                        Email = u.Email,
                        Reservations = u.Reservations
                    })
                    .FirstOrDefault();

        if (user == null)
        {
            TempData["ErrorMessage"] = "No se encontró el perfil del usuario.";
            return RedirectToAction("Login");
        }

        return View(user);
    }

    public IActionResult Logout()
    {
        // Aquí podrías implementar la lógica para cerrar sesión, como limpiar cookies o tokens.
        return RedirectToAction("Login");
    }
}
