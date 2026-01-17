using CinemaSystem.Models;
using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public class ReservationDirector(IReservationBuilder builder)
    {
        private readonly IReservationBuilder builder = builder;

        public Reservation CreateStandardReservation(Customer customer, List<Ticket> tickets, string? note = null)
        {
            builder.Reset()
                .SetCustomer(customer)
                .SetTickets(tickets)
                .SetReservationType(ReservationTypeEnum.Standard);
            if (!string.IsNullOrEmpty(note))
            {
                builder.SetReservationNote(note);
            }
            return builder.Build();
        }
        
        public Reservation CreateGroupReservation(Customer customer, List<Ticket> tickets, ReservationPurpose purpose, string? note = null)
        {
            builder.Reset()
                .SetCustomer(customer)
                .SetTickets(tickets)
                .SetReservationPurpose(purpose)
                .SetReservationType(ReservationTypeEnum.Group);
            if (!string.IsNullOrEmpty(note))
            {
                builder.SetReservationNote(note);
            }

            return builder.Build();
        }
    }
}
