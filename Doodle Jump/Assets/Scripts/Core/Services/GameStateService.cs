using Zenject;

namespace DoodleJump.Core.Services
{
    public class GameStateService : IGameStateService
    {
        private GameState _currentState = GameState.Menu;
        private readonly SignalBus _signalBus;

        public GameState CurrentState => _currentState;

        [Inject]
        public GameStateService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SetState(GameState state)
        {
            if (_currentState == state)
                return;

            _currentState = state;

            switch (state)
            {
                case GameState.Playing:
                    _signalBus.Fire<Signals.GameStartedSignal>();
                    break;
                case GameState.GameOver:
                    _signalBus.Fire<Signals.GameOverSignal>();
                    break;
            }
        }
    }
}
