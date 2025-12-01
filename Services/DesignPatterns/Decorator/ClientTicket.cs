using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public class ClientTicket : IClientTicket
    {
        private readonly TicketDto ticket;

        public ClientTicket(TicketDto ticket)
        {
            this.ticket = ticket;
        }

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
