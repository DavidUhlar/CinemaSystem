namespace CinemaSystem.Models
{
    public class FilmShow : Event
    {
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;
    }
}
