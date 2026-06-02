using HotelManagement.API.DTOs;
using HotelManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StaysController : ControllerBase
{
    private readonly IStayService _service;
    public StaysController(IStayService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllStaysAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var stay = await _service.GetStayByIdAsync(id);
        return stay == null ? NotFound() : Ok(stay);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateStayDto dto)
    {
        try
        {
            var result = await _service.CreateStayAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPatch("{id}/checkout")]
    public async Task<IActionResult> CheckOut(int id)
    {
        try
        {
            await _service.CheckOutStayAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteStayAsync(id);
        return NoContent();
    }
}
