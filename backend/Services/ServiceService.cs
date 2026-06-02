using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories;

namespace HotelManagement.API.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _repository;
    private readonly IMapper _mapper;
    public ServiceService(IServiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync() =>
        _mapper.Map<IEnumerable<ServiceDto>>(await _repository.GetAllAsync());

    public async Task<ServiceDto?> GetServiceByIdAsync(int id) =>
        _mapper.Map<ServiceDto?>(await _repository.GetByIdAsync(id));

    public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto dto)
    {
        var service = _mapper.Map<Service>(dto);
        var created = await _repository.AddAsync(service);
        return _mapper.Map<ServiceDto>(created);
    }

    public async Task UpdateServiceAsync(int id, CreateServiceDto dto)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service == null) throw new Exception("Service not found");
        _mapper.Map(dto, service);
        await _repository.UpdateAsync(service);
    }

    public async Task DeleteServiceAsync(int id) => await _repository.DeleteAsync(id);
}