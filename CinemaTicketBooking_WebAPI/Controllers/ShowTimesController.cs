using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking_WebAPI.Controllers
{
    [ApiController]
    [Route("api/showtimes")]
    public class ShowTimesController : ControllerBase
    {
        private readonly IShowTimeService _showTimeService;

        public ShowTimesController(IShowTimeService showTimeService)
        {
            _showTimeService = showTimeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowTimeDetailsDto>>> GetAll()
        {
            return Ok(await _showTimeService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShowTimeDetailsDto>> GetById(int id)
        {
            return Ok(await _showTimeService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ShowTimeDetailsDto>> Create([FromBody] CreateShowTimeDto dto)
        {
            var showTime = await _showTimeService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = showTime.Id }, showTime);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ShowTimeDetailsDto>> Update(int id, [FromBody] UpdateShowTimeDto dto)
        {
            return Ok(await _showTimeService.Update(id, dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _showTimeService.Delete(id);
            return NoContent();
        }
    }
}