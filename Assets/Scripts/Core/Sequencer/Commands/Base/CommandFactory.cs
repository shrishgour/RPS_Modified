namespace Core.Sequencer
{
    public interface ICommandFactory
    {
        Command CreateCommand(CommandData commandData);
    }

    public class CommandFactory : ICommandFactory
    {
        public Command CreateCommand(CommandData commandData)
        {
            return commandData.GetCommand();
        }
    }
}
