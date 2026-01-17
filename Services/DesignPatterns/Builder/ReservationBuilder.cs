using CinemaSystem.Models;
using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public class ReservationBuilder : IReservationBuilder
    {
        private Reservation reservation = null!;

        public ReservationBuilder()
        {
            Reset();
        }

        public Reservation Build()
        {
            reservation.TotalPrice = reservation.Tickets.Sum(t => t.TotalPrice);
            reservation.ReservationCode = GenerateCode();
            reservation.Status = ReservationStatus.Completed;
            var result = reservation;
            Reset();
            return result;
        }

        public IReservationBuilder Reset()
        {
            reservation = new Reservation
            {
                Tickets = [],
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
            };
            return this;
        }

        public IReservationBuilder SetCustomer(Customer customer)
        {
            reservation.Customer = customer;
            reservation.CustomerId = customer.Id;
            return this;
        }

        public IReservationBuilder SetReservationNote(string note)
        {
            reservation.ReservationNote = note;
            return this;
        }

        public IReservationBuilder SetReservationPurpose(ReservationPurpose purpose)
        {
            reservation.Purpose = purpose;
            return this;
        }

        public IReservationBuilder SetReservationType(ReservationTypeEnum type)
        {
            reservation.Type = type;
            return this;
        }

        public IReservationBuilder SetTickets(List<Ticket> ticket)
        {
            foreach (var t in ticket)
            {
                reservation.Tickets.Add(t);
            }
            return this;
        }

        private static string GenerateCode()
        {
            return $"RES-ST-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}
