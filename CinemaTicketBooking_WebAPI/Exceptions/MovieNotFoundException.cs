namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(int id)
            : base($"Movie with Id {id} was not found.") { }
    }
}