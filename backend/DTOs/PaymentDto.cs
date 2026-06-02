namespace HotelManagement.API.DTOs;

public class PaymentDto
{
    public int Id { get; set; }
    public int StayId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
}

public class CreatePaymentDto
{
    public int StayId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}