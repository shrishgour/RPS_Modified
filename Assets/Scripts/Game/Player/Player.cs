using Core.Events;
using Game.Events;
using UnityEngine;

namespace Game.Character
{
    public class Player : BasePlayer
    {
        public Player()
        {
            EventManager.Instance.RemoveListener<PlayerHandChosenEvent>(OnPlayerHandChosen);
            EventManager.Instance.AddListener<PlayerHandChosenEvent>(OnPlayerHandChosen);
        }

        private void OnPlayerHandChosen(PlayerHandChosenEvent e)
        {
            chosenHand = e.chosenHand;
            Debug.Log("Player Chosen hand = " + chosenHand);
        }
    }
}