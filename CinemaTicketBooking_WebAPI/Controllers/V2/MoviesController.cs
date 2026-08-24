using Asp.Versioning;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking_WebAPI.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<MovieV2Dto>>> GetAll([FromQuery] MovieFilter filter)
        {
            var result = await _movieService.GetAllV2(filter);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieV2Dto>> GetById(int id)
        {
            var movie = await _movieService.GetByIdV2(id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieV2Dto>> Create([FromBody] CreateMovieDto dto)
        {
            var movie = await _movieService.CreateV2(dto);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id, version = "2.0" }, movie);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MovieV2Dto>> Update(int id, [FromBody] UpdateMovieDto dto)
        {
            var movie = await _movieService.UpdateV2(id, dto);
            return Ok(movie);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.Delete(id);
            return NoContent();
        }
    }
}