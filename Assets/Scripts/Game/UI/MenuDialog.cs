using Core.Events;
using Core.Services;
using Core.UI;
using Game.Events;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class MenuDialog : BaseDialog
    {
        [SerializeField] private UIButton playButton;
        [SerializeField] private TextMeshProUGUI highScoreText;

        public override void OnDialogOpen()
        {
            playButton.AddPressedListener(() => { EventManager.Instance.TriggerEvent<PlayPressEvent>(new PlayPressEvent()); });

            highScoreText.SetText("HighScore : " + ServiceRegistry.Get<UserState>().HighScoreValue.ToString());
        }

        public override void OnDialogClosed()
        {
            base.OnDialogClosed();
        }
    }
}