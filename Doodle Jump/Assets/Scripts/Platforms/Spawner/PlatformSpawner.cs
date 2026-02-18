using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Data;
using UnityEngine;
using Zenject;

namespace DoodleJump.Platforms.Spawner
{
    public class PlatformSpawner : IInitializable, ITickable
    {
        private const float SpawnAheadDistance = 15f;
        private const float SpawnHorizontalHalfRange = 2.5f;
        private readonly GameConfig _gameConfig;
        private readonly IGameStateService _gameStateService;

        private readonly PlatformsFactory _platformFactory;
        private float _highestPlatformHeight;

        private float _nextSpawnHeight;

        [Inject]
        public PlatformSpawner(
            PlatformsFactory platformFactory,
            GameConfig gameConfig,
            IGameStateService gameStateService)
        {
            _platformFactory = platformFactory;
            _gameConfig = gameConfig;
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            float spawnHeight = _gameConfig.PlayerSpawnHeight;
            float rowsBelow = Mathf.Clamp(_gameConfig.InitialPlatformRowsBelowSpawn, 0,
                _gameConfig.InitialPlatformCount - 1);
            _nextSpawnHeight = spawnHeight - rowsBelow * _gameConfig.PlatformSpawnInterval;
            _highestPlatformHeight = spawnHeight;
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
                _nextSpawnHeight += _gameConfig.PlatformSpawnInterval;
            }
        }

        private void SpawnInitialPlatforms()
        {
            for (int i = 0; i < _gameConfig.InitialPlatformCount; i++)
            {
                SpawnPlatform(_nextSpawnHeight);
                
                _nextSpawnHeight += _gameConfig.PlatformSpawnInterval;
            }
        }

        private void SpawnPlatform(float height)
        {
            var x = Random.Range(-SpawnHorizontalHalfRange, SpawnHorizontalHalfRange);
            var position = new Vector3(x, height, 0f);
            
            var platform = _platformFactory.Create();
            
            platform.transform.position = position;
            
            platform.SetSize(_gameConfig.PlatformWidth, _gameConfig.PlatformHeight);
        }

        private float GetHighestReachedHeight()
        {
            return Camera.main != null ? Camera.main.transform.position.y : 0f;
        }
    }
}