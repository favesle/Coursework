namespace HotelManagement.API.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = "Available"; // Available, Occupied, Cleaning, Maintenance
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Stay> Stays { get; set; } = new List<Stay>();
}