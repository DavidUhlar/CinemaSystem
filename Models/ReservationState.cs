namespace CinemaSystem.Models
{
    public class ReservationState
    {
        public int EventId { get; set; }
        public int CustomerId { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ReservationStep CurrentStep { get; set; } = ReservationStep.CustomerCreated;
    }
}
