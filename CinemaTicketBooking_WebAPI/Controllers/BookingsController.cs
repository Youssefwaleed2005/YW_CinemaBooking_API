// Controllers/BookingsController.cs
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking_WebAPI.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BookingDto>>> GetAll([FromQuery] BookingFilter filter)
        {
            var result = await _bookingService.GetAll(filter);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookingDetailsDto>> GetById(int id)
        {
            var booking = await _bookingService.GetById(id);
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDetailsDto>> Create([FromBody] CreateBookingDto dto)
        {
            var booking = await _bookingService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpPatch("{id:int}/cancel")]
        public async Task<ActionResult<BookingDetailsDto>> Cancel(int id)
        {
            var booking = await _bookingService.Cancel(id);
            return Ok(booking);
        }

        [HttpPatch("{id:int}/confirm")]
        public async Task<ActionResult<BookingDetailsDto>> Confirm(int id)
        {
            var booking = await _bookingService.Confirm(id);
            return Ok(booking);
        }
    }
}