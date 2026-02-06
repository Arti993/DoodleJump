using UnityEngine;
using Zenject;
using DoodleJump.UI.Menu.View;
using DoodleJump.UI.Menu.ViewModel;

namespace DoodleJump.UI.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField]
        private MenuView _menuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();

            if (_menuView != null)
                Container.Bind<MenuView>().FromInstance(_menuView).AsSingle();
            else
                Container.Bind<MenuView>().FromComponentInHierarchy().AsSingle();
        }
    }
}
