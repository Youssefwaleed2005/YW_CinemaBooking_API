namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class ShowTimeNotFoundException : Exception
    {
        public ShowTimeNotFoundException(int id)
            : base($"ShowTime with Id {id} was not found.") { }
    }
}