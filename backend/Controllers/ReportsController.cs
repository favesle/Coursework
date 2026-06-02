using HotelManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[Authorize(Roles = "Admin,Manager")]
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    public ReportsController(IReportService reportService) => _reportService = reportService;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardReport() => Ok(await _reportService.GetDashboardReportAsync());
}
