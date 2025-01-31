using System;
using System.Collections;
using Core.Events;
using Core.StateMachine;
using Core.UI;
using Game.Events;

namespace Game.StateMachine
{
    public class EndState : State<RPSStateManager>
    {
        public EndState(RPSStateManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            UiManager.Instance.OpenDialog<EndDialog>(nameof(EndDialog), false,
                (dialog) => { ((EndDialog)dialog).InitDialog(stateMachine.GameResultData); });

            EventManager.Instance.AddListener<HomePressEvent>(OnHomePressed);

            if (stateMachine.GameResultData.resultState == ResultState.PlayerWon)
            {
                stateMachine.UpdateScore();
            }

            return base.Init();
        }

        private void OnHomePressed(HomePressEvent e)
        {
            if (stateMachine.GameResultData.resultState == ResultState.BotWon)
            {
                stateMachine.BeginGame();
            }
            else
            {
                stateMachine.ChangeState(new InputState(stateMachine));
            }
        }

        public override IEnumerator Exit(float delay, Action onExit)
        {
            EventManager.Instance.RemoveListener<HomePressEvent>(OnHomePressed);
            return base.Exit(delay, onExit);
        }
    }
}