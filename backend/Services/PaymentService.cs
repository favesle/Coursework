using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;
using HotelManagement.API.Repositories;

namespace HotelManagement.API.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IMapper _mapper;
    public PaymentService(IPaymentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync() =>
        _mapper.Map<IEnumerable<PaymentDto>>(await _repository.GetAllAsync());

    public async Task<PaymentDto?> GetPaymentByIdAsync(int id) =>
        _mapper.Map<PaymentDto?>(await _repository.GetByIdAsync(id));

    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var payment = _mapper.Map<Payment>(dto);
        payment.PaymentDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(payment);
        return _mapper.Map<PaymentDto>(created);
    }

    public async Task DeletePaymentAsync(int id) => await _repository.DeleteAsync(id);
}