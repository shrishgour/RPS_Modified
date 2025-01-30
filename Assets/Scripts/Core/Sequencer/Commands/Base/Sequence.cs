using System.Collections.Generic;
using UnityEngine;

namespace Core.Sequencer
{
    public class Sequence
    {
        private string id;
        private List<Command> commandList;
        private int currentCommandIndex;
        private bool skipSequence = false;

        public bool IsDone => currentCommandIndex <= -1;

        public int CurrentCommandIndex => currentCommandIndex;

        private bool NextCommandExists => currentCommandIndex >= 0;
        private Command CurrentCommand => (IsDone || currentCommandIndex < 0) ? null : commandList[currentCommandIndex];

        public Sequence(string id, List<Command> commandList)
        {
            this.id = id;
            this.commandList = commandList;
        }

        public void StartSequence(int startCommandIndex = 0)
        {
            currentCommandIndex = startCommandIndex;
            CurrentCommand.Start();
        }

        public void Update()
        {
            if (CurrentCommand != null)
            {
                if (!skipSequence)
                {
                    CurrentCommand.Update();
                }

                if (CurrentCommand.IsCommandDone)
                {
                    StartNextCommand();
                }
            }
        }

        public void StartNextCommand()
        {
            currentCommandIndex = CurrentCommand.nextNodeIndex;
            if (NextCommandExists)
            {
                CurrentCommand.Start();
                if (skipSequence)
                {
                    CurrentCommand.Skip();
                }
            }
            else
            {
                Debug.Log($"Sequence {id} is completed.");
            }
        }

        public void SkipSequence(bool status)
        {
            skipSequence = status;
            if (skipSequence)
            {
                CurrentCommand.Skip();
            }
        }

        public Command GetCommandAtIndex(int index)
        {
            return commandList[index];
        }

        public void SetNextCommandIndex(int index)
        {
            CurrentCommand.nextNodeIndex = index;
        }

        public void FinishSequence()
        {
            //do something on finish of the sequence
        }
    }
}
