using CinemaSystem.Models;
using System.Runtime.CompilerServices;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Time
{
    public class WeekendFilmPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowPlusPrice = 1;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            var priceResult = basePrice + FilmShowPlusPrice;
            return basePrice + FilmShowPlusPrice;
        }
    }
}
