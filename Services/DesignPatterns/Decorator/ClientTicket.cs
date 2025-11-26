using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public class ClientTicket : IClientTicket
    {
        private readonly Ticket ticket;

        public ClientTicket(Ticket ticket)
        {
            this.ticket = ticket;
        }

        public string GetDescription()
        {
            return $"{ticket.Type} ticket";
        }

        public Ticket GetTicket()
        {
            return ticket;
        }

        public decimal GetTotalPrice()
        {
            return ticket.Price;
        }
    }
}
