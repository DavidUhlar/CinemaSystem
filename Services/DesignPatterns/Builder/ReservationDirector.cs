using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public class ReservationDirector
    {
        private IReservationBuilder builder;

        public ReservationDirector(IReservationBuilder builder)
        {
            this.builder = builder;
        }

        public Reservation CreateStandardReservation(Customer customer, List<Ticket> tickets, string? note = null)
        {
            builder.Reset()
                .SetCustomer(customer)
                .SetTickets(tickets)
                .SetReservationType(ReservationType.Standard);
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
                .SetReservationType(ReservationType.Group);
            if (!string.IsNullOrEmpty(note))
            {
                builder.SetReservationNote(note);
            }

            return builder.Build();
        }
    }
}
