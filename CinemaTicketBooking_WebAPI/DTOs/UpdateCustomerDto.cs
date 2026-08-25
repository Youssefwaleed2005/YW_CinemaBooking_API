using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }
    }
}