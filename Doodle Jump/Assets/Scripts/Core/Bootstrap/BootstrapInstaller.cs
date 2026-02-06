using UnityEngine;
using Zenject;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapRunner>().AsSingle();
        }
    }
}
