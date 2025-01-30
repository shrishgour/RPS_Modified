using UnityEngine;

namespace Core.Sequencer.Commands
{
    [System.Serializable]
    public class LogTestChoiceCommand : CommandWithOptions
    {
        public override bool IsCommandDone { get; protected set; }
        public override bool IsSkippable { get; protected set; }

        public string something;

        public override void Start()
        {
            Debug.Log("something - " + something);
            Debug.Log("Option - " + Options[0].targetNodeIndex);
            Debug.Log("Option - " + Options[0].text);
        }

        protected override void OnSkip()
        {

        }
    }
}