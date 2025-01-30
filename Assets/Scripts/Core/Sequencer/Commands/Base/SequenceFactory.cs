using System.Collections.Generic;

namespace Core.Sequencer
{


    public interface ISequenceFactory
    {
        Sequence CreateSequence(SequenceData sequenceData);
    }

    public class SequenceFactory : ISequenceFactory
    {
        private ICommandFactory commandFactory;

        public SequenceFactory(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public Sequence CreateSequence(SequenceData sequenceData)
        {
            List<Command> commandList = new List<Command>();
            foreach (var command in sequenceData.commandDataList)
            {
                commandList.Add(commandFactory.CreateCommand(command));
            }

            return new Sequence(sequenceData.id, commandList);
        }
    }
}
