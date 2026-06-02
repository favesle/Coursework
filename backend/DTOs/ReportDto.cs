namespace HotelManagement.API.DTOs;

public class DashboardReportDto
{
    public int TotalRooms { get; set; }
    public int AvailableRooms { get; set; }
    public int OccupiedRooms { get; set; }
    public int CleaningRooms { get; set; }
    public int MaintenanceRooms { get; set; }
    public int ActiveBookings { get; set; }
    public int TotalClients { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<MonthlyRevenueDto> MonthlyRevenue { get; set; } = new();
}

public class MonthlyRevenueDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Revenue { get; set; }
}