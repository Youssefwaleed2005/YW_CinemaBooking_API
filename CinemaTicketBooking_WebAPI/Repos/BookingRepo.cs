using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking_WebAPI.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private readonly AppDBContext _context;

        public BookingRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Booking>> GetAll(BookingFilter filter)
        {
            var query = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.ShowTime)
                    .ThenInclude(st => st.Movie)
                .Include(b => b.ShowTime)
                    .ThenInclude(st => st.Auditorium)
                .AsQueryable();

            if (filter.CustomerId.HasValue)
                query = query.Where(b => b.CustomerId == filter.CustomerId.Value);

            if (!string.IsNullOrWhiteSpace(filter.CustomerName))
                query = query.Where(b => b.Customer.Name.Contains(filter.CustomerName));

            if (filter.ShowTimeId.HasValue)
                query = query.Where(b => b.ShowTimeId == filter.ShowTimeId.Value);

            if (filter.Status.HasValue)
                query = query.Where(b => b.Status == filter.Status.Value);

            query = query.OrderByDescending(b => b.BookingDate);

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Booking>
            {
                Data = data,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Booking?> GetById(int id)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.ShowTime)
                    .ThenInclude(st => st.Movie)
                .Include(b => b.ShowTime)
                    .ThenInclude(st => st.Auditorium)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

       

        public async Task Add(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}