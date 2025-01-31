using UnityEngine;

namespace Core.UI
{
    public class HandButton : UIButton
    {
        [SerializeField] private string handName;

        public string HandName => handName;
    }
}