using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories;

namespace HotelManagement.API.Services;

public class StayService : IStayService
{
    private readonly IStayRepository _stayRepo;
    private readonly IRoomRepository _roomRepo;
    private readonly IMapper _mapper;
    public StayService(IStayRepository stayRepo, IRoomRepository roomRepo, IMapper mapper)
    {
        _stayRepo = stayRepo;
        _roomRepo = roomRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StayDto>> GetAllStaysAsync() =>
        _mapper.Map<IEnumerable<StayDto>>(await _stayRepo.GetAllAsync());

    public async Task<StayDto?> GetStayByIdAsync(int id) =>
        _mapper.Map<StayDto?>(await _stayRepo.GetByIdAsync(id));

    public async Task<StayDto> CreateStayAsync(CreateStayDto dto)
    {
        var room = await _roomRepo.GetByIdAsync(dto.RoomId);
        if (room == null) throw new Exception("Room not found");
        if (room.Status != "Available") throw new Exception("Room is not available for check-in");
        if (dto.StartDate >= dto.EndDate) throw new Exception("Invalid dates");

        var stay = _mapper.Map<Stay>(dto);
        var created = await _stayRepo.AddAsync(stay);

        // Добавляем услуги
        if (dto.Services != null)
        {
            foreach (var service in dto.Services)
                await _stayRepo.AddServiceToStayAsync(created.Id, service.ServiceId, service.Quantity);
        }

        // Меняем статус номера на Occupied
        room.Status = "Occupied";
        await _roomRepo.UpdateAsync(room);

        return _mapper.Map<StayDto>(created);
    }

    public async Task CheckOutStayAsync(int id)
    {
        var stay = await _stayRepo.GetByIdAsync(id);
        if (stay == null) throw new Exception("Stay not found");
        stay.EndDate = DateTime.UtcNow;
        await _stayRepo.UpdateAsync(stay);

        var room = await _roomRepo.GetByIdAsync(stay.RoomId);
        if (room != null)
        {
            room.Status = "Cleaning";
            await _roomRepo.UpdateAsync(room);
        }
    }

    public async Task DeleteStayAsync(int id) => await _stayRepo.DeleteAsync(id);
}