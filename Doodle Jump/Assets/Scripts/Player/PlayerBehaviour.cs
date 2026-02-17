using UnityEngine;
using Zenject;
using DoodleJump.Core.Signals;
using DoodleJump.Data;
using DoodleJump.Platforms;

namespace DoodleJump.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private SignalBus _signalBus;
        private PlayerConfig _playerConfig;
        private PlayerDeathChecker _playerDeathChecker;

        [Inject]
        private void Construct(SignalBus signalBus, PlayerConfig playerConfig, PlayerDeathChecker playerDeathChecker)
        {
            _signalBus = signalBus;
            _playerConfig = playerConfig;
            _playerDeathChecker = playerDeathChecker;
        }
        
        public Rigidbody2D Rigidbody => _rigidbody;
        public Vector2 Velocity => _rigidbody.velocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            _rigidbody.gravityScale = _playerConfig.GravityScale;
            
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_collider.enabled == false)
                return;
            
            if(_playerDeathChecker.IsPlayerAlive == false)
                return;

            if (collision.gameObject.TryGetComponent(out PlatformBehaviour _))
                _signalBus.Fire(new PlatformLandedSignal(collision.gameObject.GetInstanceID()));
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void SetColliderEnabled(bool enabled)
        {
            _collider.enabled = enabled;
        }
        
        private void OnGameStarted()
        {
            SetVelocity(Vector2.zero);
            SetColliderEnabled(true);
        }
    }
}
