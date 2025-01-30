using Core.Services;

public class ProgressionService : BaseService
{
    private ProgressionManager progressionManager;
    public override void Initialize()
    {
        base.Initialize();
        progressionManager = new ProgressionManager();
    }

    public void StartChapter(string chapterID)
    {
        progressionManager.StartChapter(chapterID);
    }

    public void MarkQuestComplete(string questID)
    {
        progressionManager.MarkQuestComplete(questID);
    }
}
