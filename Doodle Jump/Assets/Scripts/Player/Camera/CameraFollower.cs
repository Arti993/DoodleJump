using DoodleJump.Data;
using UnityEngine;
using Zenject;

namespace DoodleJump.Player.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        private Transform _cachedTransform;
        private GameConfig _gameConfig;
        private float _highestY;
        private Transform _playerCachedTransform;

        private void LateUpdate()
        {
            float playerY = _playerCachedTransform.position.y;
            if (playerY > _highestY)
                _highestY = playerY;

            Vector3 position = _cachedTransform.position;
            float minY = position.y + _gameConfig.CameraConstantUpSpeed * Time.deltaTime;
            float followY = Mathf.Lerp(position.y, _highestY, _gameConfig.CameraFollowSpeed * Time.deltaTime);
            position.y = Mathf.Max(minY, followY);
            _cachedTransform.position = position;
        }

        [Inject]
        public void Construct(PlayerBehaviour playerBehaviour, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _cachedTransform = transform;
            _highestY = _cachedTransform.position.y;
            _playerCachedTransform = playerBehaviour.transform;
        }
    }
}