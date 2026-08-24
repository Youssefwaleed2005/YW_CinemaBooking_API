namespace CinemaTicketBooking_WebAPI.Models
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<ShowTime> Shows { get; set; } = new List<ShowTime>();

    }
}
