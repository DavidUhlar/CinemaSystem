namespace CinemaSystem.Models
{
    public class CateringItem
    {
        public int Id { get; set; }
        public CateringSize Size { get; set; }
        public CateringType Type { get; set; }
        public decimal Price { get; set; }
    }
}
