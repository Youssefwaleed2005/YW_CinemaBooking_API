namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class MovieV2Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }
    }
}