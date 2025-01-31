using Core.Events;
using Core.UI;
using Game.Events;
using TMPro;
using UnityEngine;

public class EndDialog : BaseDialog
{
    [SerializeField] private TextMeshProUGUI botHandText;
    [SerializeField] private TextMeshProUGUI playerHandText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private UIButton homeButton;

    public override void OnDialogOpen()
    {
        homeButton.AddPressedListener(() => EventManager.Instance.TriggerEvent<HomePressEvent>(new HomePressEvent()));
    }

    public void InitDialog(GameResultEvent e)
    {
        botHandText.SetText(e.botHand);
        playerHandText.SetText(e.playerHand);
        resultText.SetText(e.resultText);
        actText.SetText(e.actText);

        if (e.resultState == ResultState.BotWon)
        {
            homeButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Home");
        }
        else
        {
            homeButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Next Round");
        }
    }

    public override void OnDialogClosed()
    {
        homeButton.RemoveAllPressedListeners();
    }
}