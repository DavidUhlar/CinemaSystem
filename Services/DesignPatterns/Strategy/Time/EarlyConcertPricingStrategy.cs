using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Time
{
    public class EarlyConcertPricingStrategy : IPricingStrategy
    {
        private const decimal ConcertDiscountPrice = 0.9m;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            return basePrice * ConcertDiscountPrice;
        }
    }
}
