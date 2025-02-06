using Core.Config;
using Core.Events;
using Core.UI;
using DG.Tweening;
using Game.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndDialog : BaseDialog
{
    [SerializeField] private Image botHand;
    [SerializeField] private Image playerHand;
    [SerializeField] private GameObject separator;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private UIButton homeButton;
    [SerializeField] private RectTransform target;

    private Transform winnerTransform;
    private HandIcons handIcons;

    public override void OnDialogOpen()
    {
        base.OnDialogOpen();

        homeButton.AddPressedListener(() => EventManager.Instance.TriggerEvent<HomePressEvent>(new HomePressEvent()));
    }

    public override void OnDialogShown()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(playerHand.transform.parent.GetComponent<RectTransform>()
            .DOPunchPosition(playerHand.transform.position + Vector3.up * 400f, 1f, 3, 0.2f));
        sequence.Join(botHand.transform.parent.GetComponent<RectTransform>()
            .DOPunchPosition(botHand.transform.position + Vector3.down * 400f, 1f, 3, 0.2f));
        sequence.AppendCallback(() => separator.transform.localScale = new Vector3(1, 0, 1));
        sequence.AppendCallback(() => separator.SetActive(true));
        sequence.Append(separator.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutElastic));
        sequence.AppendCallback(() =>
        {
            if (winnerTransform != null)
            {
                winnerTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutElastic);
            }
        });
        sequence.AppendCallback(() => homeButton.gameObject.SetActive(true));
    }

    public void InitDialog(GameResultEvent e)
    {
        handIcons = ConfigRegistry.GetConfig<HandIcons>();
        ResetElements();

        if (string.IsNullOrEmpty(e.playerHand))
        {
            playerHand.gameObject.SetActive(false);
        }
        else
        {
            playerHand.gameObject.SetActive(true);
            playerHand.sprite = handIcons.Data[e.playerHand].icon;
        }

        botHand.sprite = handIcons.Data[e.botHand].icon;
        resultText.SetText(e.resultText);
        actText.SetText(e.actText);

        if (e.resultState == ResultState.BotWon)
        {
            homeButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Home");
            resultText.color = Color.red;
            winnerTransform = botHand.transform.parent;
        }
        else if (e.resultState == ResultState.PlayerWon)
        {
            homeButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Next Round");
            resultText.color = Color.yellow;
            winnerTransform = playerHand.transform.parent;
        }
        else
        {
            homeButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Next Round");
            resultText.color = Color.white;
            winnerTransform = null;
        }
    }

    private void ResetElements()
    {
        separator.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        playerHand.transform.parent.localScale = Vector3.one;
        botHand.transform.parent.localScale = Vector3.one;
    }

    public override void OnDialogClosed()
    {
        homeButton.RemoveAllPressedListeners();
    }
}