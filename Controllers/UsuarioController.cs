using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UsuarioController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly TravelContext _context;

    public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TravelContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult Registro() => View();

    [HttpPost]
    public async Task<IActionResult> Registro(RegistroViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre,
                Apellido = model.Apellido
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Inicio de sesión no válido");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Historial()
    {
        var userId = _userManager.GetUserId(User); // Obtén el ID del usuario autenticado
        var reservas = await _context.Reservas
            .Where(r => r.Usuario.Id == userId)
            .Include(r => r.Ruta)
            .Include(r => r.AsientoSeleccionado)
            .ToListAsync();

        return View(reservas);
    }
}
