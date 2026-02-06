using UnityEngine;
using Zenject;
using DoodleJump.Data;
using DoodleJump.Player.Model;
using DoodleJump.Player.View;
using DoodleJump.Player.ViewModel;

namespace DoodleJump.Player.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerConfig _playerConfig;

        [SerializeField]
        private PlayerView _playerView;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromScriptableObject(_playerConfig).AsSingle();

            if (_playerView != null)
                Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();
            else
                Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<PlayerModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerViewModel>().AsSingle();
        }
    }
}
