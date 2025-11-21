namespace CinemaSystem.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }

        public int CinemaHallId { get; set; }
        public virtual CinemaHall CinemaHall { get; set; } = null!;

        public virtual ICollection<SeatReservation> Reservations { get; set; } = new List<SeatReservation>();
    }
}
