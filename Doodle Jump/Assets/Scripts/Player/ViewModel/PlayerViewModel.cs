using System;
using UnityEngine;
using Zenject;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;
using DoodleJump.Player.Model;
using DoodleJump.Player.View;

namespace DoodleJump.Player.ViewModel
{
    public class PlayerViewModel : IInitializable, IFixedTickable, IDisposable
    {
        private const float DeathHeightOffset = 5f;

        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly PlayerConfig _config;
        private readonly IGameStateService _gameStateService;
        private readonly SignalBus _signalBus;

        private float _currentHorizontalDirection;
        private Transform _cameraTransform;

        [Inject]
        public PlayerViewModel(
            PlayerView view,
            PlayerModel model,
            PlayerConfig config,
            IGameStateService gameStateService,
            SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _config = config;
            _gameStateService = gameStateService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<InputDirectionSignal>(OnInputDirection);
            _signalBus.Subscribe<PlatformLandedSignal>(OnPlatformLanded);
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<InputDirectionSignal>(OnInputDirection);
            _signalBus.TryUnsubscribe<PlatformLandedSignal>(OnPlatformLanded);
        }

        public void FixedTick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            if (!_model.IsAlive)
                return;

            CheckDeathByFall();

            Vector2 velocity = _view.Velocity;
            velocity.x = _currentHorizontalDirection * _config.HorizontalSpeed;
            _view.Velocity = velocity;
        }

        private void CheckDeathByFall()
        {
            if (_cameraTransform == null)
                _cameraTransform = UnityEngine.Camera.main != null ? UnityEngine.Camera.main.transform : null;

            if (_cameraTransform == null)
                return;

            if (_view.Position.y < _cameraTransform.position.y - DeathHeightOffset)
                _model.Die();
        }

        public void ApplyBounce(float force)
        {
            if (!_model.IsAlive)
                return;

            Vector2 velocity = _view.Velocity;
            velocity.y = force;
            _view.Velocity = velocity;
        }

        public void ResetState()
        {
            _model.Reset();
            _currentHorizontalDirection = 0f;
            _view.Velocity = Vector2.zero;
        }

        private void OnInputDirection(InputDirectionSignal signal)
        {
            _currentHorizontalDirection = signal.Direction;
        }

        private void OnPlatformLanded(PlatformLandedSignal signal)
        {
            if (!_model.IsAlive)
                return;

            _model.AddScore(1);
            ApplyBounce(signal.BounceForce);
        }

        private void OnGameStarted()
        {
            ResetState();
        }
    }
}
