using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Exceptions;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using CinemaTicketBooking_WebAPI.Services.Interfaces;

namespace CinemaTicketBooking_WebAPI.Services
{
    public class MovieService : IMovieService
    {
        private  IMoviesRepo _movieRepo;
        private IMapper _mapper;

        public MovieService(IMoviesRepo movieRepo, IMapper mapper)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<MovieV1Dto>> GetAllV1(MovieFilter filter)
        {
            var paged = await _movieRepo.GetAll(filter);

            return new PagedResult<MovieV1Dto>
            {
                Data = _mapper.Map<IEnumerable<MovieV1Dto>>(paged.Data),
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };
        }

        public async Task<PagedResult<MovieV2Dto>> GetAllV2(MovieFilter filter)
        {
            var paged = await _movieRepo.GetAll(filter);

            return new PagedResult<MovieV2Dto>
            {
                Data = _mapper.Map<IEnumerable<MovieV2Dto>>(paged.Data),
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };
        }

        public async Task<MovieV1Dto> GetByIdV1(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie is null)
                throw new MovieNotFoundException(id);

            return _mapper.Map<MovieV1Dto>(movie);
        }

        public async Task<MovieV2Dto> GetByIdV2(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie is null)
                throw new MovieNotFoundException(id);

            return _mapper.Map<MovieV2Dto>(movie);
        }

        public async Task<MovieV2Dto> CreateV2(CreateMovieDto dto)
        {
            if (await _movieRepo.ExistsByName(dto.Name))
                throw new MovieAlreadyExistsException(dto.Name);

            var movie = _mapper.Map<Movie>(dto);
            await _movieRepo.Add(movie);

            return _mapper.Map<MovieV2Dto>(movie);
        }

        public async Task<MovieV1Dto> CreateV1(CreateMovieDto dto)
        {
            if (await _movieRepo.ExistsByName(dto.Name))
                throw new MovieAlreadyExistsException(dto.Name);

            var movie = _mapper.Map<Movie>(dto);
            await _movieRepo.Add(movie);

            return _mapper.Map<MovieV1Dto>(movie);
        }

        public async Task<MovieV2Dto> UpdateV2(int id, UpdateMovieDto dto)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie is null)
                throw new MovieNotFoundException(id);

            if (await _movieRepo.ExistsByName(dto.Name, excludeId: id))
                throw new MovieAlreadyExistsException(dto.Name);

            _mapper.Map(dto, movie);
            await _movieRepo.Update(movie);

            return _mapper.Map<MovieV2Dto>(movie);
        }

        public async Task<MovieV1Dto> UpdateV1(int id, UpdateMovieDto dto)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie is null)
                throw new MovieNotFoundException(id);

            if (await _movieRepo.ExistsByName(dto.Name, excludeId: id))
                throw new MovieAlreadyExistsException(dto.Name);

            _mapper.Map(dto, movie);
            await _movieRepo.Update(movie);

            return _mapper.Map<MovieV1Dto>(movie);
        }

        public async Task Delete(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie is null)
                throw new MovieNotFoundException(id);

            if (await _movieRepo.HasShowTimes(id))
                throw new Exception();

            await _movieRepo.Delete(movie);
        }
    }
}