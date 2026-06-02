using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IStayService
{
    Task<IEnumerable<StayDto>> GetAllStaysAsync();
    Task<StayDto?> GetStayByIdAsync(int id);
    Task<StayDto> CreateStayAsync(CreateStayDto dto);
    Task CheckOutStayAsync(int id);
    Task DeleteStayAsync(int id);
}