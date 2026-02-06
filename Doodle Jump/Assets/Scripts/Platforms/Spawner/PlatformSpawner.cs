using UnityEngine;
using Zenject;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Data;
using DoodleJump.Platforms.View;

namespace DoodleJump.Platforms.Spawner
{
    public class PlatformSpawner : IInitializable, ITickable
    {
        private const float SpawnHeightOffset = 2f;
        private const float SpawnAheadDistance = 15f;
        private const float SpawnHorizontalHalfRange = 2.5f;

        private readonly PlatformViewFactory _platformFactory;
        private readonly GameConfig _gameConfig;
        private readonly IGameStateService _gameStateService;

        private float _nextSpawnHeight;
        private float _highestPlatformHeight;

        [Inject]
        public PlatformSpawner(
            PlatformViewFactory platformFactory,
            GameConfig gameConfig,
            IGameStateService gameStateService)
        {
            _platformFactory = platformFactory;
            _gameConfig = gameConfig;
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            _nextSpawnHeight = 0f;
            _highestPlatformHeight = 0f;
            SpawnInitialPlatforms();
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            float playerHeight = GetHighestReachedHeight();
            if (playerHeight > _highestPlatformHeight)
                _highestPlatformHeight = playerHeight;

            while (_nextSpawnHeight < _highestPlatformHeight + SpawnAheadDistance)
            {
                SpawnPlatform(_nextSpawnHeight);
                _nextSpawnHeight += SpawnHeightOffset;
            }
        }

        private void SpawnInitialPlatforms()
        {
            for (int i = 0; i < _gameConfig.InitialPlatformCount; i++)
            {
                SpawnPlatform(_nextSpawnHeight);
                _nextSpawnHeight += SpawnHeightOffset;
            }
        }

        private void SpawnPlatform(float height)
        {
            float x = Random.Range(-SpawnHorizontalHalfRange, SpawnHorizontalHalfRange);
            Vector3 position = new Vector3(x, height, 0f);
            PlatformView platform = _platformFactory.Create();
            platform.transform.position = position;
        }

        private float GetHighestReachedHeight()
        {
            return Camera.main != null ? Camera.main.transform.position.y : 0f;
        }
    }
}
