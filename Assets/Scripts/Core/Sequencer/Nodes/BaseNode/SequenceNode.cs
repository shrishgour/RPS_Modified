using XNode;
namespace Core.Sequencer
{
    public class SequenceNode : Node
    {
        [Output(connectionType = ConnectionType.Override)]
        public int nextNode;

        public SequenceDataContainer dataContainer;
        public string sequenceID;
    }
}