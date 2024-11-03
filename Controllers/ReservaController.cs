using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class ReservaController : Controller
{
    private readonly TravelContext _context;
    private readonly UserManager<Usuario> _userManager;

    public ReservaController(TravelContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Confirmar(int rutaId, List<string> asientosSeleccionados)
    {
        if (asientosSeleccionados == null || !asientosSeleccionados.Any())
        {
            TempData["Error"] = "Debe seleccionar al menos un asiento.";
            return RedirectToAction("Detalles", "Busqueda", new { rutaId = rutaId });
        }

        var userId = _userManager.GetUserId(User); // Obtener el ID del usuario autenticado
        if (userId == null)
        {
            TempData["Error"] = "Debe iniciar sesión para hacer una reserva.";
            return RedirectToAction("Login", "Usuario");
        }

        foreach (var numeroAsiento in asientosSeleccionados)
        {
            var asiento = await _context.Asientos.FirstOrDefaultAsync(a => a.Numero == numeroAsiento && a.Id == rutaId);

            if (asiento != null && asiento.Disponible)
            {
                asiento.Disponible = false; // Marca el asiento como reservado

                var reserva = new Reserva
                {
                    UsuarioId = userId, // Asigna el ID del usuario
                    AsientoSeleccionado = asiento,
                    Ruta = await _context.RutasBuses.FindAsync(rutaId),
                    EstadoPago = "Pendiente",
                    FechaReserva = DateTime.Now
                };

                _context.Reservas.Add(reserva);
            }
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Reserva confirmada con éxito.";
        return RedirectToAction("Index", "Home");
    }
}
