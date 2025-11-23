using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Factory
{
    public class StandardTicketFactory : ITicketFactory
    {
        public Ticket CreateTicket(Event eventShow, Seat seat)
        {
            return new Ticket
            {
                Event = eventShow,
                EventId = eventShow.Id,
                Seat = seat,
                SeatId = seat.Id,
                Price = eventShow.BasePrice,
                Type = TicketType.Standard
            };
        }
    }
}
