namespace HotelManagement.API.Models;

public class Payment
{
    public int Id { get; set; }
    public int StayId { get; set; }
    public Stay Stay { get; set; } = null!;
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // Cash, Card, Bank
    public DateTime PaymentDate { get; set; }
}