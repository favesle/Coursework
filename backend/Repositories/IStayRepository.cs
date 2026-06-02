using HotelManagement.API.Models;

namespace HotelManagement.API.Repositories;

public interface IStayRepository
{
    Task<IEnumerable<Stay>> GetAllAsync();
    Task<Stay?> GetByIdAsync(int id);
    Task<Stay> AddAsync(Stay stay);
    Task UpdateAsync(Stay stay);
    Task DeleteAsync(int id);
    Task AddServiceToStayAsync(int stayId, int serviceId, int quantity);
}
