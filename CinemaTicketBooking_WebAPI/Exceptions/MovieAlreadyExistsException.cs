namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class MovieAlreadyExistsException : Exception
    {
        public MovieAlreadyExistsException(string name)
            : base($"A movie with the name '{name}' already exists.") { }
    }
}