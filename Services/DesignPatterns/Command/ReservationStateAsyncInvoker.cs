using CinemaSystem.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CinemaSystem.Services.DesignPatterns.Command
{
    public class ReservationStateAsyncInvoker
    {
        private readonly ProtectedSessionStorage storage;
        private Stack<ReservationState> stateHistory = new();
        private const string ReservationStateKey = "ReservationState";
        private const string HistoryKey = "ReservationHistory";
        private const string TempStateKey = "TempReservationState";
        private const string TempHistoryKey = "TempReservationHistory";

        public ReservationStateAsyncInvoker(ProtectedSessionStorage storage)
        {
            this.storage = storage;
        }

        public async Task InitializeAsync()
        {
            var result = await storage.GetAsync<List<ReservationState>>(HistoryKey);
            if (result.Success && result.Value != null)
            {
                stateHistory = new Stack<ReservationState>(result.Value.AsEnumerable().Reverse());
            }
        }

        public async Task ExecuteCommand(ICommandAsync command)
        {
            

            var currentState = await GetCurrentStateAsync();
            if (currentState != null)
            {
                Console.WriteLine($"Pushing state to history: {currentState.CurrentStep}");
                stateHistory.Push(DeepClone(currentState));
                
            }
            await command.Execute();
            await SaveHistoryAsync();
        }

        public async Task Undo()
        {
            Console.WriteLine($"Attempting to undo. Current history count: {stateHistory.Count}");

            if (stateHistory.Count > 0)
            {
                var previousState = stateHistory.Pop();

                await storage.SetAsync(ReservationStateKey, previousState);

                await SaveHistoryAsync();

                Console.WriteLine($"Undo successful. New state: {previousState.CurrentStep}");
            }
            else
            {
                Console.WriteLine("Cannot undo - not enough history");
            }
        }

        public bool CanUndo => stateHistory.Count > 1;

        public async Task Clear()
        {
            stateHistory.Clear();
            await storage.DeleteAsync(HistoryKey);
        }

        private async Task<ReservationState?> GetCurrentStateAsync()
        {
            var result = await storage.GetAsync<ReservationState>(ReservationStateKey);
            return result.Success ? result.Value : null;
        }

        private async Task SaveHistoryAsync()
        {
            var historyList = stateHistory.Reverse().ToList();
            await storage.SetAsync(HistoryKey, historyList);
        }

        private ReservationState DeepClone(ReservationState state)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(state);
            return System.Text.Json.JsonSerializer.Deserialize<ReservationState>(json)!;
        }

        public async Task SaveTempAsync()
        {
            var currentState = await GetCurrentStateAsync();
            if (currentState != null)
            {
                await storage.SetAsync(TempStateKey, DeepClone(currentState));
            }

            var historyList = stateHistory.Reverse().ToList();
            await storage.SetAsync(TempHistoryKey, historyList);
        }

        public async Task RestoreFromTempAsync()
        {
            var tempStateResult = await storage.GetAsync<ReservationState>(TempStateKey);
            if (tempStateResult.Success && tempStateResult.Value != null)
            {
                await storage.SetAsync(ReservationStateKey, tempStateResult.Value);
            }

            var tempHistoryResult = await storage.GetAsync<List<ReservationState>>(TempHistoryKey);
            if (tempHistoryResult.Success && tempHistoryResult.Value != null)
            {
                stateHistory = new Stack<ReservationState>(tempHistoryResult.Value.AsEnumerable().Reverse());
                await SaveHistoryAsync();
            }
        }

        public async Task ClearTempAsync()
        {
            await storage.DeleteAsync(TempStateKey);
            await storage.DeleteAsync(TempHistoryKey);
        }
    }
}
