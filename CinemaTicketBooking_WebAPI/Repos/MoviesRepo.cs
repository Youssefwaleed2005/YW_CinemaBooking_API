// Repos/MovieRepo.cs
using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking_WebAPI.Repos
{
    public class MoviesRepo : IMoviesRepo
    {
        private readonly AppDBContext _context;

        public MoviesRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Movie>> GetAll(MovieFilter filter)
        {
            var query = _context.Movies.AsQueryable();

            // ---- Filtering ----
            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(m => m.Name.Contains(filter.Search));

            if (!string.IsNullOrWhiteSpace(filter.Genre))
                query = query.Where(m => m.Genre == filter.Genre);

            if (filter.AvailableInCinema.HasValue)
                query = query.Where(m => m.AvailableInCinema == filter.AvailableInCinema.Value);

            // ---- Sorting ----
            bool desc = filter.Order?.ToLower() == "desc";

            query = filter.SortBy?.ToLower() switch
            {
                "name" => desc ? query.OrderByDescending(m => m.Name)
                               : query.OrderBy(m => m.Name),

                "releasedate" => desc ? query.OrderByDescending(m => m.ReleaseDate)
                                      : query.OrderBy(m => m.ReleaseDate),

                _ => query.OrderBy(m => m.Id)
            };

            // ---- Pagination ----
            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Movie>
            {
                Data = data,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Movie?> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByName(string name, int? excludeId = null)
        {
            return await _context.Movies
                .AnyAsync(m => m.Name == name && (!excludeId.HasValue || m.Id != excludeId.Value));
        }

        public async Task<bool> HasShowTimes(int movieId)
        {
            return await _context.ShowTimes.AnyAsync(st => st.MovieId == movieId);
        }
    }
}