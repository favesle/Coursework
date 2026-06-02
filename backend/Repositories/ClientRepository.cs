using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;
    public ClientRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.ToListAsync();
    public async Task<Client?> GetByIdAsync(int id) => await _context.Clients.FindAsync(id);
    public async Task<Client> AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }
    public async Task UpdateAsync(Client client)
    {
        _context.Entry(client).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var client = await GetByIdAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<bool> ExistsByEmailAsync(string email) => await _context.Clients.AnyAsync(c => c.Email == email);
}