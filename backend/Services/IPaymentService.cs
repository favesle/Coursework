using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentDto?> GetPaymentByIdAsync(int id);
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto);
    Task DeletePaymentAsync(int id);
}
