using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking_WebAPI.Repos
{
    public class ShowTimeRepo : IShowTimeRepo
    {
        private readonly AppDBContext _context;

        public ShowTimeRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowTime>> GetAll()
        {
            return await _context.ShowTimes
                .Include(st => st.Movie)
                .Include(st => st.Auditorium)
                .OrderBy(st => st.ShowTimeValue)
                .ToListAsync();
        }

        public async Task<ShowTime?> GetById(int id)
        {
            return await _context.ShowTimes
                .Include(st => st.Movie)
                .Include(st => st.Auditorium)
                .FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task<IEnumerable<ShowTime>> GetByAuditorium(int auditoriumId, DateTime? date)
        {
            var query = _context.ShowTimes
                .Include(st => st.Movie)
                .Include(st => st.Auditorium)
                .Where(st => st.AuditoriumId == auditoriumId);

            if (date.HasValue)
                query = query.Where(st => st.ShowTimeValue.Date == date.Value.Date);

            return await query.OrderBy(st => st.ShowTimeValue).ToListAsync();
        }

        public async Task Add(ShowTime showTime)
        {
            await _context.ShowTimes.AddAsync(showTime);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ShowTime showTime)
        {
            _context.ShowTimes.Update(showTime);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ShowTime showTime)
        {
            _context.ShowTimes.Remove(showTime);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasBookings(int showTimeId)
        {
            return await _context.Bookings.AnyAsync(b => b.ShowTimeId == showTimeId);
        }
    }
}