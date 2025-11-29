namespace CinemaSystem.Services.DesignPatterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public interface ICommandAsync
    {
        Task Execute();
    }
}
