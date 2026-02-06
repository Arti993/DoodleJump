using System;
using Zenject;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;

namespace DoodleJump.Core.Bootstrap
{
    public class GameBootstrap : IInitializable, IDisposable
    {
        private readonly IGameStateService _gameStateService;
        private readonly SignalBus _signalBus;

        [Inject]
        public GameBootstrap(IGameStateService gameStateService, SignalBus signalBus)
        {
            _gameStateService = gameStateService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
            _gameStateService.SetState(GameState.Playing);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _gameStateService.SetState(GameState.GameOver);
        }
    }
}
