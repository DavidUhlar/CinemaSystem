using CinemaSystem.Models;
using System.Runtime.CompilerServices;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Time
{
    public class WeekendFilmPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowPlusPrice = 1;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            if (eventShow.Type == EventType.Film && IsWeekend(eventShow.StartTime.DayOfWeek))
            {
                var priceResult = basePrice + FilmShowPlusPrice;
                return basePrice + FilmShowPlusPrice;
            }
            else
            {
                return basePrice;
            }
        }

        private bool IsWeekend(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }
    }
}
