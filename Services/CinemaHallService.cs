using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaSystem.Services
{
    public class CinemaHallService(CinemaDbContext db)
    {
        private readonly CinemaDbContext db = db;

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

        public async Task<List<CinemaHall>> GetAllHallsAsync()
        {
            return await db.CinemaHalls.ToListAsync();
        }
    }
}
