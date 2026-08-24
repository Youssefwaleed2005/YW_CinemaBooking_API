namespace CinemaTicketBooking_WebAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Genre { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }

        public ICollection<ShowTime> Shows { get; set; } = new List<ShowTime>();

    }
}
