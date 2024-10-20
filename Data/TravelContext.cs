using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class TravelContext : DbContext
{
    public DbSet<BusRoute> BusRoutes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public TravelContext(DbContextOptions<TravelContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de la propiedad Price en BusRoute
        modelBuilder.Entity<BusRoute>()
            .Property(b => b.Price)
            .HasPrecision(18, 2); // Define precisión y escala para evitar truncamiento

        // Configuración de las relaciones
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.BusRoute)
            .WithMany(b => b.Reservations)
            .HasForeignKey(r => r.BusRouteId);

        base.OnModelCreating(modelBuilder);
    }

    public static void SeedData(TravelContext context)
    {
        // Asegúrate de que no hay datos en la tabla antes de agregar los nuevos
        if (!context.BusRoutes.Any())
        {
            context.BusRoutes.AddRange(
                new BusRoute { BusName = "Autobús Expreso 1", Departure = "Ciudad A", Destination = "Ciudad B", DepartureTime = DateTime.Now.AddDays(1), Price = 50.00m },
                new BusRoute { BusName = "Autobús Expreso 2", Departure = "Ciudad B", Destination = "Ciudad C", DepartureTime = DateTime.Now.AddDays(2), Price = 40.00m },
                new BusRoute { BusName = "Autobús Expreso 3", Departure = "Ciudad C", Destination = "Ciudad A", DepartureTime = DateTime.Now.AddDays(3), Price = 60.00m }
            );

            context.SaveChanges(); // Guardar los cambios en la base de datos
        }

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { FullName = "Juan Pérez", Email = "juan@example.com", Password = "password123", Reservations = new List<Reservation>() },
                new User { FullName = "Ana Gómez", Email = "ana@example.com", Password = "password456", Reservations = new List<Reservation>() }
            );

            context.SaveChanges(); // Guardar los cambios en la base de datos
        }

        if (!context.Reservations.Any())
        {
            context.Reservations.AddRange(
                new Reservation { UserId = 1, BusRouteId = 1, SeatNumber = "1A", PaymentConfirmed = true },
                new Reservation { UserId = 1, BusRouteId = 2, SeatNumber = "1B", PaymentConfirmed = false },
                new Reservation { UserId = 2, BusRouteId = 3, SeatNumber = "2A", PaymentConfirmed = true }
            );

            context.SaveChanges(); // Guardar los cambios en la base de datos
        }
    }
}
