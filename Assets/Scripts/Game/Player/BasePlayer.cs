using Core.Config;

namespace Game.Character
{
    public abstract class BasePlayer : IPlayer
    {
        protected Hands handsData;
        protected string chosenHand;

        public string ChosenHand => chosenHand;

        protected BasePlayer()
        {
            handsData = ConfigRegistry.GetConfig<Hands>();
            chosenHand = string.Empty;
        }

        public virtual void ChooseHand()
        {
        }
    }
}