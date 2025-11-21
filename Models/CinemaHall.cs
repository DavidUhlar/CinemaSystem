namespace CinemaSystem.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }

        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public virtual ICollection<FilmShow> FilmShows { get; set; } = new List<FilmShow>();
    }
}
