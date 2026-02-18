using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using DoodleJump.Data;
using UnityEngine;
using Zenject;

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

            Container.BindInterfacesTo<SceneLoadingService>().AsSingle();
            Container.BindInterfacesTo<ScoreService>().AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<ScoreChangedSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<GameOverSignal>();
        }
    }
}