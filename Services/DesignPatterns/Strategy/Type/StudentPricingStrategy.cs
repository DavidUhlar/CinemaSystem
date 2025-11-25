using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Type
{
    public class StudentPricingStrategy : IPricingStrategy
    {
        private const decimal FilmShowDiscount = 0.8m;
        private const decimal ConcertDiscount = 0.9m;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            switch (eventShow.Type)
            {
                case EventType.Film:
                    return basePrice * FilmShowDiscount;
                case EventType.Concert:
                    return basePrice * ConcertDiscount;
                default:
                    return basePrice;
            }
        }
    }
}
