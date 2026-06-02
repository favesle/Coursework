namespace HotelManagement.API.Models;

public class Log
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Action { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}