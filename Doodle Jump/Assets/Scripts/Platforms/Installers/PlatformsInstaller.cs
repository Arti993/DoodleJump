using UnityEngine;
using Zenject;
using DoodleJump.Platforms.Spawner;

namespace DoodleJump.Platforms.Installers
{
    public class PlatformsInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _platformPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<PlatformBehaviour, PlatformViewFactory>()
                .FromComponentInNewPrefab(_platformPrefab)
                .UnderTransformGroup("Platforms");

            Container.BindInterfacesAndSelfTo<PlatformSpawner>().AsSingle();
        }
    }
}
