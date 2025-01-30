using UnityEngine;

namespace Core.Singleton
{
    /// <summary>
    /// A version of Singleton for objects that can't just be constructed from scratch, they need
    /// information or child objects to be set up.
    /// Unlike Singleton, Instance can return null if the object doesn't yet exist
    /// </summary>
    public class SerializedSingleton<T> : MonoBehaviour where T : SerializedSingleton<T>
    {
        private static T _instance;

        public static bool IsInstantiated => _instance != null;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));
                    }

                    // UnityEngine objects count as equalling "null" when destroyed, but aren't *really* null,
                    // which means that the conditional operator won't work.
                    // Make "null" into actual null here to solve that issue.
                    return _instance == null ? null : _instance;
                }
            }
        }
    }
}