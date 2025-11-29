using CinemaSystem.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CinemaSystem.Services.DesignPatterns.Command
{
    public class UpdateReservationStateCommand : ICommandAsync
    {
        private readonly ProtectedSessionStorage storage;
        private readonly ReservationState newState;

        public UpdateReservationStateCommand(ProtectedSessionStorage storage, ReservationState newState)
        {
            this.storage = storage;
            this.newState = newState;
        }
        public async Task Execute()
        {
            await storage.SetAsync("ReservationState", newState);
        }
    }
}
