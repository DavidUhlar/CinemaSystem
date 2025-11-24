using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy
{
    public class VipPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowMultiplier = 1.5m;
        private const decimal ConcertDiscount = 0.9m;

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
