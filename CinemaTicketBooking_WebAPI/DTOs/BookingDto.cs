using CinemaTicketBooking_WebAPI.Enums;

namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public int CustomerId { get; set; }
        public int ShowTimeId { get; set; }
    }
}