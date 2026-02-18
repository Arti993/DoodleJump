using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;
using UnityEngine;
using Zenject;

namespace DoodleJump.Player
{
    public class PlayerDeathChecker : IInitializable, IDisposable, IFixedTickable
    {
        private readonly GameConfig _gameConfig;
        private readonly IGameStateService _gameStateService;
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly SignalBus _signalBus;
        private Transform _cameraTransform;

        [Inject]
        public PlayerDeathChecker(
            PlayerBehaviour playerPlayerBehaviour,
            GameConfig gameConfig,
            SignalBus signalBus,
            IGameStateService gameStateService)
        {
            _playerBehaviour = playerPlayerBehaviour;
            _gameConfig = gameConfig;
            _signalBus = signalBus;
            _gameStateService = gameStateService;
            _cameraTransform = UnityEngine.Camera.main.transform;
        }

        public bool IsPlayerAlive { get; private set; } = true;

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GameStartedSignal>(OnGameStarted);
        }

        public void FixedTick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            if (IsPlayerAlive == false)
                return;

            CheckDeathByFall();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        private void CheckDeathByFall()
        {
            if (_playerBehaviour.transform.position.y <
                _cameraTransform.position.y - _gameConfig.DeathHeightOffset)
                Die();
        }

        private void Die()
        {
            if (IsPlayerAlive == false)
                return;

            IsPlayerAlive = false;
            _signalBus.Fire<PlayerDiedSignal>();
        }

        private void OnGameStarted()
        {
            IsPlayerAlive = true;
        }
    }
}