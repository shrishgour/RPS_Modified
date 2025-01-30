using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    [RequireComponent(typeof(Button))]
    public class BaseButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected bool isEnabled = true;

        public Button button { get; protected set; }
        public RectTransform RectTransform { get; private set; }

        protected event Action pressedEvent;

        protected void Awake()
        {
            button = GetComponent<Button>();
            RectTransform = GetComponent<RectTransform>();

            if (button != null)
            {
                button.interactable = isEnabled;
                this.enabled = isEnabled;
            }

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public virtual void OnPressed()
        {
            pressedEvent?.Invoke();
        }

        public virtual void AddPressedListener(Action handler)
        {
            pressedEvent += handler;
        }

        public virtual void RemovePressedListener(Action handler)
        {
            pressedEvent -= handler;
        }

        public virtual void RemoveAllPressedListeners()
        {
            pressedEvent = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isEnabled)
            {
                OnPressed();
            }
        }

        public bool Interactable
        {
            get { return button != null && button.interactable; }
            set
            {
                if (button == null)
                {
                    button = GetComponent<Button>();
                }
                button.interactable = value;
                isEnabled = value;
                this.enabled = value;
            }
        }
    }
}
