namespace CinemaSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public TicketType Type { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; } = null!;

        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; } = null!;

        public int SeatId { get; set; }
        public virtual Seat Seat { get; set; } = null!;

        public int? FoodItemId { get; set; }
        public virtual CateringItem? FoodItem { get; set; }

        public int? DrinkItemId { get; set; }
        public virtual CateringItem? DrinkItem { get; set; }

        public decimal TotalPrice { get; set; }
        public string TotalDescription { get; set; }
    }
}
