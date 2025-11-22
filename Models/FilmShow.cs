namespace CinemaSystem.Models
{
    public class FilmShow
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public decimal BasePrice { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;

        public int CinemaHallId { get; set; }
        public virtual CinemaHall CinemaHall { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}