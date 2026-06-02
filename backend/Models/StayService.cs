namespace HotelManagement.API.Models;

public class StayService
{
    public int StayId { get; set; }
    public Stay Stay { get; set; } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public int Quantity { get; set; }
}