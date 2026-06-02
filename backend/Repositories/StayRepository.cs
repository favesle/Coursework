using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class StayRepository : IStayRepository
{
    private readonly ApplicationDbContext _context;
    public StayRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Stay>> GetAllAsync() =>
        await _context.Stays.Include(s => s.Client).Include(s => s.Room).Include(s => s.StayServices).ThenInclude(ss => ss.Service).ToListAsync();
    public async Task<Stay?> GetByIdAsync(int id) =>
        await _context.Stays.Include(s => s.Client).Include(s => s.Room).Include(s => s.StayServices).ThenInclude(ss => ss.Service).FirstOrDefaultAsync(s => s.Id == id);
    public async Task<Stay> AddAsync(Stay stay)
    {
        _context.Stays.Add(stay);
        await _context.SaveChangesAsync();
        return stay;
    }
    public async Task UpdateAsync(Stay stay)
    {
        _context.Entry(stay).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var stay = await GetByIdAsync(id);
        if (stay != null) { _context.Stays.Remove(stay); await _context.SaveChangesAsync(); }
    }
    public async Task AddServiceToStayAsync(int stayId, int serviceId, int quantity)
    {
        var stayService = new StayService { StayId = stayId, ServiceId = serviceId, Quantity = quantity };
        _context.StayServices.Add(stayService);
        await _context.SaveChangesAsync();
    }
}
