using System.Collections.Generic;
using UnityEngine;

namespace Core.Sequencer
{
    public abstract class Command
    {
        [HideInInspector]
        public int nextNodeIndex;
        public abstract void Start();
        protected abstract void OnSkip();

        public virtual void Skip()
        {
            if (IsSkippable)
            {
                OnSkip();
                IsCommandDone = true;
            }
            else
            {
                //do something
            }
        }

        public virtual void Update()
        {
        }

        public abstract bool IsCommandDone { protected set; get; }
        public abstract bool IsSkippable { protected set; get; }
    }

    public interface ICommandWithOption
    {
        List<ChoiceOption> Options { get; set; }
    }

}