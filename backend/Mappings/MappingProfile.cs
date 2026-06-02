using AutoMapper;
using HotelManagement.API.DTOs;
using HotelManagement.API.Models;

namespace HotelManagement.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<CreateClientDto, Client>();

        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<CreateRoomDto, Room>();

        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
            .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber));
        CreateMap<CreateBookingDto, Booking>();

        CreateMap<Stay, StayDto>()
            .ForMember(dest => dest.ClientFullName, opt => opt.MapFrom(src => src.Client.FullName))
            .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
            .ForMember(dest => dest.TotalRoomCost, opt => opt.Ignore())
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src =>
                src.StayServices.Select(ss => new StayServiceDto
                {
                    ServiceId = ss.ServiceId,
                    ServiceName = ss.Service.Name,
                    Price = ss.Service.Price,
                    Quantity = ss.Quantity
                })));
        CreateMap<CreateStayDto, Stay>();
        CreateMap<StayServiceInputDto, StayService>();

        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<CreateServiceDto, Service>();

        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<CreatePaymentDto, Payment>();
    }
}
