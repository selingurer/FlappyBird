using System;
using DefaultNamespace.Event;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class GameService : IStartable, IDisposable
    {
        private readonly GameState _gameState;

        [Inject]
        public GameService(GameState gameState )
        {
            _gameState = gameState;
        }

        public void Start()
        {
            EventBus<BirdDead>.Subscribe(OnBirdDeadEvent);
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
            _gameState.Pause();
        }

     
    }
}