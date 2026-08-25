using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Repos.Interfaces
{
    public interface IBookingRepo
    {
        Task<PagedResult<Booking>> GetAll(BookingFilter filter);

        Task<Booking?> GetById(int id);


        Task Add(Booking booking);

        Task Update(Booking booking);
    }
}