using System;
using DoodleJump.UI.Binders;
using DoodleJump.UI.Models;
using DoodleJump.UI.ViewModels;
using DoodleJump.UI.Views;
using UnityEngine;
using Zenject;

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