// Repos/Interfaces/IShowTimeRepo.cs
using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Repos.Interfaces
{
    public interface IShowTimeRepo
    {
        Task<IEnumerable<ShowTime>> GetAll();
        Task<ShowTime?> GetById(int id);
        Task<IEnumerable<ShowTime>> GetByAuditorium(int auditoriumId, DateTime? date);
        Task Add(ShowTime showTime);
        Task Update(ShowTime showTime);
        Task Delete(ShowTime showTime);
        Task<bool> HasBookings(int showTimeId);
    }
}