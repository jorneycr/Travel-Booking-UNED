public class Reserva
{
    public int Id { get; set; }
    public string UsuarioId { get; set; } // Clave externa del usuario
    public Usuario Usuario { get; set; } // Relación con el Usuario
    public RutaBus Ruta { get; set; }
    public Asiento AsientoSeleccionado { get; set; }
    public string EstadoPago { get; set; }
    public DateTime FechaReserva { get; set; }
}
