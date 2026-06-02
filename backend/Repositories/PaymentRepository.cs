using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;
    public PaymentRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Payment>> GetAllAsync() => await _context.Payments.Include(p => p.Stay).ToListAsync();
    public async Task<Payment?> GetByIdAsync(int id) => await _context.Payments.FindAsync(id);
    public async Task<Payment> AddAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }
    public async Task UpdateAsync(Payment payment)
    {
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var payment = await GetByIdAsync(id);
        if (payment != null) { _context.Payments.Remove(payment); await _context.SaveChangesAsync(); }
    }
    public async Task<decimal> GetTotalRevenueAsync() => await _context.Payments.SumAsync(p => p.Amount);
}