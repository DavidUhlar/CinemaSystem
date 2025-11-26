using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public class FoodDecorator : CateringDecorator
    {
        public FoodDecorator(IClientTicket component, CateringItem item) : base(component, item)
        {
            if (item.Type != CateringType.Popcorn) {
                throw new ArgumentException("CateringItem must be of type Popcorn for FoodDecorator.");
            }
        }

        public override string GetDescription()
        {
            var ticket = component.GetTicket();
            string description = component.GetDescription() + $" + {item.Size} {item.Type}";

            if (ticket.Type == TicketType.VIP)
            {
                switch (item.Size)
                {
                    case CateringSize.Small:
                        description += " (VIP bonus, FREE)";
                        break;
                    case CateringSize.Medium:
                        description += " (VIP bonus, 10% off)";
                        break;
                    case CateringSize.Large:
                        description += " (VIP bonus, 20% off)";
                        break;
                    case CateringSize.XXL:
                        description += " (VIP bonus, 30% off)";
                        break;
                }
            }
            return description;
        }

        public override decimal GetTotalPrice()
        {
            decimal basePrice = component.GetTotalPrice();
            decimal foodPrice = item.Price;

            var ticket = component.GetTicket();

            if (ticket.Type == TicketType.VIP)
            {
                switch(item.Size)
                {
                    case CateringSize.Small:
                        foodPrice = 0;
                        break;
                    case CateringSize.Medium:
                        foodPrice *= 0.9m;
                        break;
                    case CateringSize.Large:
                        foodPrice *= 0.8m;
                        break;
                    case CateringSize.XXL:
                        foodPrice *= 0.7m;
                        break;
                }
            }

            return basePrice + foodPrice;
        }
    }
}
