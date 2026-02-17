using UnityEngine;
using Zenject;
using DoodleJump.Data;

namespace DoodleJump.Player.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameObject _playerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromScriptableObject(_playerConfig).AsSingle();

            if (_playerPrefab != null)
            {
                Container.Bind<PlayerBehaviour>()
                    .FromComponentInNewPrefab(_playerPrefab)
                    .AsSingle();
            }
            else
            {
                throw new System.InvalidOperationException();
            }
            
            Container.BindInterfacesAndSelfTo<PlayerHorizontalMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeathChecker>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBounceHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerScoreHandler>().AsSingle();
        }
    }
}
