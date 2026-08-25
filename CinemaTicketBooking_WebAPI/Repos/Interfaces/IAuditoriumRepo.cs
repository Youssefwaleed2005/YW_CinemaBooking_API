using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Repos.Interfaces
{
    public interface IAuditoriumRepo
    {
        Task<IEnumerable<Auditorium>> GetAll();
        Task<Auditorium?> GetById(int id);
        Task Add(Auditorium auditorium);
        Task Update(Auditorium auditorium);
        Task Delete(Auditorium auditorium);
        Task<bool> HasShowTimes(int auditoriumId);
    }
}