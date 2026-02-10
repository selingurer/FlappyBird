using DefaultNamespace;
using DG.Tweening;
using Event;
using UnityEngine;

public enum GameStateType
{
    GameStart,
    GamePlaying,
    GameEnd,
}

public class GameStateControllerController : IGameStateController
{
    public bool IsPaused { get; private set; }

    public GameStateType State { get; private set; }

    public void Pause()
    {
        if (IsPaused) return;

        IsPaused = true;

        Time.timeScale = 0f;
        DOTween.PauseAll();
    }

    public void Resume()
    {
        if (!IsPaused) return;

        IsPaused = false;

        Time.timeScale = 1f;
        DOTween.PlayAll();
    }

    public void SetState(GameStateType newState)
    {
        State = newState;
        
        EventBus<GameStateChanged>.Publish(new GameStateChanged
        {
            GameState = newState
        });
    }
}

public interface IGameStateController
{
    public void Pause();
    public void Resume();
    public void SetState(GameStateType newState);
}