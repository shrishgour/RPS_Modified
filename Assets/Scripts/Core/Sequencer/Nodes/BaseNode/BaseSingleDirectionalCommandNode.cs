namespace Core.Sequencer
{
    public abstract class BaseSingleDirectionalCommandNode<T> : BaseCommandNode<T> where T : Command
    {
        [Output(connectionType = ConnectionType.Override)]
        public int nextNode;
    }
}