using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Factory
{
    public class VipTicketFactory : ITicketFactory
    {
        public Ticket CreateTicket(Event eventShow, Seat seat)
        {
            return new Ticket
            {
                EventId = eventShow.Id,
                SeatId = seat.Id,
                Price = eventShow.BasePrice,
                Type = TicketType.VIP,
                Event = eventShow,
                Seat = seat
            };
        }

        public TicketDto CreateTicketDto(Event eventShow, Seat seat)
        {
            return new TicketDto
            {
                EventId = eventShow.Id,
                SeatId = seat.Id,
                Price = eventShow.BasePrice,
                Type = TicketType.VIP,
                TotalPrice = eventShow.BasePrice
            };
        }
    }
}
