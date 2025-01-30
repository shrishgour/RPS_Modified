using System.Collections.Generic;

namespace Core.Sequencer
{
    public abstract class CommandWithOptions : Command, ICommandWithOption
    {
        public virtual List<ChoiceOption> Options { get; set; }
    }
}