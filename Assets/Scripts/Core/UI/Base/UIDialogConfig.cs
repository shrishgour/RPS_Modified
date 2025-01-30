using UnityEngine;

namespace Core.Config
{
    public class UIDialogConfig : BaseMultiConfig<UIDialogConfigData, UIDialogConfig>
    {

    }

    [System.Serializable]
    public class UIDialogConfigData : IConfigData
    {
        public string ID => nameof(UIDialogConfigData);
        public string dialogID;
        public GameObject prefab;
    }
}