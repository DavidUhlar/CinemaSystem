using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy.Time
{
    public class EarlyConcertPricingStrategy : IPricingStrategy
    {
        private const decimal ConcertDiscountPrice = 0.9m;

        public decimal CalculatePrice(decimal basePrice, Event eventShow)
        {
            if (eventShow.Type == EventType.Concert && IsEarly(eventShow.StartTime))
            {
                return basePrice * ConcertDiscountPrice;
            }
            else
            {
                return basePrice;
            }
        }

        private bool IsEarly(DateTime date)
        {
            var daysUntilEvent = (date - DateTime.UtcNow).Days;
            return daysUntilEvent >= 30;
        }
    }
}
