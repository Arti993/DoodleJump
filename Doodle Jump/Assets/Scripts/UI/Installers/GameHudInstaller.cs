using System;
using UnityEngine;
using Zenject;
using DoodleJump.UI.Core;
using DoodleJump.UI.Hud.Binder;
using DoodleJump.UI.Hud.Model;
using DoodleJump.UI.Hud.View;
using DoodleJump.UI.Hud.ViewModel;

namespace DoodleJump.UI.Installers
{
    public class GameHudInstaller : MonoInstaller
    {
        [SerializeField] private GameHudView _gameHudView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameHudModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameHudViewModel>().AsSingle();

            if (_gameHudView != null)
            {
                Container.Bind<GameHudView>().FromInstance(_gameHudView).AsSingle();
                Container.Bind<IBinder>().To<GameHudBinder>().AsSingle();
                Container.BindInterfacesAndSelfTo<UiBinderInitializer>().AsSingle().NonLazy();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
