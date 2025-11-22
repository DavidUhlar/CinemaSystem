using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public class ReservationDirector
    {
        public Reservation CreateStandardReservation(Customer customer, List<Ticket> tickets)
        {
            var builder = new ReservationBuilder();

            return builder
                .SetCustomer(customer)
                .SetTickets(tickets)
                .Build();
        }
        
        public Reservation CreateGroupReservation(Customer customer, List<Ticket> tickets)
        {
            var builder = new GroupReservationBuilder();

            return builder
                .SetCustomer(customer)
                .SetTickets(tickets)
                .Build();
        }
        

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
    }
}
