using DoodleJump.Core.Signals;
using Zenject;

namespace DoodleJump.Core.Services
{
    public class GameStateService : IGameStateService
    {
        private readonly SignalBus _signalBus;

        [Inject]
        public GameStateService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public GameState CurrentState { get; private set; } = GameState.Menu;

        public void SetState(GameState state)
        {
            if (CurrentState == state)
                return;

            CurrentState = state;

            switch (state)
            {
                case GameState.Playing:
                    _signalBus.Fire<GameStartedSignal>();
                    break;
                case GameState.GameOver:
                    _signalBus.Fire<GameOverSignal>();
                    break;
            }
        }
    }
}