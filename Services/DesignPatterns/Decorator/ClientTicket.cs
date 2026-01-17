using CinemaSystem.Models;
using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public class ClientTicket(TicketDto ticket) : IClientTicket
    {
        private readonly TicketDto ticket = ticket;

        public string GetDescription()
        {
            return $"{ticket.Type} ticket";
        }

        public TicketType GetTicketType()
        {
            return ticket.Type;
        }

        public decimal GetTotalPrice()
        {
            return ticket.Price;
        }
    }
}
