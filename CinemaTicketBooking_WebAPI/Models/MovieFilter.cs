namespace CinemaTicketBooking_WebAPI.Models
{
    public class MovieFilter : PaginationParam
    {
        public string? Search { get; set; }              // partial movie name
        public string? Genre { get; set; }               // filter by genre
        public bool? AvailableInCinema { get; set; }

        public string? SortBy { get; set; }              // "name" or "releaseDate"
        public string? Order { get; set; } = "asc";      // "asc" or "desc"
    }
}