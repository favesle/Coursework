namespace HotelManagement.API.Models;

public class Client
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Passport { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Stay> Stays { get; set; } = new List<Stay>();
}