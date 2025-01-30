using Game.Requirements;
using System.Collections.Generic;

namespace Core.Config
{
    public class ProgressionConfig : BaseMultiConfig<ChapterData, ProgressionConfig>
    {

    }

    [System.Serializable]
    public class ChapterData : IConfigData
    {
        public string ID => chapterID;
        public string chapterID;
        public List<Requirement> startRequirementList;
        public string startSequenceID;
        public string endSequenceID;
        public string completionReward;
        public List<QuestData> questList;
    }

    [System.Serializable]
    public class QuestData
    {
        public string questID;
        public string questTitle;
        public List<Requirement> startRequirementList;
        public Requirement price;
        public string completionReward;
    }
}