namespace CinemaSystem.Services.DesignPatterns.Command
{
    public class ReservationInvoker
    {
        private readonly Stack<ICommand> executedCommands = new Stack<ICommand>();
        
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            executedCommands.Push(command);
        }
        public void UndoCommand()
        {
            if (executedCommands.Count > 0)
            {
                var command = executedCommands.Pop();
                command.Undo();
            }
        }

        public void CanUndo()
        {
            if (executedCommands.Count == 0)
            {
                throw new InvalidOperationException("No commands to undo.");
            }
        }
    }
}
