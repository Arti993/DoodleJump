using UnityEngine;
using Zenject;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;

namespace DoodleJump.Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            if (_gameConfig != null)
                Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();

            SignalBusInstaller.Install(Container);

            DeclareSignals();

            Container.BindInterfacesAndSelfTo<SceneLoadingService>().AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<ScoreChangedSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<PlatformLandedSignal>();
            Container.DeclareSignal<InputDirectionSignal>();
        }
    }
}
