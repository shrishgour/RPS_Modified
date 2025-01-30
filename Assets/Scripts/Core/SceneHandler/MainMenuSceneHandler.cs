using Core.SceneHandler;
using Core.Services;

public class MainMenuSceneHandler : BaseSceneHandler
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        ServiceRegistry.Get<ProgressionService>().Initialize();
        ServiceRegistry.Get<ProgressionService>().StartChapter("FirstChapter");
    }
}
