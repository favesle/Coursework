using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<RoomDto?> GetRoomByIdAsync(int id);
    Task<RoomDto> CreateRoomAsync(CreateRoomDto dto);
    Task UpdateRoomAsync(int id, CreateRoomDto dto);
    Task DeleteRoomAsync(int id);
    Task UpdateRoomStatusAsync(int id, string status);
}