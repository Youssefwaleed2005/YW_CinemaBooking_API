namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class AuditoriumNotFoundException:Exception
    {
        public AuditoriumNotFoundException(int id) 
            : base($"Auditorium with id : {id} was not found.") { }
    }
}
