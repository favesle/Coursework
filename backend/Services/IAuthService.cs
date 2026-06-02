using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IAuthService
{
    Task<AuthResponseDto> AuthenticateAsync(string username, string password);
}