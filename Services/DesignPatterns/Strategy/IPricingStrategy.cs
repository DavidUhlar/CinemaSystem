using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Strategy
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal basePrice, Event eventShow);
    }
}
