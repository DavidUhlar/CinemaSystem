using CinemaSystem.Data;
using CinemaSystem.Models;

namespace CinemaSystem.Services.DesignPatterns.Command
{
    public class CreateReservationCommand(CinemaDbContext cinemaDb, Reservation reservation, MailService mailService) : ICommand
    {
        private readonly CinemaDbContext cinemaDb = cinemaDb;
        private readonly Reservation reservation = reservation;
        private readonly MailService mailService = mailService;

        private int lastReservationId;

        public void Execute()
        {
            cinemaDb.Reservations.Add(reservation);
            cinemaDb.SaveChanges();

            lastReservationId = reservation.Id;

            Console.WriteLine($"Reservation with ID {reservation.Id} created.");

            var seatInfo = reservation.Tickets
                  .Select(t => $"Row {t.Seat.Row}, Seat {t.Seat.Number} - {t.Type}")
                  .ToList();
           

            mailService.SendReservationConfirmation(
                reservation.Customer.Email,
                $"{reservation.Customer.FirstName} {reservation.Customer.LastName}",
                reservation.ReservationCode,
                reservation.Tickets.First().Event.Title,
                reservation.Tickets.First().Event.StartTime,
                seatInfo,
                reservation.TotalPrice

            );
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


                    mailService.SendReservationCancellation(
                        reservationToRemove.Customer.Email,
                        $"{reservationToRemove.Customer.FirstName} {reservationToRemove.Customer.LastName}",
                        reservationToRemove.ReservationCode,
                        reservationToRemove.Tickets.First().Event.Title
                    );
                }
            }
        }
    }
}
