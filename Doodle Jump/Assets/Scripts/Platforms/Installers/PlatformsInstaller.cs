using UnityEngine;
using Zenject;
using DoodleJump.Platforms.Spawner;
using DoodleJump.Platforms.View;

namespace DoodleJump.Platforms.Installers
{
    public class PlatformsInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _platformPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<PlatformView, PlatformViewFactory>()
                .FromComponentInNewPrefab(_platformPrefab)
                .UnderTransformGroup("Platforms");

            Container.BindInterfacesAndSelfTo<PlatformSpawner>().AsSingle();
        }
    }
}
