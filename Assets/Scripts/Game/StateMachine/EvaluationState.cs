using System.Collections;
using Core.Config;
using Core.Events;
using Core.StateMachine;
using Game.Events;
using UnityEngine;

namespace Game.StateMachine
{
    public class EvaluationState : State<RPSStateManager>
    {
        public EvaluationState(RPSStateManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            var handsData = ConfigRegistry.GetConfig<Hands>();
            var playerHand = stateMachine.Player.ChosenHand;
            var botHand = stateMachine.Bot.ChosenHand;
            string resultText;
            string actText;
            ResultState resultState;

            if (string.IsNullOrEmpty(playerHand))
            {
                resultText = "Bot Wins!!";
                actText = "Next time pick a hand!!";
                resultState = ResultState.BotWon;
                Debug.Log("Bot Won!!");
            }
            else if (playerHand.Equals(botHand))
            {
                resultText = "Its a tie!!";
                actText = "Monkey see monkey do!!";
                resultState = ResultState.Tie;
                Debug.Log("Its a tie!!");
            }
            else if (handsData.Data[playerHand].winsList.Find(x => x.winsAgainst == botHand) != null)
            {
                resultText = "You win!!";
                actText = $"{playerHand} {handsData.Data[playerHand].winsList.Find(x => x.winsAgainst == botHand).displayString} {botHand}";
                resultState = ResultState.PlayerWon;
                Debug.Log("Player Won!!");
            }
            else
            {
                resultText = "Bot Wins!!";
                actText = $"{botHand} {handsData.Data[botHand].winsList.Find(x => x.winsAgainst == playerHand).displayString} {playerHand}";
                resultState = ResultState.BotWon;
                Debug.Log("Bot Won!!");
            }

            EventManager.Instance.TriggerEvent<GameResultEvent>(new GameResultEvent(playerHand, botHand, actText, resultText, resultState));

            stateMachine.ChangeState(new EndState(stateMachine));
            return base.Init();
        }
    }
}