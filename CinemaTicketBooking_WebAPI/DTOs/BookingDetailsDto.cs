using CinemaTicketBooking_WebAPI.Enums;

namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class BookingDetailsDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }

        public CustomerDto Customer { get; set; }
        public ShowTimeDetailsDto ShowTime { get; set; }
    }
}