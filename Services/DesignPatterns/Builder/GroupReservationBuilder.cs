using CinemaSystem.Models;
using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public class GroupReservationBuilder : IReservationBuilder
    {
        private Reservation reservation = null!;

        public GroupReservationBuilder()
        {
            Reset();
        }

        public Reservation Build()
        {
            reservation.TotalPrice = reservation.Tickets.Sum(t => t.TotalPrice) * 0.85m;
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
            reservation.Type = ReservationTypeEnum.Group;
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
            return $"RES-GRP-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}
