namespace CinemaSystem.Models
{
    public abstract class Event
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public decimal BasePrice { get; set; }
        public EventType Type { get; set; }

        public int CinemaHallId { get; set; }
        public virtual CinemaHall CinemaHall { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}