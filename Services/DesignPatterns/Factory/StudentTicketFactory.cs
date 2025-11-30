using CinemaSystem.Models;
using System.Runtime.CompilerServices;

namespace CinemaSystem.Services.DesignPatterns.Factory
{
    public class StudentTicketFactory : ITicketFactory
    {
        public Ticket CreateTicket(Event eventShow, Seat seat)
        {  
            return new Ticket
            {
                EventId = eventShow.Id,
                SeatId = seat.Id,
                Price = eventShow.BasePrice,
                Type = TicketType.Student,
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
                Type = TicketType.Student,
                TotalPrice = eventShow.BasePrice,
            };
        }
    }
}
