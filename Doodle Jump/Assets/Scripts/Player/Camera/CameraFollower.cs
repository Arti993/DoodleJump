using UnityEngine;
using Zenject;
using DoodleJump.Data;
using DoodleJump.Player.View;

namespace DoodleJump.Player.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        private PlayerView _playerView;
        private GameConfig _gameConfig;
        private Transform _cachedTransform;
        private float _highestY;

        [Inject]
        public void Construct(PlayerView playerView, GameConfig gameConfig)
        {
            _playerView = playerView;
            _gameConfig = gameConfig;
            _cachedTransform = transform;
            _highestY = _cachedTransform.position.y;
        }

        private void LateUpdate()
        {
            float playerY = _playerView.Position.y;
            if (playerY > _highestY)
                _highestY = playerY;

            Vector3 position = _cachedTransform.position;
            position.y = Mathf.Lerp(position.y, _highestY, _gameConfig.CameraFollowSpeed * Time.deltaTime);
            _cachedTransform.position = position;
        }
    }
}
