using System;
using System.Collections;
using Core.Events;
using Core.StateMachine;
using Core.UI;
using Game.Events;
using Game.UI;

namespace Game.StateMachine
{
    public class InputState : State<RPSStateManager>
    {
        public InputState(RPSStateManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            UiManager.Instance.OpenDialog<GameplayDialog>(nameof(GameplayDialog), false, (dialog) => { });
            EventManager.Instance.AddListener<PlayerHandChosenEvent>(OnPlayerHandChosen);
            EventManager.Instance.AddListener<GameTimeoutEvent>(OnGameTimeoutEvent);

            stateMachine.Bot.ChooseHand();
            return base.Init();
        }

        private void OnPlayerHandChosen(PlayerHandChosenEvent e)
        {
            stateMachine.ChangeState(new EvaluationState(stateMachine));
        }

        private void OnGameTimeoutEvent(GameTimeoutEvent e)
        {
            stateMachine.ChangeState(new EvaluationState(stateMachine));
        }

        public override IEnumerator Exit(float delay, Action onExit)
        {
            EventManager.Instance.RemoveListener<PlayerHandChosenEvent>(OnPlayerHandChosen);
            EventManager.Instance.RemoveListener<GameTimeoutEvent>(OnGameTimeoutEvent);

            return base.Exit(delay, onExit);
        }
    }
}