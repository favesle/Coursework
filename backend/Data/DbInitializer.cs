using HotelManagement.API.Models;

namespace HotelManagement.API.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any()) return;

        // Пользователи
        var admin = new User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        };
        var manager = new User
        {
            Username = "manager",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"),
            Role = "Manager",
            CreatedAt = DateTime.UtcNow
        };
        var staff = new User
        {
            Username = "staff",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("staff123"),
            Role = "Staff",
            CreatedAt = DateTime.UtcNow
        };
        context.Users.AddRange(admin, manager, staff);

        // Номера
        var rooms = new[]
        {
            new Room { RoomNumber = "101", Type = "Single", Capacity = 1, Price = 50, Status = "Available" },
            new Room { RoomNumber = "102", Type = "Double", Capacity = 2, Price = 80, Status = "Available" },
            new Room { RoomNumber = "103", Type = "Suite", Capacity = 4, Price = 150, Status = "Available" },
            new Room { RoomNumber = "104", Type = "Double", Capacity = 2, Price = 80, Status = "Available" }
        };
        context.Rooms.AddRange(rooms);

        // Услуги
        var services = new[]
        {
            new Service { Name = "Завтрак", Price = 10, Description = "Шведский стол" },
            new Service { Name = "Парковка", Price = 15, Description = "Охраняемая парковка" },
            new Service { Name = "Спа", Price = 30, Description = "1 час" },
            new Service { Name = "Трансфер", Price = 25, Description = "Аэропорт-отель" }
        };
        context.Services.AddRange(services);

        // Пример клиента
        var client = new Client
        {
            FullName = "Иван Петров",
            Passport = "1234 567890",
            Phone = "+7 999 123-45-67",
            Email = "ivan@example.com"
        };
        context.Clients.Add(client);

        context.SaveChanges();
    }
}