using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public abstract class CateringDecorator : IClientTicket
    {
        protected readonly IClientTicket component;
        protected readonly CateringItem item;
        protected CateringDecorator(IClientTicket component, CateringItem item)
        {
            this.component = component;
            this.item = item;
        }

        public Ticket GetTicket()
        {
            return component.GetTicket();
        }

        public abstract decimal GetTotalPrice();
        public abstract string GetDescription();
    }
}
