using HotelManagement.API.Models;

namespace HotelManagement.API.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<Room> AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(int id);
    Task<bool> IsRoomNumberUniqueAsync(string roomNumber);
}