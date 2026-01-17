using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class EventService(CinemaDbContext db)
    {
        private readonly CinemaDbContext db = db;

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
            //var eventToDelete = await db.Events.FindAsync(id);
            //if (eventToDelete != null)
            //{
            //    db.Events.Remove(eventToDelete);
            //    await db.SaveChangesAsync();
            //}

            var eventToDelete = await db.Events
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventToDelete == null)
                return;

            var reservationIds = eventToDelete.Tickets
                .Select(t => t.ReservationId)
                .Distinct()
                .ToList();

            var reservations = await db.Reservations
                .Where(r => reservationIds.Contains(r.Id))
                .ToListAsync();

            db.Reservations.RemoveRange(reservations);
            db.Events.Remove(eventToDelete);
            await db.SaveChangesAsync();
        }

        public async Task<bool> IsHallAvailableAsync(int hallId, DateTime startTime, int durationMinutes, int? excludeEventId = null)
        {
            var endTime = startTime.AddMinutes(durationMinutes);

            var conflictingEvents = await db.Events
                .Where(e => e.CinemaHallId == hallId)
                .Where(e => excludeEventId == null || e.Id != excludeEventId)
                .ToListAsync();

            foreach (var existingEvent in conflictingEvents)
            {
                DateTime existingStart = existingEvent.StartTime;
                DateTime existingEnd;

                if (existingEvent is FilmShow film)
                {
                    existingEnd = existingStart.AddMinutes(film.LengthInMinutes);
                }
                else
                {
                    existingEnd = existingStart.AddMinutes(160);
                }

                if (startTime < existingEnd && endTime > existingStart)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<List<Event>> GetConflictingEventsAsync(int hallId, DateTime startTime, int durationMinutes)
        {
            var endTime = startTime.AddMinutes(durationMinutes);

            var allEvents = await db.Events
                .Include(e => e.CinemaHall)
                .Where(e => e.CinemaHallId == hallId)
                .ToListAsync();

            var conflicts = new List<Event>();

            foreach (var existingEvent in allEvents)
            {
                DateTime existingStart = existingEvent.StartTime;
                DateTime existingEnd;

                if (existingEvent is FilmShow film)
                {
                    existingEnd = existingStart.AddMinutes(film.LengthInMinutes);
                }
                else
                {
                    existingEnd = existingStart.AddMinutes(160);
                }

                if (startTime < existingEnd && endTime > existingStart)
                {
                    conflicts.Add(existingEvent);
                }
            }

            return conflicts;
        }
    }
}
