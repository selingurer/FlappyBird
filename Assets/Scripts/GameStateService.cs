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

public class GameStateService : IGameStateService
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
        HandleStateSideEffects(State);
        
        EventBus<GameStateChanged>.Publish(new GameStateChanged
        {
            GameState = newState
        });
    }
    
    private void HandleStateSideEffects(GameStateType state)
    {
        switch (state)
        {
            case GameStateType.GamePlaying:
                Resume();
                break;

            case GameStateType.GameEnd:
                Pause();
                break;

            case GameStateType.GameStart:
                Pause();
                break;
        }
    }

}

public interface IGameStateService
{
    public void Pause();
    public void Resume();
    public void SetState(GameStateType newState);
}