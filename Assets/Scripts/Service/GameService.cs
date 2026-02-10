using System;
using DefaultNamespace;
using DefaultNamespace.Event;
using VContainer;
using VContainer.Unity;

namespace Service
{
    public class GameService : IStartable, IDisposable
    {
        private readonly IGameStateController _gameStateControllerController;

        [Inject]
        public GameService(IGameStateController gameStateControllerController )
        {
            _gameStateControllerController = gameStateControllerController;
        }

        public void Start()
        {
            EventBus<BirdDead>.Subscribe(OnBirdDeadEvent);
            _gameStateControllerController.SetState(GameStateType.GameStart);
        }

        private void OnBirdDeadEvent(BirdDead obj)
        {
            GameOver();
        }

        public void Dispose()
        {
            EventBus<BirdDead>.Unsubscribe(OnBirdDeadEvent);
        }

        private void GameOver()
        {
            _gameStateControllerController.SetState(GameStateType.GameEnd);
            _gameStateControllerController.Pause();
            
        }

     
    }
}