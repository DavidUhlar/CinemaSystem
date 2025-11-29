using CinemaSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class SeatService
    {
        private readonly CinemaDbContext db;

        public SeatService(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> IsSeatOccupiedAsync(int seatId, int eventId)
        {
            return await db.Tickets
                .AnyAsync(t => t.SeatId == seatId && t.EventId == eventId);
        }

        public async Task<HashSet<int>> GetOccupiedSeatIdsAsync(int eventId)
        {
            var ids = await db.Tickets
                .Where(t => t.EventId == eventId)
                .Select(t => t.SeatId)
                .ToListAsync();

            return ids.ToHashSet();
        }
    }
}
