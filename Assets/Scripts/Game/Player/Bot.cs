using Core.Events;
using Game.Events;
using UnityEngine;

namespace Game.Character
{
    public class Bot : BasePlayer
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        public override void ChooseHand()
        {
            var randomInt = Random.Range(0, handsData.Data.Count);
            chosenHand = handsData.data[randomInt].handName;

            Debug.Log("Bot has chosen = " + chosenHand);
            EventManager.Instance.TriggerEvent<BotHandChosenEvent>(new BotHandChosenEvent(chosenHand));
        }
    }
}