using UnityEngine;

namespace DoodleJump.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "DoodleJump/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private float _platformSpawnInterval = 2f;
        [SerializeField] private int _initialPlatformCount = 10;
        [SerializeField] private int _initialPlatformRowsBelowSpawn = 4;
        [SerializeField] private float _playerSpawnHeight = 0f;
        [SerializeField] private float _cameraFollowSpeed = 5f;
        [SerializeField] private float _cameraConstantUpSpeed = 1f;
        [SerializeField] private float _platformHeight = 0.325f;
        [SerializeField] private float _platformWidth = 1.645f;
        [SerializeField] private float _deathHeightOffset = 5f;

        public float PlatformSpawnInterval => _platformSpawnInterval;
        public int InitialPlatformCount => _initialPlatformCount;
        public int InitialPlatformRowsBelowSpawn => _initialPlatformRowsBelowSpawn;
        public float PlayerSpawnHeight => _playerSpawnHeight;
        public float CameraFollowSpeed => _cameraFollowSpeed;
        public float CameraConstantUpSpeed => _cameraConstantUpSpeed;
        public float PlatformHeight => _platformHeight;
        public float PlatformWidth => _platformWidth;
        public float DeathHeightOffset => _deathHeightOffset;
    }
}
