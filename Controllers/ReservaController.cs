using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReservaController : Controller
{
    private readonly TravelContext _context;
    private readonly UserManager<Usuario> _userManager;

    public ReservaController(TravelContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private async Task<Usuario> ObtenerUsuarioActual()
    {
        var userId = _userManager.GetUserId(User);
        return await _userManager.Users.Include(u => u.HistorialReservas).FirstOrDefaultAsync(u => u.Id == userId);
    }

    [HttpGet]
    public IActionResult Detalles(int rutaId)
    {
        var ruta = _context.RutasBuses.Include(r => r.Asientos).FirstOrDefault(r => r.Id == rutaId);
        if (ruta == null) return NotFound();

        return View(ruta);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmarReservaAsync(int rutaId, int asientoId)
    {
        var usuarioActual = await ObtenerUsuarioActual();
        if (usuarioActual == null)
            return Unauthorized();

        var ruta = await _context.RutasBuses.FindAsync(rutaId);
        var asiento = await _context.Asientos.FindAsync(asientoId);

        if (ruta == null || asiento == null || !asiento.Disponible)
            return BadRequest("Asiento no disponible.");

        var reserva = new Reserva
        {
            Usuario = usuarioActual,
            Ruta = ruta,
            AsientoSeleccionado = asiento,
            EstadoPago = "Pendiente",
            FechaReserva = DateTime.Now
        };

        _context.Reservas.Add(reserva);
        asiento.Disponible = false;
        await _context.SaveChangesAsync();

        return RedirectToAction("Confirmacion", new { reservaId = reserva.Id });
    }

    [HttpGet]
    public IActionResult Confirmacion(int reservaId)
    {
        var reserva = _context.Reservas
            .Include(r => r.Ruta)
            .Include(r => r.AsientoSeleccionado)
            .FirstOrDefault(r => r.Id == reservaId);

        if (reserva == null) return NotFound();

        return View(reserva);
    }

    [HttpPost]
    public async Task<IActionResult> CancelarReserva(int reservaId)
    {
        var reserva = await _context.Reservas
            .Include(r => r.AsientoSeleccionado)
            .FirstOrDefaultAsync(r => r.Id == reservaId);

        if (reserva == null || reserva.EstadoPago != "Pagado")
            return BadRequest("Reserva no cancelable.");

        reserva.EstadoPago = "Cancelado";
        reserva.AsientoSeleccionado.Disponible = true;
        await _context.SaveChangesAsync();

        return RedirectToAction("Historial", "Usuario");
    }
}
