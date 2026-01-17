using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class ReservationService(CinemaDbContext db)
    {

        private readonly CinemaDbContext db = db;

        public async Task<Reservation?> GetReservationByCodeAsync(string reservationCode)
        {
            return await db.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Tickets)
                    .ThenInclude(t => t.Seat)
                .Include(r => r.Tickets)
                    .ThenInclude(t => t.FoodItem)
                .Include(r => r.Tickets)
                    .ThenInclude(t => t.DrinkItem)
                .FirstOrDefaultAsync(r => r.ReservationCode == reservationCode);
        }
    }
}
