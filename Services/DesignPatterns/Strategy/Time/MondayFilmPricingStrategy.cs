using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Time
{
    public class MondayFilmPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowDiscount = 1;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            var priceResult = basePrice - FilmShowDiscount;
            return priceResult <= 0 ? basePrice : priceResult;
        }
    }
}
