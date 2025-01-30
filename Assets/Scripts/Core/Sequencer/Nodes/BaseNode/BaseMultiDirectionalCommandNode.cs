namespace Core.Sequencer
{
    public abstract class BaseMultiDirectionalCommandNode<T> : BaseCommandNode<T> where T : Command
    {
        [Output(connectionType = ConnectionType.Override, dynamicPortList = true)]
        public ChoiceOption[] options;

        public override IChoiceOption[] GetOptions()
        {
            return options;
        }
    }

    [System.Serializable]
    public class ChoiceOption : IChoiceOption
    {
        public string text;
        public int targetNodeIndex { get; set; }

    }
}