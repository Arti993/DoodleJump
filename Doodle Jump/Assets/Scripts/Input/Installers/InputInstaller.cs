using UnityEngine;
using Zenject;
using DoodleJump.Input.Services;

namespace DoodleJump.Input.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}
