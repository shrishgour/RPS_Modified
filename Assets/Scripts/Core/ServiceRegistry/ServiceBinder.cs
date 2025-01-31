using UnityEngine;

namespace Core.Services
{
    public class ServiceBinder : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; }

        private void Awake()
        {
            ServiceRegistry.Bind<UserState>(new UserState());

            IsInitialized = true;
        }

        private void Start()
        {
        }
    }
}