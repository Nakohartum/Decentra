using _Root.Code.MoveFeature;
using UnityEngine;

namespace _Root.Code
{
    public class PlayerMovement : IMovable
    {
        private Rigidbody2D _rigidbody;
        private float _speed;
        private Vector2 _inputVector;
        private float _acceleration = 200f;

        public PlayerMovement(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }
        
        public void Move(Vector3 direction)
        {
            
            _rigidbody.AddForce((Vector2)direction * _acceleration);
            if (_rigidbody.velocity.magnitude > _speed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
            }
        }

        public void Rotate(Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.01f)
                return;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.MoveRotation(targetAngle);
        }
    }
}