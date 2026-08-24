namespace CinemaTicketBooking_WebAPI.Models
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime ShowTimeValue { get; set; }

        public int MovieId { get; set; }
        public int AuditoriumId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        
        public Movie Movie { get; set; }
        public Auditorium Auditorium { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }

}
