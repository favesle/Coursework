using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<BookingDto?> GetBookingByIdAsync(int id);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
    Task CancelBookingAsync(int id);
    Task DeleteBookingAsync(int id);
}