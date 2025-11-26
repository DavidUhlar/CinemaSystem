namespace CinemaSystem.Services.DesignPatterns.Decorator
{
    public interface IClientTicket
    {
        decimal GetTotalPrice();
        string GetDescription();
        Models.Ticket GetTicket();
    }
}
