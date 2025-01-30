using Core.Data;
using System.Collections.Generic;

public class ProgressionUserState : StateHandler
{
    public List<string> completedChapterList;
    public ChapterStateData activeChapterData;

    public ProgressionUserState()
    {
        completedChapterList = new List<string>();
        activeChapterData = new ChapterStateData();
    }

    public override void Reset()
    {
        completedChapterList = new List<string>();
        activeChapterData = new ChapterStateData();
        base.Reset();
    }
}

[System.Serializable]
public class ChapterStateData
{
    public string chapterID;
    public List<string> activeQuestList;
    public List<string> completedQuestList;

    public ChapterStateData()
    {
        completedQuestList = new List<string>();
        activeQuestList = new List<string>();
    }
}
