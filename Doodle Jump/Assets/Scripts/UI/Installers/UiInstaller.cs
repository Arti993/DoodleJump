using UnityEngine;
using Zenject;
using DoodleJump.UI.Hud.View;
using DoodleJump.UI.Hud.ViewModel;

namespace DoodleJump.UI.Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private GameHudView _gameHudView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameHudViewModel>().AsSingle();

            if (_gameHudView != null)
                Container.Bind<GameHudView>().FromInstance(_gameHudView).AsSingle();
            else
                Container.Bind<GameHudView>().FromComponentInHierarchy().AsSingle();
        }
    }
}
