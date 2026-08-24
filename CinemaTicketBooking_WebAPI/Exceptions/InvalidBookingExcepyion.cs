namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class InvalidBookingException : Exception
    {
        public InvalidBookingException(string message)
            : base(message) { }
    }
}