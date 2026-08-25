using CinemaTicketBooking_WebAPI.DTOs;

namespace CinemaTicketBooking_WebAPI.Services.Interfaces
{
    public interface IAuditoriumService
    {
        Task<IEnumerable<AuditoriumDto>> GetAll();
        Task<AuditoriumDto> GetById(int id);
        Task<AuditoriumDto> Create(CreateAuditoriumDto dto);
        Task<AuditoriumDto> Update(int id, UpdateAuditoriumDto dto);
        Task Delete(int id);
    }
}