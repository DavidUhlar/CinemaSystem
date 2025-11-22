using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public interface IReservationBuilder
    {
        IReservationBuilder Reset();
        IReservationBuilder SetTicket(Ticket ticket);
        IReservationBuilder SetTickets(List<Ticket> ticket);
        IReservationBuilder SetCustomer(Customer customer);
        Reservation Build();
    }
}
