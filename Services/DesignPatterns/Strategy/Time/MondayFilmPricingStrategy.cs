using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Time
{
    public class MondayFilmPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowDiscount = 1;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            if (eventShow.Type == EventType.Film && eventShow.StartTime.DayOfWeek == DayOfWeek.Monday)
            {
                var priceResult = basePrice - FilmShowDiscount;
                return priceResult <= 0 ? basePrice : priceResult;
            }
            else
            {
                return basePrice;
            }
        }
    }
}
