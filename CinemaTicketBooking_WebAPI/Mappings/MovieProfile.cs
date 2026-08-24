using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;

namespace CinemaTicketBooking_WebAPI.Mapping
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // ---- Entity → Response DTOs ----
            CreateMap<Movie, MovieV1Dto>();
            CreateMap<Movie, MovieV2Dto>();

            // ---- Request DTOs → Entity ----
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Shows, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            // ---- Paged results (generic) ----
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}