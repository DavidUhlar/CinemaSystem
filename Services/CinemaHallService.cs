using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class CinemaHallService
    {
        private readonly CinemaDbContext db;

        public CinemaHallService(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<CinemaHall?> GetCinemaHallByIdAsync(int id)
        {
            return await db.CinemaHalls
                .Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<CinemaHall?> GetCinemaHallByEventIdAsync(int id)
        {
            var eventFromId = await db.Events
                .FirstOrDefaultAsync(e => e.Id == id);
            if (eventFromId != null)
            {
                return await GetCinemaHallByIdAsync(eventFromId.CinemaHallId);
            } else { 
                return null;
            }
        }


    }
}
