using CinemaTicketBooking_WebAPI.DTOs;

namespace CinemaTicketBooking_WebAPI.Services.Interfaces
{
    public interface IShowTimeService
    {
        Task<IEnumerable<ShowTimeDetailsDto>> GetAll();
        Task<ShowTimeDetailsDto> GetById(int id);
        Task<IEnumerable<ShowTimeDetailsDto>> GetByAuditorium(int auditoriumId, DateTime? date);
        Task<ShowTimeDetailsDto> Create(CreateShowTimeDto dto);
        Task<ShowTimeDetailsDto> Update(int id, UpdateShowTimeDto dto);
        Task Delete(int id);
    }
}