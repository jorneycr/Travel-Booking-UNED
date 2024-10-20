using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class SearchController : Controller
{
    private readonly TravelContext _context;

    public SearchController(TravelContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SearchRoutes(string departure, string destination, DateTime date)
    {
        var routes = _context.BusRoutes
            .Where(r => r.Departure == departure && r.Destination == destination && r.DepartureTime.Date == date.Date)
            .ToList();
        
        return View("SearchResults", routes);  // Redirige a una vista de resultados de búsqueda
    }
}
