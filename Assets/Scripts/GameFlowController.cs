using System;
using DefaultNamespace.Event;
using Event;
using Event.ButtonClick;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class GameFlowController : IInitializable, IDisposable
    {
        private readonly IGameStateService _gameStateService;

        [Inject]
        public GameFlowController(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            EventBus<StartGameButtonClicked>.Subscribe(OnStartGameClicked);
            EventBus<BirdDead>.Subscribe(OnBirdDead);
            EventBus<PlayAgainButtonClicked>.Subscribe(OnGamePlayAgainButtonClicked);
        }

        private void OnGamePlayAgainButtonClicked(PlayAgainButtonClicked obj)
        {
            _gameStateService.SetState(GameStateType.GameStart);
            EventBus<GameRestarted>.Publish(new GameRestarted());
        }

        private void OnBirdDead(BirdDead obj)
        {
            _gameStateService.SetState(GameStateType.GameEnd);

        }

        private void OnStartGameClicked(StartGameButtonClicked obj)
        {
            _gameStateService.SetState(GameStateType.GamePlaying);

        }

        public void Dispose()
        {
            EventBus<StartGameButtonClicked>.Unsubscribe(OnStartGameClicked);
            EventBus<BirdDead>.Unsubscribe(OnBirdDead);
            EventBus<PlayAgainButtonClicked>.Unsubscribe(OnGamePlayAgainButtonClicked);
        }
        
    }
}