using System;
using System.Collections.Generic;

namespace Core.Config
{
    public class Hands : BaseMultiConfig<HandsData, Hands>
    {
    }

    [Serializable]
    public class HandsData : IConfigData
    {
        public string ID => handName;
        public string handName;
        public List<HandWinData> winsList;
    }

    [Serializable]
    public class HandWinData
    {
        public string winsAgainst;
        public string displayString;
    }
}