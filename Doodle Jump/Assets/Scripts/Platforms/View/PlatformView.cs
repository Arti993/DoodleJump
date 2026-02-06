using UnityEngine;
using Zenject;
using DoodleJump.Core.Signals;
using DoodleJump.Data;

namespace DoodleJump.Platforms.View
{
    [RequireComponent(typeof(Collider2D))]
    public class PlatformView : MonoBehaviour
    {
        [SerializeField]
        private float _bounceForce = 12f;

        private SignalBus _signalBus;

        public float BounceForce => _bounceForce;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contactCount == 0 || collision.contacts[0].normal.y < 0.9f)
                return;

            _signalBus?.Fire(new PlatformLandedSignal(transform.position, _bounceForce));
        }
    }
}
