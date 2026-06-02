using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories;

namespace HotelManagement.API.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public BookingService(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync() =>
        _mapper.Map<IEnumerable<BookingDto>>(await _repository.GetAllAsync());

    public async Task<BookingDto?> GetBookingByIdAsync(int id) =>
        _mapper.Map<BookingDto?>(await _repository.GetByIdAsync(id));

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
    {
        if (dto.CheckIn >= dto.CheckOut)
            throw new Exception("Check-in must be before check-out");
        if (!await _repository.IsRoomAvailableAsync(dto.RoomId, dto.CheckIn, dto.CheckOut))
            throw new Exception("Room is not available for selected dates");
        var booking = _mapper.Map<Booking>(dto);
        booking.Status = "Active";
        var created = await _repository.AddAsync(booking);
        return _mapper.Map<BookingDto>(created);
    }

    public async Task CancelBookingAsync(int id)
    {
        var booking = await _repository.GetByIdAsync(id);
        if (booking == null) throw new Exception("Booking not found");
        booking.Status = "Cancelled";
        await _repository.UpdateAsync(booking);
    }

    public async Task DeleteBookingAsync(int id) => await _repository.DeleteAsync(id);
}