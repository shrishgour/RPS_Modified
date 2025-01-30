using System;
using TMPro;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class UIButton : BaseButton, IPointerDownHandler, IPointerUpHandler
    {
        protected event Action downEvent;
        protected event Action upEvent;

        public virtual void OnDown()
        {
            downEvent?.Invoke();
        }
        public virtual void OnUp()
        {
            upEvent?.Invoke();
        }

        public virtual void SetText(string text)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
        }

        public virtual void AddDownListener(Action handler)
        {
            downEvent += handler;
        }

        public virtual void RemoveDownListener(Action handler)
        {
            downEvent -= handler;
        }
        public virtual void AddUpListener(Action handler)
        {
            upEvent += handler;
        }

        public virtual void RemoveUpListener(Action handler)
        {
            upEvent -= handler;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isEnabled)
            {
                OnDown();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isEnabled)
            {
                OnUp();
            }
        }
    }
}
