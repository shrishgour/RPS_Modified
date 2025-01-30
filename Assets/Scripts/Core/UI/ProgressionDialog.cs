using Core.Config;
using Core.Services;
using Core.UI;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionDialog : BaseDialog
{
    public const string DialogID = nameof(ProgressionDialog);

    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questHolder;

    private ProgressionConfig progressionConfig;
    private Dictionary<string, GameObject> populatedQuestMap;

    public override void OnDialogOpen()
    {
        if (progressionConfig == null)
        {
            progressionConfig = ConfigRegistry.GetConfig<ProgressionConfig>();
        }

        populatedQuestMap ??= new Dictionary<string, GameObject>();
        Invoke("RemoveCompletedQuests", 1);
        Invoke("PopulateAvaliableQuests", 1.2f);
    }

    private void PopulateAvaliableQuests()
    {
        var activeChapterData = ServiceRegistry.Get<UserState>().ProgressionState.activeChapterData;

        foreach (var questID in activeChapterData.activeQuestList)
        {
            var chapterData = progressionConfig.Data[activeChapterData.chapterID];
            var questData = chapterData.questList.Find(x => x.questID == questID);

            if (questData != null && !populatedQuestMap.ContainsKey(questID))
            {
                var questObject = Instantiate(questPrefab, questHolder);
                questObject.GetComponent<Quest>().Initialize(questData);
                populatedQuestMap.Add(questID, questObject);
            }
        }
    }

    private void RemoveCompletedQuests()
    {
        var activeChapterData = ServiceRegistry.Get<UserState>().ProgressionState.activeChapterData;

        foreach (var questID in activeChapterData.completedQuestList)
        {
            if (populatedQuestMap.ContainsKey(questID))
            {
                var questObject = populatedQuestMap[questID];
                populatedQuestMap.Remove(questID);
                Destroy(questObject);
            }
        }
    }
}
