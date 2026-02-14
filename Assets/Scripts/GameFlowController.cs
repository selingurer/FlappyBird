using System;
using System.Collections.Generic;
using DefaultNamespace;
using Event;
using Event.ButtonClick;
using Service;
using VContainer;
using VContainer.Unity;

public class GameFlowController : IInitializable, IDisposable
{
    private readonly IEnumerable<IResettable> _resettables;
    private readonly IGameStateService _gameStateService;
    
    [Inject]
    public GameFlowController(IGameStateService gameStateService, IEnumerable<IResettable> resettables)
    {
        _gameStateService = gameStateService;
        _resettables = resettables;
    }

    public void Initialize()
    {
        EventBus<StartGameButtonClicked>.Subscribe(OnStartGameClicked);
        EventBus<BirdStateChanged>.Subscribe(OnBirdStateChanged);
        EventBus<PlayAgainButtonClicked>.Subscribe(OnGamePlayAgainButtonClicked);
    }

    private void OnGamePlayAgainButtonClicked(PlayAgainButtonClicked obj)
    {
        _gameStateService.SetState(GameStateType.GameStart);

        foreach (var resettable in _resettables)
        {
            resettable.Reset();
        }
    }

    private void OnBirdStateChanged(BirdStateChanged stateData)
    {
        if (stateData.BirdState == BirdState.Dead)
        {
            _gameStateService.SetState(GameStateType.GameEnd);
        }
    }

    private void OnStartGameClicked(StartGameButtonClicked obj)
    {
        _gameStateService.SetState(GameStateType.GamePlaying);
    }

    public void Dispose()
    {
        EventBus<StartGameButtonClicked>.Unsubscribe(OnStartGameClicked);
        EventBus<BirdStateChanged>.Unsubscribe(OnBirdStateChanged);
        EventBus<PlayAgainButtonClicked>.Unsubscribe(OnGamePlayAgainButtonClicked);
    }
}