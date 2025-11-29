using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class EventService
    {
        private readonly CinemaDbContext db;

        public EventService(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await db.Events
                .Include(e => e.CinemaHall)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await db.Events
                .Include(e => e.CinemaHall)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Event>> GetUpcomingEventsAsync()
        {
            return await db.Events
                .Include(e => e.CinemaHall)
                .Where(e => e.StartTime > DateTime.Now)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            db.Events.Add(newEvent);
            await db.SaveChangesAsync();
            return newEvent;
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            db.Events.Update(updatedEvent);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToDelete = await db.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                db.Events.Remove(eventToDelete);
                await db.SaveChangesAsync();
            }
        }
    }
}
