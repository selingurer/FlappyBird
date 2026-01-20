using System;
using DefaultNamespace.Event;
using UnityEngine;
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
            EventBus<OnTriggerEnterEvent>.Subscribe(OnTriggerEnterEventBird);
        }

        private void OnTriggerEnterEventBird(OnTriggerEnterEvent obj)
        {
            GameOver();
        }

        public void Dispose()
        {
            EventBus<OnTriggerEnterEvent>.Unsubscribe(OnTriggerEnterEventBird);
        }

        private void GameOver()
        {
            _gameState.Pause();
        }

     
    }
}