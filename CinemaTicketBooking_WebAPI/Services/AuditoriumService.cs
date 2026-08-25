// Services/AuditoriumService.cs
using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Exceptions;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using CinemaTicketBooking_WebAPI.Services.Interfaces;

namespace CinemaTicketBooking_WebAPI.Services
{
    public class AuditoriumService : IAuditoriumService
    {
        private readonly IAuditoriumRepo _auditoriumRepo;
        private readonly IMapper _mapper;

        public AuditoriumService(IAuditoriumRepo auditoriumRepo, IMapper mapper)
        {
            _auditoriumRepo = auditoriumRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditoriumDto>> GetAll()
        {
            var auditoriums = await _auditoriumRepo.GetAll();
            return _mapper.Map<IEnumerable<AuditoriumDto>>(auditoriums);
        }

        public async Task<AuditoriumDto> GetById(int id)
        {
            var auditorium = await _auditoriumRepo.GetById(id);
            if (auditorium is null)
                throw new AuditoriumNotFoundException(id);

            return _mapper.Map<AuditoriumDto>(auditorium);
        }

        public async Task<AuditoriumDto> Create(CreateAuditoriumDto dto)
        {
            var auditorium = _mapper.Map<Auditorium>(dto);
            await _auditoriumRepo.Add(auditorium);

            return _mapper.Map<AuditoriumDto>(auditorium);
        }

        public async Task<AuditoriumDto> Update(int id, UpdateAuditoriumDto dto)
        {
            var auditorium = await _auditoriumRepo.GetById(id);
            if (auditorium is null)
                throw new AuditoriumNotFoundException(id);

            auditorium.RoomNumber = dto.RoomNumber;
            auditorium.Capacity = dto.Capacity;
            auditorium.Available = dto.Available;
            auditorium.UpdatedAt = DateTime.UtcNow;

            await _auditoriumRepo.Update(auditorium);

            return _mapper.Map<AuditoriumDto>(auditorium);
        }

        public async Task Delete(int id)
        {
            var auditorium = await _auditoriumRepo.GetById(id);
            if (auditorium is null)
                throw new AuditoriumNotFoundException(id);

            if (await _auditoriumRepo.HasShowTimes(id))
                throw new InvalidBookingException($"Auditorium {id} cannot be deleted because it has scheduled showtimes.");

            await _auditoriumRepo.Delete(auditorium);
        }
    }
}