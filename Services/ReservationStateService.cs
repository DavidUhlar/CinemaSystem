using CinemaSystem.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CinemaSystem.Services
{
    public class ReservationStateService
    {
        private readonly ProtectedSessionStorage storage;
        private const string storageKey = "ReservationState";
        private const string storageKeyHistory = "ReservationHistory";

        public ReservationStateService(ProtectedSessionStorage storage)
        {
            this.storage = storage;
        }

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
