using DoodleJump.Player;
using Zenject;

namespace DoodleJump.Input
{
    public class InputService : IInputService, ITickable
    {
        private const string HorizontalAxisName = "Horizontal";
        private readonly PlayerHorizontalMovement _playerHorizontalMovement;

        [Inject]
        public InputService(PlayerHorizontalMovement playerHorizontal)
        {
            _playerHorizontalMovement = playerHorizontal;
        }

        public float GetHorizontalDirection()
        {
            return UnityEngine.Input.GetAxisRaw(HorizontalAxisName);
        }

        public void Tick()
        {
            _playerHorizontalMovement.SetDirection(GetHorizontalDirection());
        }
    }
}