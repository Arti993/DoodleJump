using UnityEngine;
using Zenject;
using DoodleJump.Core.Signals;

namespace DoodleJump.Input.Services
{
    public class InputService : IInputService, ITickable
    {
        private const string HorizontalAxisName = "Horizontal";
        private readonly SignalBus _signalBus;

        [Inject]
        public InputService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public float GetHorizontalDirection()
        {
            return UnityEngine.Input.GetAxisRaw(HorizontalAxisName);
        }

        public void Tick()
        {
            float direction = GetHorizontalDirection();
            _signalBus.Fire(new InputDirectionSignal(direction));
        }
    }
}
