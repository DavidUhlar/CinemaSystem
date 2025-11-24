using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Factory
{
    public interface ITicketFactory
    {
        Ticket CreateTicket(Event eventShow, Seat seat);
    }
}
