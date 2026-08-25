using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            // ---- Entity → Response DTOs ----
            CreateMap<Booking, BookingDto>();
            CreateMap<Booking, BookingDetailsDto>();

            // ---- Supporting nested maps ----
            CreateMap<Customer, CustomerDto>();
            CreateMap<Auditorium, AuditoriumDto>();
            CreateMap<ShowTime, ShowTimeDetailsDto>();
        }
    }
}