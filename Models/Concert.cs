namespace CinemaSystem.Models
{
    public class Concert : Event
    {
        public string Artist { get; set; } = string.Empty;
        public GenreMusic GenreMusic { get; set; }
    }
}
