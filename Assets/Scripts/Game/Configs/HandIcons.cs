using System;
using UnityEngine;

namespace Core.Config
{
    public class HandIcons : BaseMultiConfig<HandIconsData, HandIcons>
    {
    }

    [Serializable]
    public class HandIconsData : IConfigData
    {
        public string ID => handName;
        public string handName;
        public Sprite icon;
    }
}