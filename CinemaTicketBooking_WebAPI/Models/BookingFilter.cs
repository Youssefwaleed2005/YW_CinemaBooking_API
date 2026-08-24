using CinemaTicketBooking_WebAPI.Enums;

namespace CinemaTicketBooking_WebAPI.Models
{
    public class BookingFilter : PaginationParam
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? ShowTimeId { get; set; }
        public BookingStatus? Status { get; set; }
    }
}