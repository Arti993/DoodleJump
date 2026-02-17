using UnityEngine;
using Zenject;
using DoodleJump.Data;

namespace DoodleJump.Player.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        private GameConfig _gameConfig;
        private Transform _cachedTransform;
        private Transform _playerCachedTransform;
        private float _highestY;

        [Inject]
        public void Construct(PlayerBehaviour playerBehaviour, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _cachedTransform = transform;
            _highestY = _cachedTransform.position.y;
            _playerCachedTransform = playerBehaviour.transform;
        }

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
    }
}
