using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Repos.Interfaces
{
    public interface IMoviesRepo
    {
        Task<PagedResult<Movie>> GetAll(MovieFilter filter);

        Task<Movie?> GetById(int id);

        Task Add(Movie movie);

        Task Update(Movie movie);

        Task Delete(Movie movie);

        Task<bool> ExistsByName(string name, int? excludeId = null);

        Task<bool> HasShowTimes(int movieId);
    }
}