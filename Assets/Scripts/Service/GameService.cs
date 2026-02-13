using VContainer;
using VContainer.Unity;

namespace Service
{
    public class GameService : IStartable
    {
        private readonly IGameStateService _gameStateService;

        [Inject]
        public GameService(IGameStateService gameStateService )
        {
            _gameStateService = gameStateService;
        }

        public void Start()
        {
            _gameStateService.SetState(GameStateType.GameStart);
        }
        
    }
}