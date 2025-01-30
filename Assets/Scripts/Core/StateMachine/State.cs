using System;
using System.Collections;
using UnityEngine;

namespace Core.StateMachine
{
    public abstract class State<T> where T : StateMachine<T>
    {
        public State(T stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected T stateMachine;

        public virtual IEnumerator Init()
        {
            yield break;
        }

        public virtual IEnumerator Exit(float delay, Action onExit)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
                onExit?.Invoke();
            }
            else
            {
                onExit?.Invoke();
                yield break;
            }
        }
    }
}