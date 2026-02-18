using System;
using System.Collections.Generic;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using Zenject;

namespace DoodleJump.Player
{
    public class PlayerScoreHandler : IInitializable, IDisposable
    {
        private readonly IScoreService _scoreService;
        private readonly SignalBus _signalBus;

        private readonly List<int> _usedPlatforms = new();

        [Inject]
        public PlayerScoreHandler(IScoreService scoreService, SignalBus signalBus)
        {
            _scoreService = scoreService;
            _signalBus = signalBus;
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GameStartedSignal>(OnGameStarted);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public bool TryAddScore(int platformId)
        {
            if (_usedPlatforms.Contains(platformId))
                return false;

            _usedPlatforms.Add(platformId);

            _scoreService.AddScore();

            return true;
        }

        private void OnGameStarted()
        {
            _usedPlatforms.Clear();
            _scoreService.ResetScore();
        }
    }
}