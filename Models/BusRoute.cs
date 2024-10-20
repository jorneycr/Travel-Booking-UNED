public class BusRoute
{
    public int Id { get; set; }
    public string BusName { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public decimal Price { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}
