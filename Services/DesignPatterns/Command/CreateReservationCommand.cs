using CinemaSystem.Data;
using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Command
{
    public class CreateReservationCommand : ICommand
    {
        private readonly CinemaDbContext cinemaDb;
        private readonly Reservation reservation;

        private int lastReservationId;
        public CreateReservationCommand(CinemaDbContext cinemaDb, Reservation reservation)
        {
            this.reservation = reservation;
            this.cinemaDb = cinemaDb;
        }

        public void Execute()
        {
            cinemaDb.Reservations.Add(reservation);
            cinemaDb.SaveChanges();

            lastReservationId = reservation.Id;

            Console.WriteLine($"Reservation with ID {reservation.Id} created.");
        }

        public void Undo()
        {
            if (lastReservationId != 0)
            {
                var reservationToRemove = cinemaDb.Reservations.Find(lastReservationId);
                if (reservationToRemove != null)
                {
                    cinemaDb.Reservations.Remove(reservationToRemove);
                    cinemaDb.SaveChanges();
                    Console.WriteLine($"Reservation with ID {lastReservationId} removed.");
                }
            }
        }
    }
}
