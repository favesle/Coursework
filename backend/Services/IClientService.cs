using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetAllClientsAsync();
    Task<ClientDto?> GetClientByIdAsync(int id);
    Task<ClientDto> CreateClientAsync(CreateClientDto dto);
    Task UpdateClientAsync(int id, CreateClientDto dto);
    Task DeleteClientAsync(int id);
}