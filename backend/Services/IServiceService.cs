using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
    Task<ServiceDto?> GetServiceByIdAsync(int id);
    Task<ServiceDto> CreateServiceAsync(CreateServiceDto dto);
    Task UpdateServiceAsync(int id, CreateServiceDto dto);
    Task DeleteServiceAsync(int id);
}
