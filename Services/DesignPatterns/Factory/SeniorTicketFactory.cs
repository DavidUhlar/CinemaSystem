using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Factory
{
    public class SeniorTicketFactory : ITicketFactory
    {
        
        public Ticket CreateTicket(Event eventShow, Seat seat)
        {
            return new Ticket
            {
                EventId = eventShow.Id,
                SeatId = seat.Id,
                Price = eventShow.BasePrice,
                Type = TicketType.Senior,
                Event = eventShow,
                Seat = seat,
            };
        }

        public TicketDto CreateTicketDto(Event eventShow, Seat seat)
        {
            return new TicketDto
            {
                EventId = eventShow.Id,
                SeatId = seat.Id,
                Price = eventShow.BasePrice,
                Type = TicketType.Senior,
                TotalPrice = eventShow.BasePrice,
            };
        }
    }
}
