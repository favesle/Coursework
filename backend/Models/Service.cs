namespace HotelManagement.API.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public ICollection<StayService> StayServices { get; set; } = new List<StayService>();
}