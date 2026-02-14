using System;
using DefaultNamespace;
using Event.Score;
using Service;
using VContainer;
using VContainer.Unity;

public class ScorePersistenceHandler : IStartable,IDisposable
{
    private const string HIGH_SCORE_KEY = "high_score";

    ISaveLoadService _saveLoadService;
    
    [Inject]
    public ScorePersistenceHandler(
        ISaveLoadService saveLoadService,
        IScoreService scoreService)
    {
        _saveLoadService = saveLoadService;
        int savedHighScore = saveLoadService.Load<int>(HIGH_SCORE_KEY, 0);
        scoreService.Init(savedHighScore);

        EventBus<HighScoreChanged>.Subscribe(OnHighScoreChanged);
    }

    public void Dispose()
    {
        EventBus<HighScoreChanged>.Unsubscribe(OnHighScoreChanged);
    }

    private void OnHighScoreChanged(HighScoreChanged highScoreData)
    {
        _saveLoadService.Save<int>(HIGH_SCORE_KEY, highScoreData.HighScore);
    }

    public void Start()
    {
    }
}