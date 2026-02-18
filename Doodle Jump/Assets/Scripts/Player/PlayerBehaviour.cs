using DoodleJump.Core.Signals;
using DoodleJump.Data;
using DoodleJump.Platforms;
using UnityEngine;
using Zenject;

namespace DoodleJump.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerBounceHandler _bounceHandler;
        private Collider2D _collider;
        private PlayerConfig _config;
        private PlayerDeathChecker _deathChecker;
        private PlayerScoreHandler _scoreHandler;
        private SignalBus _signalBus;

        public Rigidbody2D Rigidbody { get; private set; }

        public Vector2 Velocity => Rigidbody.velocity;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            Rigidbody.gravityScale = _config.GravityScale;

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

            if (_deathChecker.IsPlayerAlive == false)
                return;

            if (collision.gameObject.TryGetComponent(out PlatformBehaviour platform))
            {
                _bounceHandler.ApplyBounce();

                int platformID = platform.gameObject.GetInstanceID();

                _ = _scoreHandler.TryAddScore(platformID);
            }
        }

        [Inject]
        private void Construct(SignalBus signalBus,
            PlayerConfig playerConfig,
            PlayerBounceHandler bounceHandler,
            PlayerScoreHandler scoreHandler,
            PlayerDeathChecker deathChecker)
        {
            _signalBus = signalBus;
            _config = playerConfig;
            _bounceHandler = bounceHandler;
            _scoreHandler = scoreHandler;
            _deathChecker = deathChecker;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Rigidbody.velocity = velocity;
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