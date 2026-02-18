using DoodleJump.UI.Binders;
using DoodleJump.UI.Models;
using DoodleJump.UI.ViewModels;
using DoodleJump.UI.Views;
using UnityEngine;
using Zenject;

namespace DoodleJump.UI.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuView _menuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();

            if (_menuView != null)
                Container.Bind<MenuView>().FromInstance(_menuView).AsSingle();
            else
                Container.Bind<MenuView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IBinder>().To<MenuBinder>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiBinderInitializer>().AsSingle().NonLazy();
        }
    }
}