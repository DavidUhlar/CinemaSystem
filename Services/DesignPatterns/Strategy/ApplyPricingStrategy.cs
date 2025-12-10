using CinemaSystem.Components.Pages.CinemaReservationWorkflow.EventPages;
using CinemaSystem.Models;
using CinemaSystem.Services.DesignPatterns.Strategy.Time;
using CinemaSystem.Services.DesignPatterns.Strategy.Type;

namespace CinemaSystem.Services.DesignPatterns.Strategy
{
    public class ApplyPricingStrategy
    {
        private IPricingStrategy strategyType;
        public ApplyPricingStrategy(TicketType ticketType)
        {
            strategyType = GetPricingStrategyType(ticketType);
        }

        public decimal CalculateFinalPrice(Event eventItem)
        {
            var currentPrice = eventItem.BasePrice;

            currentPrice = CalculatePricingStrategyTime(eventItem, currentPrice);
            currentPrice = strategyType.CalculatePrice(currentPrice, eventItem);

            return currentPrice;
        }

        private decimal CalculatePricingStrategyTime(Event eventItem, decimal currentPrice)
        {
            switch (eventItem.Type)
            {
                case EventType.Film:
                    if (IsMonday(eventItem.StartTime.DayOfWeek))
                    {
                        var mondayStrategy = new MondayFilmPricingStrategy();
                        currentPrice = mondayStrategy.CalculatePrice(currentPrice, eventItem);
                    } 
                    else if (IsWeekend(eventItem.StartTime.DayOfWeek))
                    {
                        var weekendStrategy = new WeekendFilmPricingStrategy();
                        currentPrice = weekendStrategy.CalculatePrice(currentPrice, eventItem);
                    }
                    break;
                case EventType.Concert:
                    if (IsEarly(eventItem.StartTime))
                    {
                        var earlyStrategy = new EarlyConcertPricingStrategy();
                        currentPrice = earlyStrategy.CalculatePrice(currentPrice, eventItem);
                    }
                    break;
            }
            
            return currentPrice;
        }

        private bool IsMonday(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Monday;
        }
        private bool IsWeekend(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

        private bool IsEarly(DateTime date)
        {
            var daysUntilEvent = (date - DateTime.UtcNow).Days;
            return daysUntilEvent >= 30;
        }

        private IPricingStrategy GetPricingStrategyType(TicketType ticketType)
        {
            IPricingStrategy pricingStrategy;
            switch (ticketType)
            {
                case TicketType.Student:
                    pricingStrategy = new StudentPricingStrategy();
                    break;
                case TicketType.Senior:
                    pricingStrategy = new SeniorPricingStrategy();
                    break;
                case TicketType.VIP:
                    pricingStrategy = new VipPricingStrategy();
                    break;
                default:
                    pricingStrategy = new StandardPricingStrategy();
                    break;
            }
            return pricingStrategy;
        }
    }
}
