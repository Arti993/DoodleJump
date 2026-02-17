using System;
using UnityEngine;
using Zenject;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;

namespace DoodleJump.Player
{
    public class PlayerBounceHandler : IInitializable, IFixedTickable, IDisposable
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly GameConfig _gameConfig;
        private readonly IGameStateService _gameStateService;
        private readonly PlayerDeathChecker _playerDeathChecker;
        private readonly SignalBus _signalBus;

        private bool _isFlyingUp;
        private float _targetHeight;

        [Inject]
        public PlayerBounceHandler(
            PlayerBehaviour playerBehaviour,
            GameConfig gameConfig,
            IGameStateService gameStateService,
            PlayerDeathChecker playerDeathChecker,
            SignalBus signalBus)
        {
            _playerBehaviour = playerBehaviour;
            _gameConfig = gameConfig;
            _gameStateService = gameStateService;
            _playerDeathChecker = playerDeathChecker;
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

        public void FixedTick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;
            
            if(_playerDeathChecker.IsPlayerAlive == false)
                return;

            CheckReachedHighestPoint();
        }

        private void OnPlatformLanded(PlatformLandedSignal signal)
        {
            ApplyBounce();
        }

        private void OnGameStarted()
        {
            _isFlyingUp = false;
            _playerBehaviour.SetColliderEnabled(true);
        }

        private void ApplyBounce()
        {
            float heightToReach = _gameConfig.PlatformSpawnInterval + _gameConfig.PlatformHeight;
            float gravityScale = _playerBehaviour.Rigidbody.gravityScale;
            float gravityMagnitude = Mathf.Abs(Physics2D.gravity.y) * gravityScale;
            float requiredVelocityY = Mathf.Sqrt(2f * gravityMagnitude * heightToReach);

            Vector2 velocity = _playerBehaviour.Velocity;
            velocity.y = requiredVelocityY;
            _playerBehaviour.SetVelocity(velocity);

            float jumpStartHeight = _playerBehaviour.transform.position.y;
            _targetHeight = jumpStartHeight + heightToReach;
            _isFlyingUp = true;

            _playerBehaviour.SetColliderEnabled(false);
        }

        private void CheckReachedHighestPoint()
        {
            if (!_isFlyingUp)
                return;

            float currentHeight = _playerBehaviour.transform.position.y;

            if (currentHeight >= _targetHeight || _playerBehaviour.Velocity.y <= 0f)
            {
                _isFlyingUp = false;
                _playerBehaviour.SetColliderEnabled(true);
            }
        }
    }
}
