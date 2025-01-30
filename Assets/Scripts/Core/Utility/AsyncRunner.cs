using Core.Singleton;
using System;
using System.Collections;
using UnityEngine;

namespace Core.AsyncRunner
{
    public class AsyncRunner : SerializedSingleton<AsyncRunner>
    {
        public Coroutine Run(IEnumerator enumerator)
        {
            Coroutine routine = StartCoroutine(enumerator);
            return routine;
        }

        public void Stop(IEnumerator enumerator)
        {
            StopCoroutine(enumerator);
        }

        public void Stop(Coroutine enumerator)
        {
            StopCoroutine(enumerator);
        }

        private IEnumerator WaitRoutine(YieldInstruction instruction, Action action)
        {
            yield return instruction;
            action.Invoke();
        }

        public IEnumerator AtEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action.Invoke();
        }

        public IEnumerator OnNextFrame(Action action)
        {
            yield return null;
            action.Invoke();
        }

        public IEnumerator AfterDelay(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
    }
}
