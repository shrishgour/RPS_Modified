using Core.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Sequencer
{
    [CreateAssetMenu(fileName = "SequenceDataContainer", menuName = "Sequencer/Sequence Data Container", order = 1)]
    public class SequenceDataContainer : BaseMultiConfig<SequenceData, SequenceDataContainer>
    {

    }

    [Serializable]
    public class SequenceData : IConfigData
    {
        public string id;
        public List<CommandData> commandDataList = new List<CommandData>();

        public string ID => id;
    }

    [Serializable]
    public class CommandData
    {
        public string id;
        public string commandType;
        public string commandData;

        public Command GetCommand()
        {
            System.Type type = Type.GetType(commandType);

            return (Command)JsonConvert.DeserializeObject(commandData, type);
        }
    }
}
