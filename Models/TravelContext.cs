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

    public static void SeedData(TravelContext context)
{
    // Asegúrate de que no hay datos en la tabla antes de agregar los nuevos
    if (!context.RutasBuses.Any())
    {
        context.RutasBuses.AddRange(
            new RutaBus
            {
                Origen = "Ciudad A",
                Destino = "Ciudad B",
                Fecha = DateTime.Now.AddDays(1),
                HoraSalida = new TimeSpan(8, 0, 0),
                HoraLlegada = new TimeSpan(12, 30, 0),
                Precio = 50.00m,
                BusInfo = "Autobús Expreso",
                Asientos = new List<Asiento>
                {
                    new Asiento { Numero = "1A", Disponible = true },
                    new Asiento { Numero = "1B", Disponible = true },
                    new Asiento { Numero = "2A", Disponible = true },
                    new Asiento { Numero = "2B", Disponible = true }
                }
            },
            new RutaBus
            {
                Origen = "Ciudad B",
                Destino = "Ciudad C",
                Fecha = DateTime.Now.AddDays(2),
                HoraSalida = new TimeSpan(14, 0, 0),
                HoraLlegada = new TimeSpan(18, 0, 0),
                Precio = 40.00m,
                BusInfo = "Autobús Ejecutivo",
                Asientos = new List<Asiento>
                {
                    new Asiento { Numero = "1A", Disponible = true },
                    new Asiento { Numero = "1B", Disponible = true },
                    new Asiento { Numero = "2A", Disponible = true },
                    new Asiento { Numero = "2B", Disponible = true }
                }
            },
            new RutaBus
            {
                Origen = "Ciudad C",
                Destino = "Ciudad A",
                Fecha = DateTime.Now.AddDays(3),
                HoraSalida = new TimeSpan(10, 30, 0),
                HoraLlegada = new TimeSpan(15, 0, 0),
                Precio = 60.00m,
                BusInfo = "Autobús Clásico",
                Asientos = new List<Asiento>
                {
                    new Asiento { Numero = "1A", Disponible = true },
                    new Asiento { Numero = "1B", Disponible = true },
                    new Asiento { Numero = "2A", Disponible = true },
                    new Asiento { Numero = "2B", Disponible = true }
                }
            }
        );

        context.SaveChanges(); // Guardar los cambios en la base de datos
    }

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new Usuario { UserName = "juan@example.com", Email = "juan@example.com", Nombre = "Juan", Apellido = "Pérez" },
            new Usuario { UserName = "ana@example.com", Email = "ana@example.com", Nombre = "Ana", Apellido = "Gómez" }
        );

        context.SaveChanges(); // Guardar los cambios en la base de datos
    }

    if (!context.Reservas.Any())
    {
        context.Reservas.AddRange(
            new Reserva
            {
                Usuario = context.Users.FirstOrDefault(u => u.Email == "jorney@hotmail.com"),
                Ruta = context.RutasBuses.FirstOrDefault(r => r.Origen == "Ciudad A" && r.Destino == "Ciudad B"),
                AsientoSeleccionado = context.Asientos.FirstOrDefault(a => a.Numero == "1A"),
                EstadoPago = "Confirmado",
                FechaReserva = DateTime.Now
            },
            new Reserva
            {
                Usuario = context.Users.FirstOrDefault(u => u.Email == "jorney@hotmail.com"),
                Ruta = context.RutasBuses.FirstOrDefault(r => r.Origen == "Ciudad B" && r.Destino == "Ciudad C"),
                AsientoSeleccionado = context.Asientos.FirstOrDefault(a => a.Numero == "1B"),
                EstadoPago = "Pendiente",
                FechaReserva = DateTime.Now
            },
            new Reserva
            {
                Usuario = context.Users.FirstOrDefault(u => u.Email == "jorney@hotmail.com"),
                Ruta = context.RutasBuses.FirstOrDefault(r => r.Origen == "Ciudad C" && r.Destino == "Ciudad A"),
                AsientoSeleccionado = context.Asientos.FirstOrDefault(a => a.Numero == "2A"),
                EstadoPago = "Confirmado",
                FechaReserva = DateTime.Now
            }
        );

        context.SaveChanges(); // Guardar los cambios en la base de datos
    }
}

}

