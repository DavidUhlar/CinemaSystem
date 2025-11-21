namespace CinemaSystem.Models
{
    public class SeatReservation
    {
        public int Id { get; set; }

        public int FilmShowId { get; set; }
        public virtual FilmShow FilmShow { get; set; } = null!;

        public int SeatId { get; set; }
        public virtual Seat Seat { get; set; } = null!;

        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; } = null!;

        public decimal Price { get; set; }
    }
}