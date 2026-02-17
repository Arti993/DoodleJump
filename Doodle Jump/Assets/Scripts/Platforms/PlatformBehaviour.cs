using UnityEngine;

namespace DoodleJump.Platforms
{
    [RequireComponent(typeof(Collider2D))]
    public class PlatformBehaviour : MonoBehaviour
    {
        public void SetSize(float width, float height)
        {
            Vector3 scale = transform.localScale;
            scale.x = width;
            scale.y = height;
            transform.localScale = scale;
        }
    }
}
