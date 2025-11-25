using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy
{
    public class StandardPricingStrategy : IPricingStrategy
    {

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            return basePrice;
        }
    }
}
