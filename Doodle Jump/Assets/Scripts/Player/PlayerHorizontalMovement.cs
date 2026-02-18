using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;
using UnityEngine;
using Zenject;

namespace DoodleJump.Player
{
    public class PlayerHorizontalMovement : IInitializable, IFixedTickable, IDisposable
    {
        private readonly IGameStateService _gameStateService;
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerDeathChecker _playerDeathChecker;
        private readonly SignalBus _signalBus;

        private float _currentHorizontalDirection;

        [Inject]
        public PlayerHorizontalMovement(
            PlayerBehaviour playerBehaviour,
            PlayerConfig playerConfig,
            IGameStateService gameStateService,
            PlayerDeathChecker playerDeathChecker,
            SignalBus signalBus)
        {
            _playerBehaviour = playerBehaviour;
            _playerConfig = playerConfig;
            _gameStateService = gameStateService;
            _playerDeathChecker = playerDeathChecker;
            _signalBus = signalBus;
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GameStartedSignal>(OnGameStarted);
        }

        public void FixedTick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            if (_playerDeathChecker.IsPlayerAlive == false)
                return;

            Vector2 velocity = _playerBehaviour.Velocity;
            velocity.x = _currentHorizontalDirection * _playerConfig.HorizontalSpeed;

            _playerBehaviour.SetVelocity(velocity);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public void SetDirection(float direction)
        {
            _currentHorizontalDirection = direction;
        }

        private void OnGameStarted()
        {
            _currentHorizontalDirection = 0f;
        }
    }
}