using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Services.Interfaces
{
    public interface IBookingService
    {
        Task<PagedResult<BookingDto>> GetAll(BookingFilter filter);

        Task<BookingDetailsDto> GetById(int id);

        Task<BookingDetailsDto> Create(CreateBookingDto dto);

        Task<BookingDetailsDto> Cancel(int id);

        Task<BookingDetailsDto> Confirm(int id);
    }
}