using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories;

namespace HotelManagement.API.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;
    public RoomService(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync() =>
        _mapper.Map<IEnumerable<RoomDto>>(await _repository.GetAllAsync());

    public async Task<RoomDto?> GetRoomByIdAsync(int id) =>
        _mapper.Map<RoomDto?>(await _repository.GetByIdAsync(id));

    public async Task<RoomDto> CreateRoomAsync(CreateRoomDto dto)
    {
        if (!await _repository.IsRoomNumberUniqueAsync(dto.RoomNumber))
            throw new Exception("Room number already exists");
        var room = _mapper.Map<Room>(dto);
        room.Status = "Available";
        var created = await _repository.AddAsync(room);
        return _mapper.Map<RoomDto>(created);
    }

    public async Task UpdateRoomAsync(int id, CreateRoomDto dto)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) throw new Exception("Room not found");
        if (room.RoomNumber != dto.RoomNumber && !await _repository.IsRoomNumberUniqueAsync(dto.RoomNumber))
            throw new Exception("Room number already exists");
        _mapper.Map(dto, room);
        await _repository.UpdateAsync(room);
    }

    public async Task DeleteRoomAsync(int id) => await _repository.DeleteAsync(id);

    public async Task UpdateRoomStatusAsync(int id, string status)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) throw new Exception("Room not found");
        room.Status = status;
        await _repository.UpdateAsync(room);
    }
}