using Core.Events;
using Core.Services;
using Core.UI;
using DG.Tweening;
using Game.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuDialog : BaseDialog
    {
        [SerializeField] private UIButton playButton;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private Image playBorder;

        private Sequence sequence;

        public override void OnDialogOpen()
        {
            base.OnDialogOpen();

            sequence.Kill();
            playButton.AddPressedListener(() =>
            {
                EventManager.Instance.TriggerEvent<PlayPressEvent>(new PlayPressEvent());
            });
            highScoreText.SetText("HighScore : " + ServiceRegistry.Get<UserState>().HighScoreValue.ToString());
        }

        public override void OnDialogShown()
        {
            AnimatePlayButton();
        }

        private void AnimatePlayButton()
        {
            playBorder.transform.localScale = Vector3.one;
            playBorder.color = new Color(1, 1, 1, 0.2f);

            sequence = DOTween.Sequence();
            sequence.Append(playButton.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f, 10, 0.1f));
            sequence.Append(playBorder.transform.DOScale(Vector3.one * 3, 1.5f).SetEase(Ease.OutCirc));
            sequence.Join(playBorder.DOFade(0, 0.8f));
            sequence.OnComplete(() => AnimatePlayButton());
        }
    }
}