namespace CinemaTicketBooking_WebAPI.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(int id)
            : base($"Customer with Id {id} was not found.") { }
    }
}