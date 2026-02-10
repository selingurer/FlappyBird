using System;
using DefaultNamespace;
using Event;
using VContainer;
using VContainer.Unity;

namespace ScriptableObjects.UI
{
    public class PanelController : IStartable, IDisposable
    {
        private readonly IUIService _uIService;
        private readonly UIPanelData _uIPanelData;

        [Inject]
        public PanelController(IUIService uiService, UIPanelData uiPanelData)
        {
            _uIService = uiService;
            _uIPanelData = uiPanelData;
        }

        public void Start()
        {
            EventBus<GameStateChanged>.Subscribe(OnGameStateChanged);
        }


        public void Dispose()
        {
            EventBus<GameStateChanged>.Unsubscribe(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChanged state)
        {
            _uIService.ClosedAllPanel();

            switch (state.GameState)
            {
                case GameStateType.GameStart:
                    _uIService.ShowPanel(_uIPanelData.GameStartPanel);
                    break;
                case GameStateType.GamePlaying:
                    break;
                case GameStateType.GameEnd:
                    _uIService.ShowPanel(_uIPanelData.GameEndPanel);
                    break;
            }
        }
    }
}