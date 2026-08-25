    using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Enums;
using CinemaTicketBooking_WebAPI.Exceptions;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using CinemaTicketBooking_WebAPI.Services.Interfaces;

namespace CinemaTicketBooking_WebAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IShowTimeRepo _showTimeRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;

        public BookingService(
            IBookingRepo bookingRepo,
            IShowTimeRepo showTimeRepo,
            ICustomerRepo customerRepo,
            IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _showTimeRepo = showTimeRepo;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookingDto>> GetAll(BookingFilter filter)
        {
            var paged = await _bookingRepo.GetAll(filter);

            return new PagedResult<BookingDto>
            {
                Data = _mapper.Map<IEnumerable<BookingDto>>(paged.Data),
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };
        }

        public async Task<BookingDetailsDto> GetById(int id)
        {
            var booking = await _bookingRepo.GetById(id);
            if (booking is null)
                throw new BookingNotFoundException(id);

            return _mapper.Map<BookingDetailsDto>(booking);
        }

        public async Task<BookingDetailsDto> Create(CreateBookingDto dto)
        {
            // Business rule: booking cannot be created for a showtime that does not exist
            var showTime = await _showTimeRepo.GetById(dto.ShowTimeId);
            if (showTime is null)
                throw new ShowTimeNotFoundException(dto.ShowTimeId);

            // Business rule: a booking must belong to a valid guest customer
            var customer = await _customerRepo.GetById(dto.CustomerId);
            if (customer is null)
                throw new CustomerNotFoundException(dto.CustomerId);

            var booking = new Booking
            {
                CustomerId = dto.CustomerId,
                ShowTimeId = dto.ShowTimeId,
                Status = BookingStatus.Pending,   
                BookingDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _bookingRepo.Add(booking);

            var created = await _bookingRepo.GetById(booking.Id);
            return _mapper.Map<BookingDetailsDto>(created);
        }

        public async Task<BookingDetailsDto> Cancel(int id)
        {
            var booking = await _bookingRepo.GetById(id);
            if (booking is null)
                throw new BookingNotFoundException(id);

            if (booking.Status == BookingStatus.Cancelled)
                throw new InvalidBookingException($"Booking {id} is already cancelled.");

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepo.Update(booking);

            return _mapper.Map<BookingDetailsDto>(booking);
        }

        public async Task<BookingDetailsDto> Confirm(int id)
        {
            var booking = await _bookingRepo.GetById(id);
            if (booking is null)
                throw new BookingNotFoundException(id);

            if (booking.Status == BookingStatus.Cancelled)
                throw new InvalidBookingException($"Booking {id} is cancelled and cannot be confirmed.");

            if (booking.Status == BookingStatus.Confirmed)
                throw new InvalidBookingException($"Booking {id} is already confirmed.");

            booking.Status = BookingStatus.Confirmed;
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepo.Update(booking);

            return _mapper.Map<BookingDetailsDto>(booking);
        }
    }
}