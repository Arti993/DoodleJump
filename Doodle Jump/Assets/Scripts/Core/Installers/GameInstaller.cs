using Zenject;
using DoodleJump.Core.Bootstrap;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
            Container.BindInterfacesTo<GameBootstrap>().AsSingle();
        }
    }
}
