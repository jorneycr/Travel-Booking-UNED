using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PagoController : Controller
{
    private readonly TravelContext _context;
    private readonly UserManager<Usuario> _userManager;

    public PagoController(TravelContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public IActionResult ProcesarPago(int rutaId, string asientosSeleccionados)
    {
        // Mantén los asientos seleccionados como string sin convertir a int
        var asientos = asientosSeleccionados.Split(',').ToList();

        var ruta = _context.RutasBuses.Find(rutaId);
        if (ruta == null) return NotFound();

        ViewBag.RutaId = rutaId;
        ViewBag.AsientosSeleccionados = asientos;

        // Redirige a la vista de pago con la información necesaria
        return View("ProcesarPago");
    }

    [HttpPost]
    public IActionResult ConfirmarPago(int rutaId, string asientosSeleccionados, string cardNumber, string expiryDate, string cvv)
    {
        // Simulación de pago
        if (cardNumber.Length == 16 && expiryDate.Length == 5 && cvv.Length == 3)
        {
            // Marcar asientos como reservados en la base de datos
            var asientos = asientosSeleccionados.Split(',').ToList();
            var ruta = _context.RutasBuses.Include(r => r.Asientos).FirstOrDefault(r => r.Id == rutaId);

            if (ruta != null)
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    TempData["Error"] = "Debe iniciar sesión para hacer una reserva.";
                    return RedirectToAction("Login", "Usuario");
                }
                // Cambia la lógica de comparación para trabajar con strings
                foreach (var asiento in ruta.Asientos.Where(a => asientos.Contains(a.Numero)))
                {
                    asiento.Disponible = false;
                    var reserva = new Reserva
                    {
                        UsuarioId = userId,
                        AsientoSeleccionadoId = asiento.Id,
                        RutaId = rutaId,
                        EstadoPago = "Pendiente",
                        FechaReserva = DateTime.Now
                    };
                    _context.Reservas.Add(reserva);
                }

                _context.SaveChanges();
                TempData["Success"] = "Pago realizado y asientos reservados exitosamente.";
                return RedirectToAction("Historial", "Usuario");
            }
        }

        TempData["Error"] = "Error en el procesamiento del pago. Inténtelo nuevamente.";
        return RedirectToAction("Detalles", new { rutaId });
    }
}
