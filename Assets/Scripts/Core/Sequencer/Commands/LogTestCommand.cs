using UnityEngine;
namespace Core.Sequencer.Commands
{
    [System.Serializable]
    public class LogTestCommand : Command
    {
        public int exInt;
        public float exFloat;
        public string exString;

        public override bool IsCommandDone { get; protected set; }
        public override bool IsSkippable { get; protected set; }

        public override void Start()
        {
            Debug.Log("1 - " + exInt);
            Debug.Log("2 - " + exFloat);
            Debug.Log("3 - " + exString);
            IsCommandDone = true;
        }

        protected override void OnSkip()
        {
        }
    }
}
