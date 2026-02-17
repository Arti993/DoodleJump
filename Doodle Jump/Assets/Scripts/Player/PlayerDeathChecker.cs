using System;
using UnityEngine;
using Zenject;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;

namespace DoodleJump.Player
{
    public class PlayerDeathChecker : IInitializable, IDisposable, IFixedTickable
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly GameConfig _gameConfig;
        private readonly IGameStateService _gameStateService;
        private readonly SignalBus _signalBus;
        
        private bool _isPlayerAlive = true;
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
        
        public bool IsPlayerAlive => _isPlayerAlive;
        
        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GameStartedSignal>(OnGameStarted);
        }

        public void FixedTick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            if (_isPlayerAlive == false)
                return;

            CheckDeathByFall();
        }

        private void CheckDeathByFall()
        {
            if (_playerBehaviour.transform.position.y <
                _cameraTransform.position.y - _gameConfig.DeathHeightOffset)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isPlayerAlive == false)
                return;

            _isPlayerAlive = false;
            _signalBus.Fire<PlayerDiedSignal>();
        }

        private void OnGameStarted()
        {
            _isPlayerAlive = true;
        }
    }
}
