using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IReportService
{
    Task<DashboardReportDto> GetDashboardReportAsync();
}