using System;
using System.Collections;
using Core.Events;
using Core.StateMachine;
using Core.UI;
using Game.Events;
using Game.UI;

namespace Game.StateMachine
{
    public class BeginState : State<RPSStateManager>
    {
        public BeginState(RPSStateManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            UiManager.Instance.OpenDialog<MenuDialog>(nameof(MenuDialog), false, (dialog) => { });
            EventManager.Instance.AddListener<PlayPressEvent>(OnPlayPressEvent);
            return base.Init();
        }

        private void OnPlayPressEvent(PlayPressEvent e)
        {
            stateMachine.ChangeState(new InputState(stateMachine));
        }

        public override IEnumerator Exit(float delay, Action onExit)
        {
            EventManager.Instance.RemoveListener<PlayPressEvent>(OnPlayPressEvent);
            return base.Exit(delay, onExit);
        }
    }
}