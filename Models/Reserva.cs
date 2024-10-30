public class Reserva
{
    public int Id { get; set; }
    public Usuario Usuario { get; set; } // Relación con el Usuario
    public RutaBus Ruta { get; set; } // Ruta relacionada
    public Asiento AsientoSeleccionado { get; set; } // Asiento específico seleccionado
    public string EstadoPago { get; set; } // Ejemplo: "Pendiente", "Confirmado", "Cancelado"
    public DateTime FechaReserva { get; set; }
}
