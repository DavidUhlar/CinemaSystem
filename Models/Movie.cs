using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{
    
    public class Movie
    {

        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public int ageRestriction { get; set; }
        public Genre genre { get; set; }

        public virtual ICollection<FilmShow> FilmShows { get; set; } = new List<FilmShow>();
    }
}
