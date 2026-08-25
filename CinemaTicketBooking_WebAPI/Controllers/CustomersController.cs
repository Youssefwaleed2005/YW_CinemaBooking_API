using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking_WebAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            return Ok(await _customerService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            var customer = await _customerService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            return Ok(await _customerService.Update(id, dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);
            return NoContent();
        }
    }
}