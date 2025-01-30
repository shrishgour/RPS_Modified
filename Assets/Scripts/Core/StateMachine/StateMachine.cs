using UnityEngine;

namespace Core.StateMachine
{
    public abstract class StateMachine<T> : MonoBehaviour where T : StateMachine<T>
    {
        protected State<T> currentState;

        public State<T> GetCurrentState()
        {
            return currentState;
        }

        /// <param name="state"></param>
        /// <param name="exitDelay">defaults to -1, in case of special claus of having delay between transition of states this delay can be used.</param>
        public virtual void ChangeState(State<T> state, float exitDelay = -1f)
        {
            if (currentState != null)
            {
                StartCoroutine(currentState.Exit(exitDelay, () =>
                {
                    if (exitDelay >= 0f)
                    {
                        currentState = state;
                        StartCoroutine(currentState.Init());
                    }
                }));
            }

            if (currentState == null || exitDelay < 0f)
            {
                currentState = state;
                StartCoroutine(currentState.Init());
            }
        }
    }
}