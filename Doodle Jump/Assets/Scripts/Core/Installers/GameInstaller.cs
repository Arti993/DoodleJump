using Zenject;
using DoodleJump.Core.Bootstrap;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameStateService>().AsSingle();
            Container.BindInterfacesTo<GameplayBootstrap>().AsSingle();
        }
    }
}
