namespace CinemaSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string ReservationCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ReservationStatus Status { get; set; }

        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}