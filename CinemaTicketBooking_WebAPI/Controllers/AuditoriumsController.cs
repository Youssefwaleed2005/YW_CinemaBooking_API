using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking_WebAPI.Controllers
{
    [ApiController]
    [Route("api/auditoriums")]
    public class AuditoriumsController : ControllerBase
    {
        private readonly IAuditoriumService _auditoriumService;
        private readonly IShowTimeService _showTimeService;

        public AuditoriumsController(IAuditoriumService auditoriumService, IShowTimeService showTimeService)
        {
            _auditoriumService = auditoriumService;
            _showTimeService = showTimeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditoriumDto>>> GetAll()
        {
            return Ok(await _auditoriumService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuditoriumDto>> GetById(int id)
        {
            return Ok(await _auditoriumService.GetById(id));
        }

        [HttpGet("{id:int}/showtimes")]
        public async Task<ActionResult<IEnumerable<ShowTimeDetailsDto>>> GetShowTimes(int id, [FromQuery] DateTime? date)
        {
            return Ok(await _showTimeService.GetByAuditorium(id, date));
        }

        [HttpPost]
        public async Task<ActionResult<AuditoriumDto>> Create([FromBody] CreateAuditoriumDto dto)
        {
            var auditorium = await _auditoriumService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = auditorium.Id }, auditorium);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AuditoriumDto>> Update(int id, [FromBody] UpdateAuditoriumDto dto)
        {
            return Ok(await _auditoriumService.Update(id, dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _auditoriumService.Delete(id);
            return NoContent();
        }
    }
}