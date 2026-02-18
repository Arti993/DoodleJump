using DoodleJump.Platforms.Spawner;
using UnityEngine;
using Zenject;

namespace DoodleJump.Platforms.Installers
{
    public class PlatformsInstaller : MonoInstaller
    {
        private const string Platforms = "Platforms";

        [SerializeField] private GameObject _platformPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<PlatformBehaviour, PlatformsFactory>()
                .FromComponentInNewPrefab(_platformPrefab)
                .UnderTransformGroup(Platforms);

            Container.BindInterfacesAndSelfTo<PlatformSpawner>().AsSingle();
        }
    }
}