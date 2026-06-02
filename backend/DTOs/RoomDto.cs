namespace HotelManagement.API.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateRoomDto
{
    public string RoomNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal Price { get; set; }
}

public class UpdateRoomStatusDto
{
    public string Status { get; set; } = string.Empty;
}