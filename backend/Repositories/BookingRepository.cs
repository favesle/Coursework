using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;
    public BookingRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await _context.Bookings.Include(b => b.Client).Include(b => b.Room).ToListAsync();
    public async Task<Booking?> GetByIdAsync(int id) =>
        await _context.Bookings.Include(b => b.Client).Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == id);
    public async Task<Booking> AddAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }
    public async Task UpdateAsync(Booking booking)
    {
        _context.Entry(booking).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var booking = await GetByIdAsync(id);
        if (booking != null) { _context.Bookings.Remove(booking); await _context.SaveChangesAsync(); }
    }
    public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut) =>
        !await _context.Bookings.AnyAsync(b => b.RoomId == roomId && b.Status == "Active" &&
            ((checkIn >= b.CheckIn && checkIn < b.CheckOut) ||
             (checkOut > b.CheckIn && checkOut <= b.CheckOut) ||
             (checkIn <= b.CheckIn && checkOut >= b.CheckOut)));
}
