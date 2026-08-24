using Asp.Versioning;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking_WebAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<MovieV1Dto>>> GetAll([FromQuery] MovieFilter filter)
        {
            var result = await _movieService.GetAllV1(filter);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieV1Dto>> GetById(int id)
        {
            var movie = await _movieService.GetByIdV1(id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieV1Dto>> Create([FromBody] CreateMovieDto dto)
        {
            var movie = await _movieService.CreateV1(dto);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id, version = "1.0" }, movie);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MovieV1Dto>> Update(int id, [FromBody] UpdateMovieDto dto)
        {
            var movie = await _movieService.UpdateV1(id, dto);
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