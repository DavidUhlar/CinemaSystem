namespace CinemaSystem.Services
{
    public class CounterState
    {
        public int Count { get; private set; }

        public bool Jozo => Count >= 10;

        public event Action? OnChange;

        public void Increment()
        {
            Count++;
            NotifyStateChanged();
        }

        public void Reset()
        {
            Count = 0;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
