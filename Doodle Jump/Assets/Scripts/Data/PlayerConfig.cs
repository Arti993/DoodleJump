using UnityEngine;

namespace DoodleJump.Data
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "DoodleJump/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _horizontalSpeed = 8f;

        [SerializeField] private float _gravityScale = 1f;

        public float HorizontalSpeed => _horizontalSpeed;
        public float GravityScale => _gravityScale;
    }
}