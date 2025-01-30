using Core.Config;
using Core.Services;
using Core.UI;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private UIButton priceBtn;

    private QuestData questData;

    public void Initialize(QuestData questData)
    {
        this.questData = questData;
        questTitle.SetText(questData.questTitle);
        priceBtn.SetText(questData.price.metaData.ToString());
        priceBtn.AddPressedListener(OnPriceButtonPress);
    }

    private void OnDestroy()
    {
        priceBtn.RemoveAllPressedListeners();
    }

    private void OnPriceButtonPress()
    {
        if (questData.price.IsComplete)
        {
            ServiceRegistry.Get<ProgressionService>().MarkQuestComplete(questData.questID);
            UiManager.Instance.CloseDialog(ProgressionDialog.DialogID);
        }
    }
}
