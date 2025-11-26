using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public class DrinkDecorator : CateringDecorator
    {
        public DrinkDecorator(IClientTicket component, CateringItem item) : base(component, item)
        {
            if (item.Type != CateringType.Drink) {
                throw new ArgumentException("CateringItem must be of type Drink for DrinkDecorator.");
            }
        }

        public override string GetDescription()
        {
            string description = component.GetDescription() + $" + {item.Size} {item.Type}";
            var ticket = component.GetTicket();
            if (ticket.Type == TicketType.VIP && item.Size == CateringSize.Small)
            {
                description += " (VIP Bonus)";
            }
            else if (item.Size == CateringSize.Small && component.GetTotalPrice() >= 15m)
            {
                description += " (FREE)";
            }
            return description;
        }

        public override decimal GetTotalPrice()
        {
            decimal basePrice = component.GetTotalPrice();
            decimal drinkPrice = item.Price;
            var ticket = component.GetTicket();

            if (ticket.Type == TicketType.VIP && item.Size == CateringSize.Small)
            {
                drinkPrice = 0;
            }
            else if (item.Size == CateringSize.Small && component.GetTotalPrice() >= 15m)
            {
                drinkPrice = 0;
            }
            return basePrice + drinkPrice;
        }
    }
}
