// Repos/AuditoriumRepo.cs
using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking_WebAPI.Repos
{
    public class AuditoriumRepo : IAuditoriumRepo
    {
        private readonly AppDBContext _context;

        public AuditoriumRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auditorium>> GetAll()
        {
            return await _context.Auditoriums.OrderBy(a => a.RoomNumber).ToListAsync();
        }

        public async Task<Auditorium?> GetById(int id)
        {
            return await _context.Auditoriums.FindAsync(id);
        }

        public async Task Add(Auditorium auditorium)
        {
            await _context.Auditoriums.AddAsync(auditorium);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Auditorium auditorium)
        {
            _context.Auditoriums.Update(auditorium);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Auditorium auditorium)
        {
            _context.Auditoriums.Remove(auditorium);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasShowTimes(int auditoriumId)
        {
            return await _context.ShowTimes.AnyAsync(st => st.AuditoriumId == auditoriumId);
        }
    }
}