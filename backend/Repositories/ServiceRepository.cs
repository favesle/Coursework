using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;
    public ServiceRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Service>> GetAllAsync() => await _context.Services.ToListAsync();
    public async Task<Service?> GetByIdAsync(int id) => await _context.Services.FindAsync(id);
    public async Task<Service> AddAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }
    public async Task UpdateAsync(Service service)
    {
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var service = await GetByIdAsync(id);
        if (service != null) { _context.Services.Remove(service); await _context.SaveChangesAsync(); }
    }
}
