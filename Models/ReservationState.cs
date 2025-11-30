namespace CinemaSystem.Models
{
    public class ReservationState
    {
        public int EventId { get; set; }
        public int CustomerId { get; set; }
        public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
        public ReservationStep CurrentStep { get; set; } = ReservationStep.CustomerCreated;
    }

    public class TicketDto
    {
        public int SeatId { get; set; }
        public TicketType Type { get; set; }
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public int? FoodItemId { get; set; }
        public int? DrinkItemId { get; set; }
        public decimal TotalPrice { get; set; }
        public string TotalDescription { get; set; } = "";
    }
}
