using CinemaTicketBooking_WebAPI.DTOs;

namespace CinemaTicketBooking_WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAll();
        Task<CustomerDto> GetById(int id);
        Task<CustomerDto> Create(CreateCustomerDto dto);
        Task<CustomerDto> Update(int id, UpdateCustomerDto dto);
        Task Delete(int id);
    }
}