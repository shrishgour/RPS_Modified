using Core.Services;

namespace Game.Requirements
{
    public class RequirementService : BaseService
    {
        private UserState userState;

        public bool ValidateRequirement(Requirement requirement)
        {
            userState = ServiceRegistry.Get<UserState>();
            bool result = false;

            switch (requirement.requirementType)
            {
                case RequirementType.Chapter:
                    if (userState.ProgressionState.completedChapterList.Contains(requirement.metaData))
                    {
                        result = true;
                    }
                    break;
                case RequirementType.Quest:
                    if (userState.ProgressionState.activeChapterData.completedQuestList.Contains(requirement.metaData))
                    {
                        result = true;
                    }
                    break;
                case RequirementType.Coins:
                    result = true;
                    break;
                default:
                    break;
            }

            return result;
        }
    }

    public enum RequirementType
    {
        Chapter,
        Quest,
        Coins
    }

    [System.Serializable]
    public class Requirement
    {
        public RequirementType requirementType;
        public string value;
        public string metaData;

        public bool IsComplete => ServiceRegistry.Get<RequirementService>().ValidateRequirement(this);
    }
}
