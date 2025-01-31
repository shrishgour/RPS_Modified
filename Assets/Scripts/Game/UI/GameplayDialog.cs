using System.Collections;
using Core.Config;
using Core.Events;
using Core.UI;
using DG.Tweening;
using Game.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameplayDialog : BaseDialog
    {
        [SerializeField] private TextMeshProUGUI botText;
        [SerializeField] private TextMeshProUGUI playerText;
        [SerializeField] private HandButton rockButton;
        [SerializeField] private HandButton paperButton;
        [SerializeField] private HandButton scissorsButton;
        [SerializeField] private HandButton lizardButton;
        [SerializeField] private HandButton spockButton;
        [SerializeField] private float roundTime;
        [SerializeField] private Image timer;
        [SerializeField] private Transform[] botButtons;

        private Hands handsData;

        public override void OnDialogOpen()
        {
            handsData = ConfigRegistry.GetConfig<Hands>();
            ButtonInits();

            StartCoroutine(RunTimer());
            StartCoroutine(RunBotButtonAnimation());

            EventManager.Instance.AddListener<BotHandChosenEvent>(OnBotHandChosen);
        }

        private void ButtonInits()
        {
            rockButton.AddPressedListener(() => OnHandButtonPress(rockButton));
            paperButton.AddPressedListener(() => OnHandButtonPress(paperButton));
            scissorsButton.AddPressedListener(() => OnHandButtonPress(scissorsButton));
            lizardButton.AddPressedListener(() => OnHandButtonPress(lizardButton));
            spockButton.AddPressedListener(() => OnHandButtonPress(spockButton));
        }

        private void OnHandButtonPress(HandButton button)
        {
            var chosenHand = handsData.Data[button.HandName].handName;
            EventManager.Instance.TriggerEvent<PlayerHandChosenEvent>(new PlayerHandChosenEvent(chosenHand));
        }

        private void OnBotHandChosen(BotHandChosenEvent e)
        {
            //botText.SetText("Bot Has Chosen!!");
        }

        private IEnumerator RunTimer()
        {
            var currentTime = roundTime;
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timer.transform.DOScaleX(currentTime / roundTime, 0.01f);
                yield return null;
            }

            EventManager.Instance.TriggerEvent<GameTimeoutEvent>(new GameTimeoutEvent());
        }

        private IEnumerator RunBotButtonAnimation()
        {
            while (true)
            {
                var index = Random.Range(0, botButtons.Length);
                botButtons[index].DOPunchScale(Vector3.one, 0.2f);
                yield return new WaitForSeconds(1);
            }
        }

        public override void OnDialogClosed()
        {
            StopCoroutine(RunBotButtonAnimation());
            EventManager.Instance.RemoveListener<BotHandChosenEvent>(OnBotHandChosen);

            rockButton.RemoveAllPressedListeners();
            paperButton.RemoveAllPressedListeners();
            scissorsButton.RemoveAllPressedListeners();
            lizardButton.RemoveAllPressedListeners();
            spockButton.RemoveAllPressedListeners();
        }
    }
}