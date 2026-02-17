using System;
using System.Collections.Generic;
using DoodleJump.Core.Services;
using Zenject;
using DoodleJump.Core.Signals;

namespace DoodleJump.Player
{
    public class PlayerScoreHandler : IInitializable, IDisposable
    {
        private readonly IScoreService _scoreService;
        private readonly SignalBus _signalBus;

        private readonly List<int> _usedPlatforms = new List<int>();

        [Inject]
        public PlayerScoreHandler(IScoreService scoreService, SignalBus signalBus)
        {
            _scoreService = scoreService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlatformLandedSignal>(OnPlatformLanded);
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlatformLandedSignal>(OnPlatformLanded);
            _signalBus.TryUnsubscribe<GameStartedSignal>(OnGameStarted);
        }

        private void OnPlatformLanded(PlatformLandedSignal signal)
        {
            if (_usedPlatforms.Contains(signal.PlatformInstanceID))
                return;

            _usedPlatforms.Add(signal.PlatformInstanceID);
            
            _scoreService.AddScore();
        }

        private void OnGameStarted()
        {
            _usedPlatforms.Clear();
            _scoreService.ResetScore();
        }
    }
}
