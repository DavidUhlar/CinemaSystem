namespace CinemaSystem.Models
{
    public class FilmShow : Event
    {
        public AgeEnum AgeRestriction { get; set; }
        public Genre Genre { get; set; }
        public int LengthInMinutes { get; set; }
        public string Director { get; set; } = string.Empty;
    }
}
