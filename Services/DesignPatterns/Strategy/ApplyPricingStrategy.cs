using CinemaSystem.Models;
using CinemaSystem.Services.DesignPatterns.Strategy.Time;
using CinemaSystem.Services.DesignPatterns.Strategy.Type;

namespace CinemaSystem.Services.DesignPatterns.Strategy
{
    public class ApplyPricingStrategy
    {

        private List<IPricingStrategy> strategies = new List<IPricingStrategy>();
        private IPricingStrategy strategyType;
        public ApplyPricingStrategy(TicketType ticketType)
        {
            strategies.Add(new MondayFilmPricingStrategy());
            strategies.Add(new WeekendFilmPricingStrategy());
            strategies.Add(new EarlyConcertPricingStrategy());
            strategyType = getPricingStrategyType(ticketType);
        }

        public decimal CalculateFinalPrice(Event eventItem)
        {
            var currentPrice = eventItem.BasePrice;

            foreach (var strategy in strategies)
            {
                currentPrice = strategy.CalculatePrice(currentPrice, eventItem);
            }
            currentPrice = strategyType.CalculatePrice(currentPrice, eventItem);

            return currentPrice;
        }

        private IPricingStrategy getPricingStrategyType(TicketType ticketType)
        {
            IPricingStrategy pricingStrategy;
            switch (ticketType)
            {
                case TicketType.Student:
                    pricingStrategy = new StudentPricingStrategy();
                    break;
                case TicketType.Senior:
                    pricingStrategy = new SeniorPricingStrategy();
                    break;
                case TicketType.VIP:
                    pricingStrategy = new VipPricingStrategy();
                    break;
                default:
                    pricingStrategy = new StandardPricingStrategy();
                    break;
            }
            return pricingStrategy;
        }
    }
}
