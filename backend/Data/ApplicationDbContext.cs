using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Stay> Stays { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<StayService> StayServices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Составной ключ для StayService
        modelBuilder.Entity<StayService>()
            .HasKey(ss => new { ss.StayId, ss.ServiceId });

        // Уникальные индексы
        modelBuilder.Entity<Room>()
            .HasIndex(r => r.RoomNumber)
            .IsUnique();

        modelBuilder.Entity<Client>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Каскадные ограничения
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Client)
            .WithMany(c => c.Bookings)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
