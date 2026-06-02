using HotelManagement.API.Data;
using HotelManagement.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;
    public ReportService(ApplicationDbContext context) => _context = context;

    public async Task<DashboardReportDto> GetDashboardReportAsync()
    {
        var rooms = await _context.Rooms.ToListAsync();
        var bookings = await _context.Bookings.Where(b => b.Status == "Active" && b.CheckIn <= DateTime.UtcNow && b.CheckOut >= DateTime.UtcNow).ToListAsync();
        var totalRevenue = await _context.Payments.SumAsync(p => p.Amount);
        var totalClients = await _context.Clients.CountAsync();

        var monthlyRevenue = await _context.Payments
            .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
            .Select(g => new MonthlyRevenueDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Revenue = g.Sum(p => p.Amount)
            })
            .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
            .Take(6)
            .ToListAsync();

        return new DashboardReportDto
        {
            TotalRooms = rooms.Count,
            AvailableRooms = rooms.Count(r => r.Status == "Available"),
            OccupiedRooms = rooms.Count(r => r.Status == "Occupied"),
            CleaningRooms = rooms.Count(r => r.Status == "Cleaning"),
            MaintenanceRooms = rooms.Count(r => r.Status == "Maintenance"),
            ActiveBookings = bookings.Count,
            TotalClients = totalClients,
            TotalRevenue = totalRevenue,
            MonthlyRevenue = monthlyRevenue
        };
    }
}