using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories;

namespace HotelManagement.API.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;
    public ClientService(IClientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClientDto>> GetAllClientsAsync() =>
        _mapper.Map<IEnumerable<ClientDto>>(await _repository.GetAllAsync());

    public async Task<ClientDto?> GetClientByIdAsync(int id) =>
        _mapper.Map<ClientDto?>(await _repository.GetByIdAsync(id));

    public async Task<ClientDto> CreateClientAsync(CreateClientDto dto)
    {
        if (await _repository.ExistsByEmailAsync(dto.Email))
            throw new Exception("Client with this email already exists");
        var client = _mapper.Map<Client>(dto);
        var created = await _repository.AddAsync(client);
        return _mapper.Map<ClientDto>(created);
    }

    public async Task UpdateClientAsync(int id, CreateClientDto dto)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client == null) throw new Exception("Client not found");
        _mapper.Map(dto, client);
        await _repository.UpdateAsync(client);
    }

    public async Task DeleteClientAsync(int id) => await _repository.DeleteAsync(id);
}