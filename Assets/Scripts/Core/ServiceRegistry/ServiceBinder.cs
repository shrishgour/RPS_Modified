using Core.Sequencer;
using Game.Requirements;
using UnityEngine;

namespace Core.Services
{
    public class ServiceBinder : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; }

        private void Awake()
        {
            ServiceRegistry.Bind<UserState>(new UserState());
            ServiceRegistry.Bind<SequenceService>(new SequenceService());
            ServiceRegistry.Bind<ProgressionService>(new ProgressionService());
            ServiceRegistry.Bind<RequirementService>(new RequirementService());

            IsInitialized = true;
        }

        private void Start()
        {
        }
    }
}