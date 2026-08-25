using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Repos.Interfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetById(int id);
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Delete(Customer customer);
        Task<bool> HasBookings(int customerId);
    }
}