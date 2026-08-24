// Exceptions/BookingNotFoundException.cs
namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException(int id)
            : base($"Booking with Id {id} was not found.") { }
    }
}