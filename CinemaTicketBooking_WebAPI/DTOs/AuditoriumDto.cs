namespace CinemaTicketBooking_WebAPI.DTOs
{
    public class AuditoriumDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }
}