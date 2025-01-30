using System;
using XNode;

namespace Core.Sequencer
{
    public interface IChoiceOption
    {
        int targetNodeIndex { set; get; }
    }

    public interface ICommandNode
    {
        Command GetCommand();
        Type GetCommandType();

        IChoiceOption[] GetOptions();
    }

    public abstract class BaseCommandNode<T> : Node, ICommandNode where T : Command
    {
        [Input()]
        public int previousNode;

        public T value;

        public Command GetCommand()
        {
            return value;
        }

        public Type GetCommandType()
        {
            return typeof(T);
        }

        public virtual IChoiceOption[] GetOptions()
        {
            return new IChoiceOption[0];
        }
    }
}