using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class TravelContext : IdentityDbContext<Usuario>
{
    public TravelContext(DbContextOptions<TravelContext> options) : base(options) { }

    // Define DbSet para cada modelo de la aplicación
    public DbSet<RutaBus> RutasBuses { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Asiento> Asientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación Usuario-Reserva (Uno a Muchos)
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.HistorialReservas)
            .IsRequired(); // Si la reserva siempre debe tener un usuario, puedes hacer que sea requerido.

        // Configuración de la relación Reserva-RutaBus (Uno a Muchos)
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Ruta)
            .WithMany()
            .IsRequired();

        // Configuración de la relación RutaBus-Asiento (Uno a Muchos)
        modelBuilder.Entity<Asiento>()
            .HasOne<RutaBus>()
            .WithMany(r => r.Asientos)
            .HasForeignKey("RutaBusId"); // Clave externa que representa la relación

        // Configuración de la relación Reserva-Asiento (Uno a Uno)
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.AsientoSeleccionado)
            .WithOne()
            .HasForeignKey<Reserva>("AsientoSeleccionadoId");

        // Configuración de precisión para la propiedad Precio de RutaBus
        modelBuilder.Entity<RutaBus>()
            .Property(r => r.Precio)
            .HasPrecision(18, 2);
    }
}

