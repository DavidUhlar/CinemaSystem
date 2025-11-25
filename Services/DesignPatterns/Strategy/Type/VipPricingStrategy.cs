using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Type
{
    public class VipPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowMultiplier = 1.5m;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            switch (eventShow.Type)
            {
                case EventType.Film:
                    return basePrice * FilmShowMultiplier;
                default:
                    return basePrice;
            }
        }
    }
}
