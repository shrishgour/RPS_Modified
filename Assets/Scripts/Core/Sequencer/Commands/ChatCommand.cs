namespace Core.Sequencer.Commands
{
    [System.Serializable]
    public class ChatCommand : Command
    {
        public override bool IsCommandDone { get; protected set; }
        public override bool IsSkippable { get; protected set; }

        public ChatData[] chatDatas;

        public override void Start()
        {

        }

        protected override void OnSkip()
        {

        }
    }

    [System.Serializable]
    public class ChatData
    {
        public string charID;
        public string emotion;
        public string chatString;
    }
}