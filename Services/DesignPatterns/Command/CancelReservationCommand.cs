using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services.DesignPatterns.Command
{
    public class CancelReservationCommand : ICommand
    {
        private readonly CinemaDbContext cinemaDb;
        private readonly int reservationId;

        private Reservation? lastReservation;
        public CancelReservationCommand(CinemaDbContext cinemaDb, int reservationId)
        {
            this.reservationId = reservationId;
            this.cinemaDb = cinemaDb;
        }

        public void Execute()
        {
            if (reservationId != 0)
            {
                var lastReservationTemp = cinemaDb.Reservations.Find(reservationId);
                if (lastReservationTemp != null)
                {
                    lastReservation = lastReservationTemp;
                    cinemaDb.Reservations.Remove(lastReservationTemp);
                    cinemaDb.SaveChanges();
                    Console.WriteLine($"Reservation with ID {lastReservation.Id} removed.");
                }
            }
        }

        public void Undo()
        {
            if (lastReservation != null)
            {
                cinemaDb.Entry(lastReservation).State = EntityState.Detached;

                cinemaDb.Reservations.Add(lastReservation);
                cinemaDb.SaveChanges();

                Console.WriteLine($"Reservation with ID {lastReservation.Id} created.");
            }
        }
    }
}
