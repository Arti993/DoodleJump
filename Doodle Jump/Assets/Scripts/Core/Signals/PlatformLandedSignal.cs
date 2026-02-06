using UnityEngine;

namespace DoodleJump.Core.Signals
{
    public class PlatformLandedSignal
    {
        public Vector3 PlatformPosition { get; }
        public float BounceForce { get; }

        public PlatformLandedSignal(Vector3 platformPosition, float bounceForce)
        {
            PlatformPosition = platformPosition;
            BounceForce = bounceForce;
        }
    }
}
