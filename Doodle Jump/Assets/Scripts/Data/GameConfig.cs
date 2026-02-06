using UnityEngine;

namespace DoodleJump.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "DoodleJump/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private float _platformSpawnInterval = 2f;

        [SerializeField]
        private int _initialPlatformCount = 10;

        [SerializeField]
        private float _cameraFollowSpeed = 5f;

        public float PlatformSpawnInterval => _platformSpawnInterval;
        public int InitialPlatformCount => _initialPlatformCount;
        public float CameraFollowSpeed => _cameraFollowSpeed;
    }
}
