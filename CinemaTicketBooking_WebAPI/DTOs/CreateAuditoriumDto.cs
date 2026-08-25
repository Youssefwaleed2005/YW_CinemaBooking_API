using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class CreateAuditoriumDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string RoomNumber { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Capacity { get; set; }

        public bool Available { get; set; }
    }
}