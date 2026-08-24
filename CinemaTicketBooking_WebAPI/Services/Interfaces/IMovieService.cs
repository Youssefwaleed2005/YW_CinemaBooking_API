using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<PagedResult<MovieV1Dto>> GetAllV1(MovieFilter filter);
        Task<PagedResult<MovieV2Dto>> GetAllV2(MovieFilter filter);

        Task<MovieV1Dto> GetByIdV1(int id);
        Task<MovieV2Dto> GetByIdV2(int id);
        Task<MovieV1Dto> CreateV1(CreateMovieDto dto);
        Task<MovieV1Dto> UpdateV1(int id, UpdateMovieDto dto);

        Task<MovieV2Dto> CreateV2(CreateMovieDto dto);
        Task<MovieV2Dto> UpdateV2(int id, UpdateMovieDto dto);
        Task Delete(int id);
    }
}