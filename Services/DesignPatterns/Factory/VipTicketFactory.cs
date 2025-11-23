using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Factory
{
    public class VipTicketFactory : ITicketFactory
    {
        private readonly decimal vipPrice = 1.5m;
        public Ticket CreateTicket(Event eventShow, Seat seat)
        {
            return new Ticket
            {
                Event = eventShow,
                EventId = eventShow.Id,
                Seat = seat,
                SeatId = seat.Id,
                Price = eventShow.BasePrice * vipPrice,
                Type = TicketType.VIP
            };
        }
    }
}
