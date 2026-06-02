namespace HotelManagement.API.DTOs;

public class StayDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalRoomCost { get; set; }
    public List<StayServiceDto> Services { get; set; } = new();
}

public class StayServiceDto
{
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total => Price * Quantity;
}

public class CreateStayDto
{
    public int ClientId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<StayServiceInputDto>? Services { get; set; }
}

public class StayServiceInputDto
{
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
}