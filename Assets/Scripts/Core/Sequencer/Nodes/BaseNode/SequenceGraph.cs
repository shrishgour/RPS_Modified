using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;

namespace Core.Sequencer
{
    [CreateAssetMenu(menuName = "Sequencer/Sequence Graph")]
    public class SequenceGraph : NodeGraph
    {
        private SequenceDataContainer dataContainer;
        private string sequenceID;

        public void ConvertGraphToSequence()
        {
#if UNITY_EDITOR
            SequenceNode rootNode = nodes.Find(n => n is SequenceNode) as SequenceNode;

            dataContainer = rootNode.dataContainer;
            sequenceID = rootNode.sequenceID;

            var sequenceData = dataContainer.data.Find(s => s.id == sequenceID);

            if (sequenceData == null)
            {
                sequenceData = new SequenceData();
                sequenceData.id = sequenceID;
                dataContainer.data.Add(sequenceData);
            }

            sequenceData.commandDataList = new List<CommandData>();

            foreach (Node node in nodes)
            {
                if (node is ICommandNode)
                {
                    ICommandNode commandNode = (ICommandNode)node;
                    Command command = commandNode.GetCommand();

                    Node nextNode = GetNextNode(node);
                    int index = nodes.FindIndex(n => n.Equals(nextNode));
                    command.nextNodeIndex = index < 0 ? index : index - 1;

                    IChoiceOption[] options = commandNode.GetOptions();
                    List<ChoiceOption> optionList = new List<ChoiceOption>();

                    if (options.Length > 0)
                    {
                        for (int i = 0; i < options.Length; i++)
                        {
                            Node nextOption = GetNextNode(commandNode as Node, i);
                            int optionIndex = nodes.IndexOf(nextOption);
                            optionIndex = optionIndex < 0 ? optionIndex : optionIndex - 1;
                            options[i].targetNodeIndex = optionIndex;
                            IChoiceOption option = options[i];
                            optionList.Add((ChoiceOption)option);
                        }

                        ((ICommandWithOption)command).Options = optionList;
                    }

                    int currentNodeIndex = nodes.IndexOf(node);
                    currentNodeIndex = currentNodeIndex < 0 ? currentNodeIndex : currentNodeIndex - 1;

                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    sequenceData.commandDataList.Add(
                        new CommandData
                        {
                            id = currentNodeIndex.ToString(),
                            commandType = commandNode.GetCommandType().ToString(),
                            commandData = JsonConvert.SerializeObject((command is ICommandWithOption) ? ((CommandWithOptions)command) : command, serializerSettings)
                        });
                }
            }

            EditorUtility.SetDirty(dataContainer);
            AssetDatabase.SaveAssets();
#endif
        }

        public Node GetNextNode(Node currentNode)
        {
            NodePort port = currentNode.GetOutputPort("nextNode");
            if (port != null && port.GetConnections().Count > 0)
            {
                return port.GetConnection(0).node;
            }

            return null;
        }

        public Node GetNextNode(Node currentNode, int index)
        {
            NodePort port = currentNode.GetOutputPort($"options {index}");
            if (port != null && port.GetConnections().Count > 0)
            {
                return port.GetConnection(0).node;
            }

            return null;
        }
    }
}