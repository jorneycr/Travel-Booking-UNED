public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // Consider encrypting passwords in a real application
    public ICollection<Reservation> Reservations { get; set; }
}
