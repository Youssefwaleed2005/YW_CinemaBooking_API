using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class UpdateShowTimeDto
    {
        [Required]
        public DateTime ShowTimeValue { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
    }
}