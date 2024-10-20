using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class PaymentController : Controller
{
    private readonly TravelContext _context;

    public PaymentController(TravelContext context)
    {
        _context = context;
    }

    public IActionResult Index(int reservationId)
    {
        var reservation = _context.Reservations
                                  .Include(r => r.BusRoute)
                                  .FirstOrDefault(r => r.Id == reservationId);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation); // Mostrar la página de pago
    }

    [HttpPost]
    public IActionResult ConfirmPayment(int reservationId)
    {
        var reservation = _context.Reservations.Find(reservationId);
        if (reservation == null)
        {
            return NotFound();
        }

        // Procesar el pago aquí (simulado)
        reservation.PaymentConfirmed = true;
        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { reservationId = reservationId });
    }

    public IActionResult Confirmation(int reservationId)
    {
        var reservation = _context.Reservations
                                  .Include(r => r.BusRoute)
                                  .FirstOrDefault(r => r.Id == reservationId);

        return View(reservation); // Mostrar la confirmación del pago y detalles
    }
}
