using CinemaTicketBooking_WebAPI.Enums;

namespace CinemaTicketBooking_WebAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CustomerId { get; set; }
        public int ShowTimeId { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; }
        public ShowTime ShowTime { get; set; }
    }
}
