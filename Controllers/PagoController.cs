using Microsoft.AspNetCore.Mvc;

public class PagoController : Controller
{
    private readonly TravelContext _context;

    public PagoController(TravelContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult ProcesarPago(int reservaId)
    {
        var reserva = _context.Reservas.Find(reservaId);
        if (reserva == null) return NotFound();

        // Simulación de pago exitoso
        reserva.EstadoPago = "Pagado";
        _context.SaveChanges();

        return RedirectToAction("Confirmacion", "Reserva", new { reservaId });
    }
}
