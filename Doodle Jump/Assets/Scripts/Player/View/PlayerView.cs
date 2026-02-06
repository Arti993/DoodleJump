using UnityEngine;
using Zenject;

namespace DoodleJump.Player.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (_rigidbody2D == null)
                    _rigidbody2D = GetComponent<Rigidbody2D>();

                return _rigidbody2D;
            }
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector2 Velocity
        {
            get => Rigidbody2D.velocity;
            set => Rigidbody2D.velocity = value;
        }
    }
}
