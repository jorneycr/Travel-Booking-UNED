using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ReservationController : Controller
{
    private readonly TravelContext _context;

    public ReservationController(TravelContext context)
    {
        _context = context;
    }

    public IActionResult Book(int routeId)
    {
        var route = _context.BusRoutes.Find(routeId);
        return View(route); // Mostrar la vista para seleccionar asientos
    }

    [HttpPost]
    public IActionResult ConfirmBooking(int routeId, string seatNumber)
    {
        var userId = GetCurrentUserId();  // Obtener el ID del usuario actual (puedes manejar esto con autenticación)
        var reservation = new Reservation
        {
            UserId = userId,
            BusRouteId = routeId,
            SeatNumber = seatNumber,
            PaymentConfirmed = false
        };

        _context.Reservations.Add(reservation);
        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { reservationId = reservation.Id });
    }

    public IActionResult Confirmation(int reservationId)
    {
        var reservation = _context.Reservations
                                  .Include(r => r.BusRoute)
                                  .FirstOrDefault(r => r.Id == reservationId);

        return View(reservation);  // Mostrar detalles de la reserva confirmada
    }

    private int GetCurrentUserId()
    {
        // Lógica para obtener el ID del usuario actual, posiblemente usando cookies o JWT
        return 1; // Ejemplo
    }
}
