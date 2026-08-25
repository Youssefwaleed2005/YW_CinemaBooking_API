using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Exceptions;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using CinemaTicketBooking_WebAPI.Services.Interfaces;

namespace CinemaTicketBooking_WebAPI.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly IShowTimeRepo _showTimeRepo;
        private readonly IMoviesRepo _movieRepo;
        private readonly IAuditoriumRepo _auditoriumRepo;
        private readonly IMapper _mapper;

        public ShowTimeService(
            IShowTimeRepo showTimeRepo,
            IMoviesRepo movieRepo,
            IAuditoriumRepo auditoriumRepo,
            IMapper mapper)
        {
            _showTimeRepo = showTimeRepo;
            _movieRepo = movieRepo;
            _auditoriumRepo = auditoriumRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowTimeDetailsDto>> GetAll()
        {
            var showTimes = await _showTimeRepo.GetAll();
            return _mapper.Map<IEnumerable<ShowTimeDetailsDto>>(showTimes);
        }

        public async Task<ShowTimeDetailsDto> GetById(int id)
        {
            var showTime = await _showTimeRepo.GetById(id);
            if (showTime is null)
                throw new ShowTimeNotFoundException(id);

            return _mapper.Map<ShowTimeDetailsDto>(showTime);
        }

        public async Task<IEnumerable<ShowTimeDetailsDto>> GetByAuditorium(int auditoriumId, DateTime? date)
        {
            var auditorium = await _auditoriumRepo.GetById(auditoriumId);
            if (auditorium is null)
                throw new AuditoriumNotFoundException(auditoriumId);

            var showTimes = await _showTimeRepo.GetByAuditorium(auditoriumId, date);
            return _mapper.Map<IEnumerable<ShowTimeDetailsDto>>(showTimes);
        }

        public async Task<ShowTimeDetailsDto> Create(CreateShowTimeDto dto)
        {
            // Business rule: a movie referenced by a showtime must exist
            var movie = await _movieRepo.GetById(dto.MovieId);
            if (movie is null)
                throw new MovieNotFoundException(dto.MovieId);

            // Business rule: a movie can only be scheduled according to its cinema availability
            if (!movie.AvailableInCinema)
                throw new InvalidBookingException(
                    $"Movie '{movie.Name}' is not available in cinema and cannot be scheduled.");

            // Business rule: an auditorium referenced by a showtime must exist
            var auditorium = await _auditoriumRepo.GetById(dto.AuditoriumId);
            if (auditorium is null)
                throw new AuditoriumNotFoundException(dto.AuditoriumId);

            var showTime = _mapper.Map<ShowTime>(dto);
            await _showTimeRepo.Add(showTime);

            var created = await _showTimeRepo.GetById(showTime.Id);
            return _mapper.Map<ShowTimeDetailsDto>(created);
        }

        public async Task<ShowTimeDetailsDto> Update(int id, UpdateShowTimeDto dto)
        {
            var showTime = await _showTimeRepo.GetById(id);
            if (showTime is null)
                throw new ShowTimeNotFoundException(id);

            var movie = await _movieRepo.GetById(dto.MovieId);
            if (movie is null)
                throw new MovieNotFoundException(dto.MovieId);

            if (!movie.AvailableInCinema)
                throw new InvalidBookingException(
                    $"Movie '{movie.Name}' is not available in cinema and cannot be scheduled.");

            var auditorium = await _auditoriumRepo.GetById(dto.AuditoriumId);
            if (auditorium is null)
                throw new AuditoriumNotFoundException(dto.AuditoriumId);

            showTime.ShowTimeValue = dto.ShowTimeValue;
            showTime.MovieId = dto.MovieId;
            showTime.AuditoriumId = dto.AuditoriumId;
            showTime.UpdatedAt = DateTime.UtcNow;

            await _showTimeRepo.Update(showTime);

            var updated = await _showTimeRepo.GetById(id);
            return _mapper.Map<ShowTimeDetailsDto>(updated);
        }

        public async Task Delete(int id)
        {
            var showTime = await _showTimeRepo.GetById(id);
            if (showTime is null)
                throw new ShowTimeNotFoundException(id);

            if (await _showTimeRepo.HasBookings(id))
                throw new InvalidBookingException(
                    $"ShowTime {id} cannot be deleted because it has bookings.");

            await _showTimeRepo.Delete(showTime);
        }
    }
}