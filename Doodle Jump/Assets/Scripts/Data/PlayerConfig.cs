using UnityEngine;

namespace DoodleJump.Data
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "DoodleJump/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField]
        private float _jumpForce = 12f;

        [SerializeField]
        private float _horizontalSpeed = 8f;

        [SerializeField]
        private float _gravityScale = 2f;

        public float JumpForce => _jumpForce;
        public float HorizontalSpeed => _horizontalSpeed;
        public float GravityScale => _gravityScale;
    }
}
