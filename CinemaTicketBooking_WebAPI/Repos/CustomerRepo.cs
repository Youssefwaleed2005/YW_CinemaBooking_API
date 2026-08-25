using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking_WebAPI.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly AppDBContext _context;

        public CustomerRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasBookings(int customerId)
        {
            return await _context.Bookings.AnyAsync(b => b.CustomerId == customerId);
        }
    }
}