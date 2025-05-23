using _Root.Code.MoveFeature;
using UnityEngine;

namespace _Root.Code
{
    public class PlayerMovement : IMovable
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        public PlayerMovement(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }
        
        public void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + (Vector2)direction * _speed * Time.fixedDeltaTime);
        }

        public void Rotate(Vector3 direction)
        {
            
            _rigidbody.MoveRotation(2);
        }
    }
}