using CinemaSystem.Models;
using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public interface IReservationBuilder
    {
        IReservationBuilder Reset();
        IReservationBuilder SetTickets(List<Ticket> ticket);
        IReservationBuilder SetCustomer(Customer customer);
        IReservationBuilder SetReservationNote(string note);
        IReservationBuilder SetReservationPurpose(ReservationPurpose purpose);
        IReservationBuilder SetReservationType(ReservationTypeEnum type);
        Reservation Build();
    }
}
