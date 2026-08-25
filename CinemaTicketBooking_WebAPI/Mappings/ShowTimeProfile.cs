using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Mapping
{
    public class ShowTimeProfile : Profile
    {
        public ShowTimeProfile()
        {
            CreateMap<CreateShowTimeDto, ShowTime>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}