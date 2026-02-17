using System;
using UnityEngine;
using Zenject;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;

namespace DoodleJump.Player
{
    public class PlayerHorizontalMovement : IInitializable, IFixedTickable, IDisposable
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly PlayerConfig _playerConfig;
        private readonly IGameStateService _gameStateService;
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

        public void Initialize()
        {
            _signalBus.Subscribe<InputDirectionSignal>(OnInputDirection);
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<InputDirectionSignal>(OnInputDirection);
            _signalBus.TryUnsubscribe<GameStartedSignal>(OnGameStarted);
        }

        public void FixedTick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;
            
            if(_playerDeathChecker.IsPlayerAlive == false)
                return;

            Vector2 velocity = _playerBehaviour.Velocity;
            velocity.x = _currentHorizontalDirection * _playerConfig.HorizontalSpeed;
            _playerBehaviour.SetVelocity(velocity);
        }

        private void OnInputDirection(InputDirectionSignal signal)
        {
            _currentHorizontalDirection = signal.Direction;
        }

        private void OnGameStarted()
        {
            _currentHorizontalDirection = 0f;
        }
    }
}
