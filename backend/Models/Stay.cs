namespace HotelManagement.API.Models;

public class Stay
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<StayService> StayServices { get; set; } = new List<StayService>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}