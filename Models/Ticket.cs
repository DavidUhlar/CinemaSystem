namespace CinemaSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public TicketType Status { get; set; }

        public decimal Price { get; set; }

        public int FilmShowId { get; set; }
        public virtual FilmShow FilmShow { get; set; } = null!;

        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; } = null!;

        public int SeatId { get; set; }
        public virtual Seat Seat { get; set; } = null!;
    }
}
