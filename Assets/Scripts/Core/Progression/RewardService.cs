using Core.Services;
using System.Collections.Generic;

namespace Game.Rewards
{
    public class RewardService : BaseService
    {
        public bool GrantReward(RewardBundle rewardBundle)
        {

            return true;
        }
    }

    public enum RewardType
    {
        Coins,
        Gems
    }

    [System.Serializable]
    public class RewardBundle
    {
        public List<Reward> rewardList;
        public bool GrantReward => ServiceRegistry.Get<RewardService>().GrantReward(this);
    }

    [System.Serializable]
    public class Reward
    {
        public RewardType rewardType;
        public int value;
        public string metaData;
    }
}
