using CinemaSystem.Models;
using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public abstract class CateringDecorator(IClientTicket component, CateringItem item) : IClientTicket
    {
        protected readonly IClientTicket component = component;
        protected readonly CateringItem item = item;

        public TicketType GetTicketType()
        {
            return component.GetTicketType();
        }

        public abstract decimal GetTotalPrice();
        public abstract string GetDescription();
    }
}
