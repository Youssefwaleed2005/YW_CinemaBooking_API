using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ShowTimeId { get; set; }
    }
}