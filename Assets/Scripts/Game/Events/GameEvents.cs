using Core.Events;

namespace Game.Events
{
    public class PlayPressEvent : GameEvent
    {
    }

    public class HomePressEvent : GameEvent
    {
    }

    public class PlayerHandChosenEvent : GameEvent
    {
        public string chosenHand;

        public PlayerHandChosenEvent(string chosenHand)
        {
            this.chosenHand = chosenHand;
        }
    }

    public class BotHandChosenEvent : GameEvent
    {
        public string chosenHand;

        public BotHandChosenEvent(string chosenHand)
        {
            this.chosenHand = chosenHand;
        }
    }

    public class GameTimeoutEvent : GameEvent
    {
    }

    public class GameResultEvent : GameEvent
    {
        public string playerHand;
        public string botHand;
        public string actText;
        public string resultText;
        public ResultState resultState;

        public GameResultEvent(string playerHand, string botHand, string actText, string resultText, ResultState resultState)
        {
            this.playerHand = playerHand;
            this.botHand = botHand;
            this.actText = actText;
            this.resultText = resultText;
            this.resultState = resultState;
        }
    }
}