using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public class ReservationDirector
    {
        public Reservation CreateStandardReservationOneTicket(Customer customer, Ticket ticket, string? note = null)
        {
            var builder = new ReservationBuilder();

            builder
                .SetCustomer(customer)
                .SetTicket(ticket)
                .SetReservationType(ReservationType.Standard);
            if (!string.IsNullOrEmpty(note))
            {
                builder.SetReservationNote(note);
            }
            return builder.Build();
        }

        public Reservation CreateStandardReservation(Customer customer, List<Ticket> tickets, string? note = null)
        {
            var builder = new ReservationBuilder();

            builder
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
            var builder = new GroupReservationBuilder();

            builder
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
        
        /*
        public Reservation CreateReservation(Customer customer, List<Ticket> tickets)
        {
            if (tickets.Count >= 5)
            {
                return CreateGroupReservation(customer, tickets);
            }
            else
            {
                return CreateStandardReservation(customer, tickets);
            }
        }
        */
    }
}
