public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BusRouteId { get; set; }
    public string SeatNumber { get; set; }
    public bool PaymentConfirmed { get; set; }

    public User User { get; set; }
    public BusRoute BusRoute { get; set; }
}
