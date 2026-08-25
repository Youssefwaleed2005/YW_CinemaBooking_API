namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class ShowTimeDetailsDto
    {
        public int Id { get; set; }
        public DateTime ShowTimeValue { get; set; }

        public MovieV2Dto Movie { get; set; }
        public AuditoriumDto Auditorium { get; set; }
    }
}