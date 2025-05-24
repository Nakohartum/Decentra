using UnityEngine;

namespace _Root.Code.MoveFeature.CarMovement
{
    public class PhysicsCarMovement : IMovable
    {
        private Rigidbody2D _rigidbody;
        private float _speed;
        private float _acceleration;
        private float _turnSpeed;

        public PhysicsCarMovement(Rigidbody2D rigidbody, float speed, float acceleration, float turnSpeed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            _acceleration = acceleration;
            _turnSpeed = turnSpeed;
        }
        
        public void Move(Vector3 direction)
        {
            Move(direction.y);
        }

        public void Rotate(Vector3 direction)
        {
            float directionFactor = Vector2.Dot(_rigidbody.velocity, _rigidbody.transform.up) >= 0 ? 1f : -1f;
            Rotate(direction.x, directionFactor);
        }

        private void Move(float vertical)
        {
            _rigidbody.AddForce(_rigidbody.transform.up * vertical * _acceleration);
            if (_rigidbody.velocity.magnitude > _speed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
            }
        }

        private void Rotate(float horizontal, float factor)
        {
            float turnMultiplier = Mathf.Clamp01(_rigidbody.velocity.magnitude / _speed);
            float torque = -horizontal * turnMultiplier * _turnSpeed * factor;
            _rigidbody.AddTorque(torque);
        }
    }
}