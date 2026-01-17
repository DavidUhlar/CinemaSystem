using CinemaSystem.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CinemaSystem.Services
{
    public class ReservationStateService(ProtectedSessionStorage storage)
    {
        private readonly ProtectedSessionStorage storage = storage;
        private const string storageKey = "ReservationState";

        public async Task<ReservationState> LoadAsync()
        {
            var result = await storage.GetAsync<ReservationState>(storageKey);
            return result.Success && result.Value != null
                ? result.Value
                : new ReservationState();
        }


        public async Task SaveAsync(ReservationState state)
        {
            await storage.SetAsync(storageKey, state);
        }

        public async Task ClearAsync()
        {
            await storage.DeleteAsync(storageKey);
        }
    }
}
