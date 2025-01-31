using System;
using System.Collections.Generic;

namespace Core.Events
{
    public class EventManager
    {
        private static EventManager eventManager;

        public delegate void EventDelegate<T>(T e) where T : GameEvent;

        private delegate void EventDelegate(GameEvent e);

        private Dictionary<Type, EventDelegate> delegates = new();
        private Dictionary<Delegate, EventDelegate> delegateLookUp = new();

        public static EventManager Instance
        {
            get
            {
                if (eventManager == null)
                {
                    eventManager = new EventManager();
                }

                return eventManager;
            }
        }

        public void AddListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            AddDelegate(del);
        }

        public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            RemoveDelegate(del);
        }

        public void TriggerEvent<T>(T e) where T : GameEvent
        {
            if (delegates.TryGetValue(typeof(T), out var eventDelegates))
            {
                eventDelegates?.Invoke(e);
            }
        }

        public bool HasListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            return delegateLookUp.ContainsKey(del);
        }

        private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : GameEvent
        {
            if (delegateLookUp.ContainsKey(del))
            {
                return null;
            }

            //Converting to non generic delegate
            EventDelegate nonGenericDelegate = val => del((T)val);
            delegateLookUp[del] = nonGenericDelegate;

            EventDelegate currDelegates = null;
            if (delegates.TryGetValue(typeof(T), out currDelegates))
            {
                //delegates[typeof(T)] = currDelegates;
            }

            delegates[typeof(T)] = currDelegates + nonGenericDelegate;

            return nonGenericDelegate;
        }

        private void RemoveDelegate<T>(EventDelegate<T> del) where T : GameEvent
        {
            if (delegateLookUp.TryGetValue(del, out var nonGenericDelegate))
            {
                if (delegates.TryGetValue(typeof(T), out var tempDelegates))
                {
                    tempDelegates -= nonGenericDelegate;
                    if (tempDelegates == null)
                    {
                        delegates.Remove(typeof(T));
                    }
                    else
                    {
                        delegates[typeof(T)] = tempDelegates;
                    }
                }

                delegateLookUp.Remove(del);
            }
        }
    }
}