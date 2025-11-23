namespace CinemaSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string ReservationCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ReservationStatus Status { get; set; }

        public ReservationType Type { get; set; }

        public ReservationPurpose Purpose { get; set; }

        public string ReservationNote { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}