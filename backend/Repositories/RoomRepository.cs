using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;
    public RoomRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Room>> GetAllAsync() => await _context.Rooms.ToListAsync();
    public async Task<Room?> GetByIdAsync(int id) => await _context.Rooms.FindAsync(id);
    public async Task<Room> AddAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }
    public async Task UpdateAsync(Room room)
    {
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var room = await GetByIdAsync(id);
        if (room != null) { _context.Rooms.Remove(room); await _context.SaveChangesAsync(); }
    }
    public async Task<bool> IsRoomNumberUniqueAsync(string roomNumber) =>
        !await _context.Rooms.AnyAsync(r => r.RoomNumber == roomNumber);
}